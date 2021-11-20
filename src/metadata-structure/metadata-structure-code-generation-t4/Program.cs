using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dramatic.Conversational.MFilesAPI;
using ValueOf;

namespace code_generation_t4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Test lowercasing
            var result  = MFilesCodeGeneration.ConvertToStartingLowerCase("PDValueTitle");


            var vault   = MFilesCodeGeneration.ConnectVault();


            //var PDCode = GeneratePropertyDefCode(vault);
            //var CLCode = GenerateClassCode(vault);
            var PVCode = MFilesCodeGeneration.GeneratePropertyValueCode(vault);



            // Using the generated IDs
            var nameOrTitlePropDefID = VaultStructure.PDNameortitleID;
            var documentObjectTypeID = VaultStructure.OTDocumentsID;

            var searchProperties = new PropertyValues();
            //searchProperties.Add(-1, PropertyValueFor.Nameortitle("The title"));

        }

    }
}
