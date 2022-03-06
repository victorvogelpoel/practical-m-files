// VaultApplication.cs
// 24-11-2021
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
using System.Reflection;
using System.Text;
using System.Threading;
using Dramatic.LogToMFiles;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Configuration.Domain;
using MFiles.VAF.Configuration.Domain.Dashboards;
using MFiles.VAF.Configuration.Interfaces.Domain;
using MFiles.VAF.Core;
using MFilesAPI;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace VaultapplicationLogtoMFilesObjectWithSerilog
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication : ConfigurableVaultApplicationBase<Configuration>
    {
        private readonly LoggingLevelSwitch                             _loggingLevelSwitch     = new LoggingLevelSwitch(LogEventLevel.Information);
        private readonly string                                         _buildFileVersion       = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyFileVersionAttribute), false)).Version;

        // ===========================================================================================================================================================
        // Logging configuration and settings


        /// <summary>
        /// Initialize the Vault Application, including logging structure in the vault.
        /// </summary>
        /// <param name="vault"></param>
        protected override void InitializeApplication(Vault vault)
        {
            base.InitializeApplication(vault);

            // Initialize the logging level for the serilog logger
            _loggingLevelSwitch.MinimumLevel = GetLoggingLevelFor(Configuration?.LoggingConfiguration?.LogLevel ?? "OFF");

            // DO NOT LOG FROM InitializeApplication. Installing the vault app will take ages and fail ....
        }


        /// <summary>
        /// Build a Serilog logger with the supplied vault reference
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        private ILogger LoggerWith(IVault vault)
        {
            var prefix = Configuration?.LoggingConfiguration?.LogObjectNamePrefix ?? DefaultLoggingVaultStructure.LogObjectNamePrefix;
            if (string.IsNullOrWhiteSpace(prefix)) { prefix = "DemoVaultApp-Log-"; }

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(_loggingLevelSwitch)

                .WriteTo.MFilesLogObjectMessage(vault,
                                            mfilesLogObjectNamePrefix:      $"[{Environment.MachineName.ToUpperInvariant()}] {prefix}",
                                            mfilesLogObjectTypeAlias:       Configuration?.LoggingConfiguration?.LogOT?.Alias           ?? DefaultLoggingVaultStructure.LogObjectTypeAlias,
                                            mfilesLogClassAlias:            Configuration?.LoggingConfiguration?.LogCL?.Alias           ?? DefaultLoggingVaultStructure.LogClassAlias,
                                            mfilesLogMessagePropDefAlias:   Configuration?.LoggingConfiguration?.LogMessagePD?.Alias    ?? DefaultLoggingVaultStructure.LogMessagePropertyDefinitionAlias,
                                            outputTemplate:                 "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")

                // AND build an MFiles LogFile sink, for the fun of it
                .WriteTo.MFilesLogFile(vault,
                                            mfilesLogFileNamePrefix:        $"[{Environment.MachineName.ToUpperInvariant()}] {prefix}",
                                            mfilesLogFileClassAlias:        Configuration?.LoggingConfiguration?.LogFileCL?.Alias       ?? DefaultLoggingVaultStructure.LogFileClassAlias,
                                            outputTemplate:                 "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            return logger;
        }



        // Create a lazy initialized logger with the Permanent Vault
        private static readonly object _loggerObject = new object();
        private ILogger _loggerInstance = null;
        private ILogger Log
        {
            get
            {
                if (_loggerInstance == null)
                {
                    lock(_loggerObject)
                    {
                        if (_loggerInstance == null)
                        {
                            _loggerInstance = LoggerWith(PermanentVault);
                        }
                    }
                }

                return _loggerInstance;
            }
        }


        /// <summary>
        /// Calculate the Serilog logEventLevel from the vault application configured log level
        /// </summary>
        /// <param name="logLevelString"></param>
        /// <returns></returns>
        private LogEventLevel GetLoggingLevelFor(string logLevelString)
        {
            switch(logLevelString)
            {
                case "OFF":     return ((LogEventLevel) 1 + (int) LogEventLevel.Fatal);     // https://stackoverflow.com/questions/30849166/how-to-turn-off-serilog
                case "INFO":    return LogEventLevel.Information;
                case "WARNING": return LogEventLevel.Warning;
                case "ERROR":   return LogEventLevel.Error;

                default:        return LogEventLevel.Information;
            }
        }


        /// <summary>
        /// Update the Serilog loggingLevelSwitch, when the LogLevel configuration for the Vault Application is changed in M-Files Admin.
        /// </summary>
        /// <param name="oldConfiguration"></param>
        /// <param name="updateExternals"></param>
        protected override void OnConfigurationUpdated(Configuration oldConfiguration, bool updateExternals)
        {
            if (oldConfiguration?.LoggingConfiguration?.LogLevel != Configuration?.LoggingConfiguration?.LogLevel)
            {
                _loggingLevelSwitch.MinimumLevel = GetLoggingLevelFor(Configuration?.LoggingConfiguration?.LogLevel);

                Log.Information("Admin changed Log level to {LogLevel}", Configuration?.LoggingConfiguration?.LogLevel);
            }
        }



         /// <summary>
        /// Power down the vault application. At least, flush the logging sinks.
        /// </summary>
        /// <param name="vault"></param>
        protected override void UninitializeApplication(Vault vault)
        {
            Log.Information("VaultApplication {ApplicationName} {BuildVersion} is POWERING DOWN.", ApplicationDefinition.Name, _buildFileVersion);

            // IMPORTANT: CloseAndFlush all sinks - this will flush any batched sinks emit the log events to the vault.
            (this.Log as IDisposable)?.Dispose();

            Thread.Sleep(2500);

            base.UninitializeApplication(vault);
        }


        // -----------------------------------------------------------------------------------------------------------------------------------
        // Proving the Serilog sink can also be used from a vault extension method.

        [VaultExtensionMethod("DemoVaultApp.LogInformation", RequiredVaultAccess = MFVaultAccess.MFVaultAccessCreateDocs)]
        private string LogInformation(EventHandlerEnvironment env)
        {
            this.Log.Information(env.Input);
            return $"Logged the following Information message:\r\n{env.Input}";
        }

        [VaultExtensionMethod("DemoVaultApp.LogWarning", RequiredVaultAccess = MFVaultAccess.MFVaultAccessCreateDocs)]
        private string LogWarning(EventHandlerEnvironment env)
        {
            this.Log.Warning(env.Input);
            return $"Logged the following warning message:\r\n{env.Input}";
        }

        [VaultExtensionMethod("DemoVaultApp.LogError", RequiredVaultAccess = MFVaultAccess.MFVaultAccessCreateDocs)]
        private string LogError(EventHandlerEnvironment env)
        {
            this.Log.Error(env.Input);
            return $"Logged the following error message:\r\n{env.Input}";
        }


        // -----------------------------------------------------------------------------------------------------------------------------------
        // Proving the Serilog sink can also be used from an MF Admin domain menu command.
        // Add a sample command to the MF Admin configuration domain area of this vault application, where you can right click and trigger this command.
        private readonly CustomDomainCommand cmdTestLogMessageMenuItem = new CustomDomainCommand
        {
            ID              = "cmdTestLogMessage",
            Execute         = (context, operations) => operations.ShowMessage(context.Vault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("DemoVaultApp.LogInformation", $"Testing, one, two, three. Logged with love from the vault application domain area.") + "\r\n\r\nNote that it may take 5-10 seconds to show up in the M-Files log object."),
            DisplayName     = "Logging: log a test message",
            Locations       = new List<ICommandLocation> { new DomainMenuCommandLocation(icon: "play") }
        };


        /// <summary>
        /// The command which will be executed.
        /// </summary>
        /// <remarks>The "Execute" method will be called when the command is clicked.</remarks>
        private readonly CustomDomainCommand refreshDashboardCommand = new CustomDomainCommand
        {
            ID = "cmdRefreshDashboard",
            Execute = (c, o) =>
            {
                o.RefreshDashboard();
            }
        };


        // -----------------------------------------------------------------------------------------------------------------------------------
        // Showing Log level and logging vault structure status in the Configuration dashboard in MF Admin
        /// <inheritdoc />
        public override string GetDashboardContent(IConfigurationRequestContext context)
        {
            var dashboard = new StatusDashboard();

            var baseContent = base.GetDashboardContent(context);
            if (!string.IsNullOrWhiteSpace(baseContent))
            {
				dashboard.AddContent(new DashboardCustomContent(baseContent));
            }

            // Reacquire the cached vault structure
            ReinitializeMetadataStructureCache(PermanentVault);

            // NOTE: there is a lot of repeating text in the FormattableStrings below.
            // The strings use "<br/>" for moving to the next line and it seems a DashboardPanel
            // renders "<br/>" as TEXT instead of an HTML newline break if the string is in a
            // FormattableString ARGUMENT instead of the formattable string... Go figure.

            FormattableString loggingState;

            if (null != Configuration && null != Configuration.LoggingConfiguration)
            {
                var loggingConfiguration            = Configuration.LoggingConfiguration;
                var loggingConfigurationIsResolved  = loggingConfiguration.LogOT.IsResolved &&
                                                      loggingConfiguration.LogCL.IsResolved &&
                                                      loggingConfiguration.LogMessagePD.IsResolved &&
                                                      loggingConfiguration.LogFileCL.IsResolved;



                if (loggingConfigurationIsResolved && loggingConfiguration.LogLevel != "OFF")
                {
                    loggingState = $"LOGGING CHECK:<br/>- log level is {loggingConfiguration.LogLevel}.<br/>- logging structure (objectType, classes, propertyDef) is {(loggingConfigurationIsResolved ? "all present in the vault." : "MISSING or incomplete. Please run \"DemoVault.AddLoggingStructure.exe\" to ensure logging structure to the vault and refresh the vaultapp (to reread structure).")}<br/><br/>Checking in a document would be logged via this sample vault application event handler and you would see a log message appear in todays Log object and Log file document. Open the M-Files desktop app to display todays Log object and Log File document.";
                }
                else if (!loggingConfigurationIsResolved)
                {
                    var missingLoggingStructureAliases = PermanentVault.GetMissingLoggingVaultStructure(loggingConfiguration.LogOT.Alias, loggingConfiguration.LogCL.Alias, loggingConfiguration.LogMessagePD.Alias, loggingConfiguration.LogFileCL.Alias);

                    loggingState = $"LOGGING CHECK:<br/>- log level is {loggingConfiguration.LogLevel}.<br/>- logging structure (objectType, classes, propertyDef) is {(loggingConfigurationIsResolved ? "all present in the vault." : "MISSING or incomplete. Please run \"DemoVault.AddLoggingStructure.exe\" to ensure logging structure to the vault and refresh the vaultapp (to reread structure).")}<br/>- Missing logging structure aliases are: {(String.Join(", ", missingLoggingStructureAliases))}";
                }
                else
                {
                    loggingState = $"LOGGING CHECK:<br/>- log level is {loggingConfiguration.LogLevel}.<br/>- logging structure (objectType, classes, propertyDef) is {(loggingConfigurationIsResolved ? "all present in the vault." : "MISSING or incomplete. Please run \"DemoVault.AddLoggingStructure.exe\" to ensure logging structure to the vault and refresh the vaultapp (to reread structure).")}";
                }
            }
            else
            {
                loggingState = $"LOGGING CHECK:<br/>- Could not find configuration for logging in this sample vault application; please configure logging first.";
            }



            var refreshPanel = new DashboardPanel();
            refreshPanel.SetInnerContent(loggingState);

            // Add the refresh command to the panel, and the panel to the dashboard.
            refreshPanel.Commands.Add(DashboardHelper.CreateDomainCommand("Refresh log check", this.refreshDashboardCommand.ID));
            dashboard.AddContent(refreshPanel);

            return dashboard.ToString();
        }


        public override IEnumerable<CustomDomainCommand> GetCommands(IConfigurationRequestContext context)
        {
	        return new List<CustomDomainCommand>(base.GetCommands(context))
	        {
                cmdTestLogMessageMenuItem,
			    refreshDashboardCommand
	        };
        }






        // ===========================================================================================================================================================
        // The business use case events

        /// <summary>
        /// Check if the event is for a document object that has the configured Log File Class
        /// </summary>
        /// <param name="env">EventHandlerEnvironment for the event</param>
        /// <returns>false if the event is NOT for a document of the configured log file class, true if it is.</returns>
        private bool EventIsForLogFileDocument(EventHandlerEnvironment env)
        {
            return env.ObjVerEx.HasClass(Configuration?.LoggingConfiguration?.LogFileCL ?? DefaultLoggingVaultStructure.LogFileClassAlias);
        }


        /// <summary>
        /// Sample event handler that logs check ins.
        /// </summary>
        /// <param name="env"></param>
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        public void BeforeCheckInChangesFinalizeUpdateLogDemo(EventHandlerEnvironment env)
        {
            // Appending log events to the a Log File document will trigger this event again, so we need to block logging about *that* object.
            if (EventIsForLogFileDocument(env)) { return; }


            // Just log about the document object change
            // Note that this statement logs with a logger that uses the PermanetVault reference
            this.Log.Information("User {User} has checked in document {DisplayID} at {TimeStamp}", env.CurrentUserID, env.DisplayID, DateTime.Now);

            // And in 5-10 seconds, logger this message will be visible in the M-Files Desktop app as an Log object and Log document file.


            // ... do other stuff ?
        }
    }
}