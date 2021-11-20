using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFilesAPI;

namespace code_generation_t4
{
    public class MFilesCodeGeneration
    {

        public static string GeneratePropertyValueForCode(Vault vault)
        {
            var propertyDefsAdmin   = vault.PropertyDefOperations.GetPropertyDefsAdmin().Cast<PropertyDefAdmin>();
            var code                = new StringBuilder();

            var allPropertyDefVariableNames     = propertyDefsAdmin.Select(pd => ConvertToVariableName($"PD{pd.PropertyDef.Name}")).ToList();

            foreach (var propertyDefAdmin in propertyDefsAdmin)
            {
                var propertyDef                 = propertyDefAdmin.PropertyDef;
                var shortDataTypeName           = propertyDefAdmin.PropertyDef.DataType.ToString().Substring(10);  // Remove "MFDataType" from eg "MFDataTypeText" -> "Text"
                var aliases                     = propertyDefAdmin.SemanticAliases.Value;
                var propertyDefVariableName     = ConvertToVariableName($"PD{propertyDef.Name}");
                var aliasesList                 = aliases.Split(';').Select(a => a.Trim()).Where(a => !String.IsNullOrWhiteSpace(a)).Distinct().ToList();

                var xmldoc  = $"        /// <summary>Construct a PropertyValue for \"{propertyDef.Name}\" (ID:{propertyDef.ID}), Type:{shortDataTypeName}, Aliases:{(String.IsNullOrWhiteSpace(aliases)?"(none)":$"\"{aliases}\"")}</summary>";

                // Test if the propertyDefVariableName exists more than once in the propDef list
                if (allPropertyDefVariableNames.Count(pd => pd == propertyDefVariableName) > 1)
                {
                    // Aaii, the PropertyDef list contains duplicate names; try the alias
                    var newpropertyDefVariableName = propertyDefVariableName;

                    foreach (var alias in aliasesList)
                    {
                        var aliasVariableName = ConvertToVariableName(alias);

                        if (allPropertyDefVariableNames.Any(pd => pd == aliasVariableName))
                        {
                            newpropertyDefVariableName = aliasVariableName;
                            break;
                        }
                    }

                    if (newpropertyDefVariableName == propertyDefVariableName)
                    {
                        // Alias did not resolve to an new unique variable name.
                        // Now add the propDef ID to make the variable name unique.
                        newpropertyDefVariableName = $"{newpropertyDefVariableName}{propertyDef.ID}";
                    }

                    propertyDefVariableName = newpropertyDefVariableName;
                }

                var dotnettype  = "string";
                switch(propertyDefAdmin.PropertyDef.DataType)
                {
                    case MFDataType.MFDatatypeUninitialized: dotnettype = "string"; break;
                    case MFDataType.MFDatatypeText: dotnettype = "string"; break;
                    case MFDataType.MFDatatypeInteger: dotnettype = "int"; break;
                    case MFDataType.MFDatatypeFloating:dotnettype = "float"; break;
                    case MFDataType.MFDatatypeDate:dotnettype = "DateTime"; break;
                    case MFDataType.MFDatatypeTime:dotnettype = "DateTime"; break;
                    case MFDataType.MFDatatypeTimestamp:dotnettype = "DateTime"; break;
                    case MFDataType.MFDatatypeBoolean:dotnettype = "bool"; break;
                    case MFDataType.MFDatatypeLookup:dotnettype = "Lookup"; break;
                    case MFDataType.MFDatatypeMultiSelectLookup: dotnettype = "Lookups"; break;
                    case MFDataType.MFDatatypeInteger64:dotnettype = "int64"; break;
                    case MFDataType.MFDatatypeFILETIME: dotnettype = "DateTime"; break;
                    case MFDataType.MFDatatypeMultiLineText:dotnettype = "string"; break;
                    case MFDataType.MFDatatypeACL: break;
                }

                code.AppendLine(xmldoc);
                code.AppendLine($"        public static PropertyValue {propertyDefVariableName}({dotnettype} {ConvertToStartingLowerCase(propertyDefVariableName)}) => {shortDataTypeName}PropDef({propertyDef.ID}, {ConvertToStartingLowerCase(propertyDefVariableName)});");
                code.AppendLine();
            }

