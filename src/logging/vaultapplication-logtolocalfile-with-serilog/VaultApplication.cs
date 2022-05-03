// VaultApplication.cs
// 18-10-2021
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
using System.IO;
using System.Reflection;
using MFiles.VAF.Common;
using MFiles.VAF.Core;
using MFilesAPI;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace VaultApplicationLogToLocalFileWithSerilog
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication : ConfigurableVaultApplicationBase<Configuration>
    {
        private readonly LoggingLevelSwitch _loggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);
        private readonly string _buildFileVersion               = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyFileVersionAttribute), false)).Version;

        protected override void InitializeApplication(Vault vault)
        {
            base.InitializeApplication(vault);

            ConfigureApplication(Configuration);

            // And log some information about this VaultApp
            Log.Information("Starting up VaultApp {VaultAppName} build version {VaultAppBuildVersion} in vault {VaultName}",  ApplicationDefinition.Name, _buildFileVersion, vault.Name);
        }

        // ===========================================================================================================================================================
        // Logging configuration and settings

        /// <summary>
        /// Configure logging in the vault application, even create structure if necessary.
        /// </summary>
        /// <param name="vault"></param>
        public void ConfigureApplication(Configuration configuration)
        {
            // Initialize the _loggingLevelSwitch from configuration
            ConfigureLoggingLevelSwitch(configuration.LogLevel);

            string logFolder = $"C:\\TEMP\\VaultApp-{ApplicationDefinition.Guid}\\";
            Directory.CreateDirectory(logFolder);

            // Configure logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(_loggingLevelSwitch)

                .WriteTo.File($"{logFolder}Log-.txt", outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: 31, rollingInterval: RollingInterval.Day)

                .CreateLogger();
        }

        private void ConfigureLoggingLevelSwitch(string logLevel)
        {
            switch(logLevel)
            {
                case "OFF":     _loggingLevelSwitch.MinimumLevel = ((LogEventLevel) 1 + (int) LogEventLevel.Fatal);     break;  // https://stackoverflow.com/questions/30849166/how-to-turn-off-serilog
                case "INFO":    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;                           break;
                case "WARNING": _loggingLevelSwitch.MinimumLevel = LogEventLevel.Warning;                               break;
                case "ERROR":   _loggingLevelSwitch.MinimumLevel = LogEventLevel.Error;                                 break;
                default:        _loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;                           break;
            }
        }


        /// <summary>
        /// Update the Serilog loggingLevelSwitch, when the LogLevel configuration for the Vault Application is changed in M-Files Admin.
        /// </summary>
        /// <param name="oldConfiguration"></param>
        /// <param name="updateExternals"></param>
        protected override void OnConfigurationUpdated(Configuration oldConfiguration, bool isValid, bool updateExternals)
        {
            if (oldConfiguration.LogLevel != Configuration.LogLevel)
            {
                ConfigureLoggingLevelSwitch(Configuration.LogLevel);

                Log.Information("Log level changed to {LogLevel}", Configuration.LogLevel);
            }

            base.OnConfigurationUpdated(oldConfiguration, isValid, updateExternals);
        }



        /// <summary>
        /// Sample Event that fired upon checkin of a Builtin Document object type.
        /// It logs the event using Serilog.
        /// </summary>
        /// <param name="env"></param>
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        public void BeforeCheckInChangesFinalizeUpdateLogDemo(EventHandlerEnvironment env)
        {

            Log.Information("User {UserID} has checked in document {DisplayID} at {TimeStamp}", env.CurrentUserID, env.DisplayID, DateTime.Now);
        }
    }
}