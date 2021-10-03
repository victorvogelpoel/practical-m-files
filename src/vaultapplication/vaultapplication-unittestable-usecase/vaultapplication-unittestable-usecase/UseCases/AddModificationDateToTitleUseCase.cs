// AddModificationDateToTitleUseCase.cs
// 3-10-2021
// Copyright 2021 Dramatic Development - Victor Vogelpoel
// If this works, it was written by Victor Vogelpoel (victor@victorvogelpoel.nl).
// If it doesn't, I don't know who wrote it.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
using System;
using System.Globalization;
using MFilesAPI;
using VaultApplicationUnittestableUseCase.Infrastructure;

namespace VaultApplicationUnittestableUseCase.UseCases
{
    public class AddModificationDateToTitleUseCase
    {
        private readonly IVaultMethods _vault;

        public AddModificationDateToTitleUseCase(IVaultMethods vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Add or update the object's modification date to the object's title property.
        /// </summary>
        /// <param name="objVer">objVer of the object to update</param>
        /// <param name="userID">ID of M-Files user to set as LastModifiedBy property (otherwise it will show "(M-Files Server)")</param>
        public void UpdateObjectTitle(ObjVer objVer, int userID)
        {
            var objectTitle     = _vault.ObjectPropertyOperationsGetProperty(objVer, (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).TypedValue.DisplayValue;
            var datePostFix     = objectTitle.Length > 11 ? objectTitle.Substring(objectTitle.Length-10) : "";

            var objectModified  = ((DateTime)_vault.ObjectPropertyOperationsGetProperty(objVer, (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefLastModified).TypedValue.Value).ToLocalTime();

            if (DateTime.TryParseExact(datePostFix, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var date))
            {
                // The current title is postfixed with a date.
                if (date.Date != objectModified.Date)
                {
                    objectTitle = objectTitle.Substring(0, objectTitle.Length-11);
                    objectTitle += $" {objectModified:yyyy-MM-dd}";

                    UpdateObjectTitle(objVer, objectTitle, userID);
                }
            }
            else
            {
                // The current objectTitle does not contain a date postfix. Add it
                objectTitle += $" {objectModified:yyyy-MM-dd}";

                UpdateObjectTitle(objVer, objectTitle, userID);
            }
        }


        private  void UpdateObjectTitle(ObjVer objVer, string updatedTitle, int lastModifiedByUserId)
        {
            // Set the update title propertyValue
            var updatedTitlePV = new PropertyValueClass { PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle };
            updatedTitlePV.Value.SetValue(MFDataType.MFDatatypeText, updatedTitle);

            _vault.ObjectPropertyOperationsSetProperty(objVer, updatedTitlePV);

            // Aaaand, reset the LastModifiedBy to the specified user (currentUser); otherwise the object will show "(M-Files Server)".
            var lastModifiedByValue = new TypedValue();
            lastModifiedByValue.SetValue(MFDataType.MFDatatypeLookup, lastModifiedByUserId);

            _vault.ObjectPropertyOperationsSetLastModificationInfoAdmin(objVer, UpdateLastModifiedBy:true, lastModifiedByValue, UpdateLastModified: false, null);
        }

    }
}
