// MFObjectRepository.cs
// 16-9-2021
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFilesAPI;
using VaultApplicationCleanArchitecture.Application.Interfaces;

namespace VaultApplicationCleanArchitecture.Infrastructure
{
    public class MFObjectRepository : IObjectRepository
    {
        private readonly IVault _vault;

        public MFObjectRepository(IVault vault)
        {
            _vault = vault ?? throw new ArgumentNullException(nameof(vault));
        }

        public string GetObjectTitle(ObjVer objVer)
        {
            return _vault.ObjectPropertyOperations.GetProperty(objVer, (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).TypedValue.DisplayValue;
        }


        public DateTime GetObjectLastModified(ObjVer objVer)
        {
            return ((DateTime)_vault.ObjectPropertyOperations.GetProperty(objVer, (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefLastModified).TypedValue.Value).ToLocalTime();
        }


        public void UpdateObjectTitle(ObjVer objVer, string updatedTitle, int lastModifiedByUserId)
        {
            // Set the update title propertyValue
            var updatedTitlePV = new PropertyValue
            {
                PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle
            };
            updatedTitlePV.Value.SetValue(MFDataType.MFDatatypeText, updatedTitle);

            _vault.ObjectPropertyOperations.SetProperty(objVer, updatedTitlePV);

            // Aaaand, reset the LastModifiedBy to the specified user (currentUser); otherwise the object will show "(M-Files Server)".
            var lastModifiedByValue = new TypedValue();
            lastModifiedByValue.SetValue(MFDataType.MFDatatypeLookup, lastModifiedByUserId);

            _vault.ObjectPropertyOperations.SetLastModificationInfoAdmin(objVer, UpdateLastModifiedBy:true, lastModifiedByValue, UpdateLastModified: false, null);
        }
    }
}
