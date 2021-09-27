// VaultApplication.cs
// 23-9-2021
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
using System.Reflection;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Core;
using MFilesAPI;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace VaultapplicationLogToMSTeamsWithSerilog
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

            var thisVaultApp = vault.CustomApplicationManagementOperations.GetCustomApplication("41FB962E-332E-4FCB-AE9C-0A631123F4C1"); // From appdef.xml

            // And log some information about this VaultApp
            Log.Information("Starting up VaultApp {VaultAppName} build version {VaultAppBuildVersion} in vault {VaultName}", thisVaultApp.Name, _buildFileVersion, vault.Name);
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

            // Configure logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(_loggingLevelSwitch)

                .Enrich.WithProperty("Customer", "SomeCustomer")

                // TODO: REPLACE first argument with a uri of a incoming webhook connector to your own MS Teams channel.
                // SEE the instructions on this page for creating the incoming webhook connector and getting the uri to place here:
                // https://docs.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/how-to/add-incoming-webhook
                .WriteTo.MicrosoftTeams("#place uri of a incoming webhook connector to your own MS Teams channel here#",    // "https://[TENANT].webhook.office.com/webhookb2/[SOMEGUID]@[SOMEGUID2]/IncomingWebhook/[SOMEGUID3]/[SOMEGUID4]"
                                        titleTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss}] practical-m-files/vaultapplication-logtomsteams-with-serilog",
                                        omitPropertiesSection: true)

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
        /// <param name="context"></param>
        /// <param name="clientOps"></param>
        /// <param name="oldConfiguration"></param>
        protected override void OnConfigurationUpdated(IConfigurationRequestContext context, ClientOperations clientOps, Configuration oldConfiguration)
        {
            if (oldConfiguration.LogLevel != Configuration.LogLevel)
            {
                ConfigureLoggingLevelSwitch(Configuration.LogLevel);

                Log.Information("Log level changed to {LogLevel}", Configuration.LogLevel);
            }

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