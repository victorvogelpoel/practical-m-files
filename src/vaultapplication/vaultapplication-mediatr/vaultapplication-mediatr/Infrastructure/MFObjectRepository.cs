using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFilesAPI;
using VaultApplicationMediatr.Application.Interfaces;

namespace VaultApplicationMediatr.Infrastructure
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