            return code.ToString();
        }



        public static string GeneratePropertyDefCode(Vault vault)
        {
            var propertyDefsAdmin   = vault.PropertyDefOperations.GetPropertyDefsAdmin().Cast<PropertyDefAdmin>();
            var code                = new StringBuilder();

            var allPropertyDefVariableNames     = propertyDefsAdmin.Select(pd => ConvertToVariableName($"PD{pd.PropertyDef.Name}")).ToList();

            foreach (var propertyDefAdmin in propertyDefsAdmin)
            {
                var propertyDef                 = propertyDefAdmin.PropertyDef;
                var shortDataTypeName           = propertyDefAdmin.PropertyDef.DataType.ToString().Substring(10);  // Remove "MFDataType" from eg "MFDataTypeText" -> "Text"
                var propertyDefVariableName     = ConvertToVariableName($"PD{propertyDef.Name}");
                var aliases                     = propertyDefAdmin.SemanticAliases.Value;
                var aliasesList                 = aliases.Split(';').Select(a => a.Trim()).Where(a => !String.IsNullOrWhiteSpace(a)).Distinct().ToList();

                var xmldoc  = $"        /// <summary>PropertyDef \"{propertyDef.Name}\" (ID:{propertyDef.ID}), Type:{shortDataTypeName}, Aliases:{(String.IsNullOrWhiteSpace(aliases)?"(none)":$"\"{aliases}\"")}</summary>";

                // Test if the propertyDefVariableName exists more than once in the propDef list
                if (allPropertyDefVariableNames.Count(pd => pd == propertyDefVariableName) > 1)
                {
                    // Aaii, the PropertyDef list contains duplicate names; try the alias
                    var newpropertyDefVariableName = propertyDefVariableName;

                    foreach (var alias in aliasesList)
                    {
                        var aliasVariableName = ConvertToVariableName(alias);

                        if (allPropertyDefVariableNames.Any(pd => pd == aliasVariableName))
                        {
                            newpropertyDefVariableName = aliasVariableName;
                            break;
                        }
                    }

                    if (newpropertyDefVariableName == propertyDefVariableName)
                    {
                        // Alias did not resolve to an new unique variable name.
                        // Now add the propDef ID to make the variable name unique.
                        newpropertyDefVariableName = $"{newpropertyDefVariableName}{propertyDef.ID}";
                    }

                    propertyDefVariableName = newpropertyDefVariableName;
                }

                // First output the aliases
                if (aliasesList.Count == 1)
                {
                    code.AppendLine(xmldoc);
                    code.AppendLine($"        public static readonly string {propertyDefVariableName}Alias = \"{aliasesList[0]}\";");
                }
                else
                {
                    var aliasIndex = 1;
                    foreach (var alias in aliasesList)
                    {
                        code.AppendLine(xmldoc);
                        code.AppendLine($"        public static readonly string {propertyDefVariableName}Alias{aliasIndex++} = \"{alias}\";");
                    }
                }

                // Then output the propertyDef
                code.AppendLine(xmldoc);
                code.AppendLine($"        public static readonly int {propertyDefVariableName}ID = {propertyDef.ID};");
                code.AppendLine(xmldoc);
                code.AppendLine($"        public static readonly string {propertyDefVariableName}Name = \"{propertyDef.Name}\";");
                code.AppendLine(xmldoc);
                code.AppendLine($"        public static readonly MFDataType {propertyDefVariableName}DataType = MFDataType.{propertyDef.DataType};");
                code.AppendLine();
            }

            return code.ToString();
        }

        public static string GenerateClassCode(Vault vault)
        {
            var classesAdmin        = vault.ClassOperations.GetAllObjectClassesAdmin().Cast<ObjectClassAdmin>();
            var classGroups         = vault.ClassGroupOperations.GetClassGroups((int)MFBuiltInDocumentClass.MFBuiltInDocumentClassUnclassifiedDocument).Cast<ClassGroup>().ToList(); // TODO: get class groups for all objectTypes

            var code                = new StringBuilder();

            foreach (var classObject in classesAdmin)
            {
                var classVariableName   = ConvertToVariableName($"CL{classObject.Name}");
                var aliases             = classObject.SemanticAliases.Value;
                var aliasesList         = aliases.Split(';').Select(a => a.Trim()).Where(a => !String.IsNullOrWhiteSpace(a)).Distinct().ToList();

                var xmldoc = $"        /// <summary>Class \"{classObject.Name}\" (ID:{classObject.ID}, OT:{classObject.ObjectType}), Aliases:{(String.IsNullOrWhiteSpace(aliases)?"(none)":$"\"{aliases}\"")}</summary>";

                // Classes may have the same name, but have a different ID. For example, multiple "Memo" classes exist in different class groups.
                // We need to prefix the generated variable name with classgroup name
                if (classesAdmin.Count(c => c.Name == classObject.Name) > 1)
                {
                    // Find the ParentClassGroup for the
                    var parentClassGroupName = classGroups.Find(cg => cg.Members.IndexOf(classObject.ID) != -1)?.Name;

                    // Postfix the classVariableName with ClassGroup name
                    classVariableName += parentClassGroupName;
                }

                classVariableName               = ConvertToVariableName($"{classVariableName}", "CL");

                // First output the aliases
                if (aliasesList.Count == 1)
                {
                    code.AppendLine(xmldoc);
                    code.AppendLine($"        public static readonly string {classVariableName}Alias = \"{aliasesList[0]}\";");
                }
                else
                {
                    var aliasIndex = 1;
                    foreach (var alias in aliasesList)
                    {
                        code.AppendLine(xmldoc);
                        code.AppendLine($"        public static readonly string {classVariableName}Alias{aliasIndex++} = \"{alias}\";");
                    }
                }

                code.AppendLine(xmldoc);
                code.AppendLine($"        public static readonly int {classVariableName}ID = {classObject.ID};");
                code.AppendLine(xmldoc);
                code.AppendLine($"        public static readonly string {classVariableName}Name = \"{classObject.Name}\";");
                code.AppendLine(xmldoc);
                code.AppendLine($"        public static readonly int {classVariableName}ObjectType = {classObject.ObjectType};");
                code.AppendLine(xmldoc);
                code.AppendLine($"        public static readonly int {classVariableName}NamePropertyDef = {classObject.NamePropertyDef};");
                code.AppendLine();
            }

            return code.ToString();
        }


        public static Vault ConnectVault()
        {
            var mfServerApplication = new MFilesServerApplication();

            var vaultID = "{316EE0B8-8752-40CB-A03D-22B9F647B521}";
            var vaultServer = "LTVICTOR3";
            var vaultPort = "2266";

            // Connect using the default authentication details,
            // specifying the server details.
            mfServerApplication.Connect(
                ProtocolSequence: "ncacn_ip_tcp",
                NetworkAddress: vaultServer,
                Endpoint: vaultPort);

            // Obtain a connection to the vault with GUID {C840BE1A-5B47-4AC0-8EF7-835C166C8E24}.
            // Note: this will except if the vault is not found.
            var vault = mfServerApplication.LogInToVault(vaultID);
            var vaultName = vault.Name;

            return vault;
        }

        /// <summary>
        /// "PDSomeThing" -> "pdsomeThing"
        /// </summary>
        /// <param name="vaultResourceName"></param>
        /// <returns></returns>
        public static string ConvertToStartingLowerCase(string vaultResourceName)
        {
            if (String.IsNullOrEmpty(vaultResourceName)) { return ""; }

            var newString = new StringBuilder();

            for (int i=0; i<vaultResourceName.Length; i++)
            {
                if (Char.IsUpper(vaultResourceName[i]))
                {
                    newString.Append(Char.ToLower(vaultResourceName[i]));
                }
                else
                {
                    newString.Append(vaultResourceName.Substring(i));
                    break;
                }
            }

            return newString.ToString();
        }


        public static string ConvertToVariableName(string vaultResourceName, string prefixWhenStartingWithDigit = "S")
        {
            if (string.IsNullOrWhiteSpace(vaultResourceName))
                return vaultResourceName;

            vaultResourceName = vaultResourceName.Normalize(NormalizationForm.FormD);
            var chars = vaultResourceName.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark && Char.IsLetterOrDigit(c)).ToArray();
            vaultResourceName = new string(chars).Normalize(NormalizationForm.FormC);

            if (vaultResourceName.Length == 0)
            {
                throw new ArgumentException($"vault resource name \"{vaultResourceName}\" results in empty variableName.", nameof(vaultResourceName));
            }

            if (Char.IsDigit(vaultResourceName[0]))
            {
                vaultResourceName = prefixWhenStartingWithDigit + vaultResourceName;
            }

            return vaultResourceName;
        }
    }
}
