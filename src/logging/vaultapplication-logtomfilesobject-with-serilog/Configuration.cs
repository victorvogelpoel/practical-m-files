// Configuration.cs
// 3-9-2021
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
using System.Runtime.Serialization;
using Dramatic.LogToMFiles;
using MFiles.VAF.Configuration;

namespace VaultapplicationLogtoMFilesObjectWithSerilog
{
    [DataContract]
    public class Configuration
    {
        [DataMember]
        [JsonConfEditor(Label = "Logging")]
        public LoggingConfiguration LoggingConfiguration { get; set; }
    }


    [DataContract]
    public class LoggingConfiguration
    {
        [DataMember]
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [JsonConfEditor(
            TypeEditor      = "options",
            IsRequired      = true,
            Options         = "{selectOptions:[\"OFF\", \"INFO\", \"WARNING\", \"ERROR\"]}",
            DefaultValue    = "OFF",
            Commentable     = true,
            Label           = "Log level",
            HelpText        = "Configure the minimal log level of writing log events to the M-Files Log object: OFF, INFO, WARNING or ERROR."
            )]
        public string LogLevel { get; set; } = "OFF";


        [MFObjType(Required = true)]
        [DataMember]
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [JsonConfEditor(
            Label           = "Log ObjectType",
            IsRequired      = true,
            DefaultValue    = DefaultLoggingVaultStructure.LogObjectTypeAlias,
            Commentable     = true,
            Hidden          = true, ShowWhen = ".parent._children{.key == 'LogLevel' && .value != 'OFF' }",
            HelpText        = "Alias for the Log object type, default is \"OT.Serilog.MFilesObjectLogSink.Log\"")]
        public MFIdentifier LogOT { get; set; } =  DefaultLoggingVaultStructure.LogObjectTypeAlias;


        [MFClass(Required = true, RefMember = nameof(LoggingConfiguration.LogOT))]
        [DataMember]
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [JsonConfEditor(
            Label           = "Log Object class",
            IsRequired      = true,
            DefaultValue    = DefaultLoggingVaultStructure.LogClassAlias,
            Commentable     = true,
            Hidden          = true,
            ShowWhen        = ".parent._children{.key == 'LogLevel' && .value != 'OFF' }",
            HelpText        = "Alias for the Log class, default is \"CL.Serilog.MFilesObjectLogSink.Log\"")]
        public MFIdentifier LogCL { get; set; } = DefaultLoggingVaultStructure.LogClassAlias;


        [MFPropertyDef(Required = true, RefMember = nameof(LoggingConfiguration.LogCL))]
        [DataMember]
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [JsonConfEditor(
            Label           = "LogMessage MultilineText property",
            IsRequired      = true,
            DefaultValue    = DefaultLoggingVaultStructure.LogMessagePropertyDefinitionAlias,
            Commentable     = true,
            Hidden = true, ShowWhen = ".parent._children{.key == 'LogLevel' && .value != 'OFF' }",
            HelpText        = "Alias for the LogMessage property definition, default is \"PD.Serilog.MFilesObjectLogSink.LogMessage\"")]
        public MFIdentifier LogMessagePD { get; set; } = DefaultLoggingVaultStructure.LogMessagePropertyDefinitionAlias;


        [DataMember]
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [JsonConfEditor(
            Label           = "Prefix for name of Log Object",
            IsRequired      = true,
            DefaultValue    = "DemoVaultApp-Log-",
            Commentable     = true,
            Hidden = true, ShowWhen = ".parent._children{.key == 'LogLevel' && .value != 'OFF' }",
            HelpText        = "Prefix for NameOrTitle of the Log objects; default is \"DemoVaultApp-Log-\" and a sample Log object would be named \"[HOSTNAME] DemoVaultApp-Log-2021-05-26\"")]
        public string LogObjectNamePrefix { get; set; } = "DemoVaultApp-Log-";


        // --------------------------------------------------
        // LogFile configuration

        [MFClass(Required = true)]
        [DataMember]
        [Security(ChangeBy = SecurityAttribute.UserLevel.VaultAdmin)]
        [JsonConfEditor(
            Label           = "Log File class",
            IsRequired      = true,
            DefaultValue    = DefaultLoggingVaultStructure.LogFileClassAlias,
            Commentable     = true,
            Hidden = true, ShowWhen = ".parent._children{.key == 'LogLevel' && .value != 'OFF' }",
            HelpText        = "Alias for the LogFile document class , default is \"CL.Serilog.MFilesObjectLogSink.LogFile\"")]
        public MFIdentifier LogFileCL { get; set; } = DefaultLoggingVaultStructure.LogFileClassAlias;

    }
}