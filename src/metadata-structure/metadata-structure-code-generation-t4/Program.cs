using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dramatic.Conversational.MFilesAPI;

namespace code_generation_t4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Test lowercasing
            //var result  = MFilesCodeGeneration.ConvertToStartingLowerCase("PDValueTitle");

            // Helper connecting to vault for developing the code generation code.
            var vault   = MFilesCodeGeneration.ConnectVault();

            // Helper methods for developing generation code
            //var PDCode = MFilesCodeGeneration.GeneratePropertyDefCode(vault);
            //var CLCode = MFilesCodeGeneration.GenerateClassCode(vault);
            //var PVCode = MFilesCodeGeneration.GeneratePropertyValueForCode(vault);


            // ------------------------------------------------------------------------------------------------
            // Sample code that references the generated code from the practical-m-files vault
            // The "vaultstructure_ids.TT" file creates a .CS file on save/build that implements the static "VaultStructure" class with PropertyDef,
            // ObjectType and Class IDs:
            //var nameOrTitlePropDefID = VaultStructure.PDNameortitleID;
            //var documentObjectTypeID = VaultStructure.OTDocumentsID;

            // The "vaultstructure_propertyvaluefor.TT" file creates a .CS file on save/build that implements the static class "PropertyValueFor"
            // with a PropertyValue constructor for each PropertyDef in the vault!
            //var searchProperties = new PropertyValues();
            //searchProperties.Add(-1, PropertyValueFor.PDNameortitle("The title"));

        }

    }
}
