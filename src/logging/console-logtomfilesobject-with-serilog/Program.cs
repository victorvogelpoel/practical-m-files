using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Dramatic.LogToMFiles;
using Serilog;
using Serilog.Core;
using Serilog.Events;


namespace ConsoleLogtomfilesobjectWithSerilog
{
    class Program
    {
        static void Main(string[] _)
        {
            // -------------------------------------------------------------------------------------------------------

            try
            {
                var serverApp           = new MFilesAPI.MFilesServerApplication();
                serverApp.Connect(MFilesAPI.MFAuthType.MFAuthTypeLoggedOnWindowsUser);
                var vaultOnServer       = serverApp.GetOnlineVaults().GetVaultByName("practical-m-files");
                var vault               = serverApp.LogInAsUserToVault(vaultOnServer.GUID);


                // Define vault structure for logging if it isn't there: OT "Log", CL "Log", PD "LogMessage", CL "LogFile" and aliases to find them back.
                var structureConfig = new LoggingVaultStructureConfiguration
                {
                    // Structure for the LogObject sink
                    LogObjectTypeNameSingular   = DefaultLoggingVaultStructure.LogObjectTypeNameSingular,             // "Log"
                    LogObjectTypeNamePlural     = DefaultLoggingVaultStructure.LogObjectTypeNamePlural,               // "Logs"
                    LogMessagePropDefName       = DefaultLoggingVaultStructure.LogMessagePropDefName,                 // "LogMessage"

                    LogObjectTypeAlias          = DefaultLoggingVaultStructure.LogObjectTypeAlias,                    // "OT.Serilog.MFilesObjectLogSink.Log"
                    LogClassAlias               = DefaultLoggingVaultStructure.LogClassAlias,                         // "CL.Serilog.MFilesObjectLogSink.Log"
                    LogMessagePropDefAlias      = DefaultLoggingVaultStructure.LogMessagePropertyDefinitionAlias,     // "PD.Serilog.MFilesObjectLogSink.LogMessage"

                    // Structure for the LogFile sink
                    LogFileClassName            = DefaultLoggingVaultStructure.LogFileClassName,                      // "LogFile"
                    LogFileClassAlias           = DefaultLoggingVaultStructure.LogFileClassAlias                      // "CL.Serilog.MFilesObjectLogSink.LogFile"
                };

                // Making sure the logging structure exists or is created in the vault.
                // IMPORTANT: EnsureLoggingVaultStructure() requires the logged on use be an ADMIN.
                // Alternatively, instead of EnsureLoggingVaultStructure(), a vault administrator can create the Logging structure in the vault by hand.
                vault.EnsureLoggingVaultStructure(structureConfig);

                // -------------------------------------------------------------------------------------------------------
                // Now the fun starts!

                // Define the minimal log level for the log pipeline; any log level below this (eg Verbose, Debug), will not go through.
                var loggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);

                // Build a Serilog logger with MFilesObjectLogSink and Console
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.ControlledBy(loggingLevelSwitch)

                    // Log events to a 'rolling' Log object in the vault with a MultiLineText property.
                    .WriteTo.MFilesLogObjectMessage(vault,
                                            mfilesLogObjectNamePrefix:      $"[{Environment.MachineName.ToUpperInvariant()}] practical-m-files-console-Log-",  // eg Log object with name "[LTVICTOR3] practical-m-files-console-Log-2021-11-25"
                                            mfilesLogObjectTypeAlias:       structureConfig.LogObjectTypeAlias,
                                            mfilesLogClassAlias:            structureConfig.LogClassAlias,
                                            mfilesLogMessagePropDefAlias:   structureConfig.LogMessagePropDefAlias,
                                            outputTemplate:                 "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")

                    // AND log events to a 'rolling' Log text document object,

                    .WriteTo.MFilesLogFile(vault,
                                            mfilesLogFileNamePrefix:        $"[{Environment.MachineName.ToUpperInvariant()}] practical-m-files-console-Log-",  // eg document file "[LTVICTOR3] practical-m-files-console-Log-2021-11-25.txt"
                                            mfilesLogFileClassAlias:        structureConfig.LogFileClassAlias,
                                            outputTemplate:                 "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")


                    // Write to colored console terminal :-)
                    .WriteTo.Console()

                    // Write to a rolling file
                    //.WriteTo.File(@"c:\somepath\Log-.txt",
                    //                outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    //                restrictedToMinimumLevel:LogEventLevel.Information,
                    //                rollingInterval: RollingInterval.Day)

                    .CreateLogger();


                // -----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                // Now log messages
                // With above configuration, each Log.xxxx() statement will log to M-Files Log object and Log File, AND to the console.
                // Note that the Log messages do NOT appear immediately in the vault as a Log object, but are collected and pushed every 5 secs.
                // Also note that the Log and LogFile objects will be created in the logged-on user's name.

                Log.Information("This adds this log message to a Log object in the vault with the name \"practical-m-files-console-Log-{Today}\"", DateTime.Today.ToString("yyyy-MM-dd"));

                Log.Information("This adds another info message to the {LogOT} object", structureConfig.LogObjectTypeNameSingular);     // NOTE, structured logging, NOT C# string intrapolation!

                Log.Warning("And now a warning");

                Log.Error("And an ERROR!");

                // NOTE: in M-Files vault desktop application, navigate to objects of class "Log" and documents of LogFile.

                Thread.Sleep(6000);

                // IMPORTANT to flush out the batched messages to the vault, at the end of the application, otherwise messages within the last 5 seconds would not end up in the vault!
                Log.CloseAndFlush();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToDetailedString());
            }

            Console.WriteLine("Now see the OT 'Log' entries in 'practical-m-files` vault...");

            Console.WriteLine("Hit enter to exit");
            Console.ReadLine();
        }



        private static void OutputException(Exception ex)
        {
            Console.WriteLine("{1}:{0}{2}", Environment.NewLine, ex.GetType().FullName, ex.Message);

            if (null != ex.InnerException)
            {
                OutputException(ex.InnerException);
            }
        }
    }




    // source: https://gist.github.com/RehanSaeed/c256828acc04d685d024
    public static class ExceptionExtensions
    {
        public static string ToDetailedString(this Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return ToDetailedString(exception, ExceptionOptions.Default);
        }

        public static string ToDetailedString(this Exception exception, ExceptionOptions options)
        {
            var stringBuilder = new StringBuilder();

            AppendValue(stringBuilder, "Type", exception.GetType().FullName, options);

            foreach (PropertyInfo property in exception
                .GetType()
                .GetProperties()
                .OrderByDescending(x => string.Equals(x.Name, nameof(exception.Message), StringComparison.Ordinal))
                .ThenByDescending(x => string.Equals(x.Name, nameof(exception.Source), StringComparison.Ordinal))
                .ThenBy(x => string.Equals(x.Name, nameof(exception.InnerException), StringComparison.Ordinal))
                .ThenBy(x => string.Equals(x.Name, nameof(AggregateException.InnerExceptions), StringComparison.Ordinal)))
            {
                var value = property.GetValue(exception, null);
                if (value == null && options.OmitNullProperties)
                {
                    if (options.OmitNullProperties)
                    {
                        continue;
                    }
                    else
                    {
                        value = string.Empty;
                    }
                }

                AppendValue(stringBuilder, property.Name, value, options);
            }

            return stringBuilder.ToString().TrimEnd('\r', '\n');
        }

        private static void AppendCollection(
            StringBuilder stringBuilder,
            string propertyName,
            IEnumerable collection,
            ExceptionOptions options)
        {
            stringBuilder.AppendLine($"{options.Indent}{propertyName} =");

            var innerOptions = new ExceptionOptions(options, options.CurrentIndentLevel + 1);

            var i = 0;
            foreach (var item in collection)
            {
                var innerPropertyName = $"[{i}]";

                if (item is Exception)
                {
                    var innerException = (Exception)item;
                    AppendException(
                        stringBuilder,
                        innerPropertyName,
                        innerException,
                        innerOptions);
                }
                else
                {
                    AppendValue(
                        stringBuilder,
                        innerPropertyName,
                        item,
                        innerOptions);
                }

                ++i;
            }
        }

        private static void AppendException(
            StringBuilder stringBuilder,
            string propertyName,
            Exception exception,
            ExceptionOptions options)
        {
            var innerExceptionString = ToDetailedString(
                exception,
                new ExceptionOptions(options, options.CurrentIndentLevel + 1));

            stringBuilder.AppendLine($"{options.Indent}{propertyName} =");
            stringBuilder.AppendLine(innerExceptionString);
        }

        private static string IndentString(string value, ExceptionOptions options)
        {
            return value.Replace(Environment.NewLine, Environment.NewLine + options.Indent);
        }

        private static void AppendValue(
            StringBuilder stringBuilder,
            string propertyName,
            object value,
            ExceptionOptions options)
        {
            if (value is DictionaryEntry)
            {
                DictionaryEntry dictionaryEntry = (DictionaryEntry)value;
                stringBuilder.AppendLine($"{options.Indent}{propertyName} = {dictionaryEntry.Key} : {dictionaryEntry.Value}");
            }
            else if (value is Exception)
            {
                var innerException = (Exception)value;
                AppendException(
                    stringBuilder,
                    propertyName,
                    innerException,
                    options);
            }
            else if (value is IEnumerable && !(value is string))
            {
                var collection = (IEnumerable)value;
                if (collection.GetEnumerator().MoveNext())
                {
                    AppendCollection(
                        stringBuilder,
                        propertyName,
                        collection,
                        options);
                }
            }
            else
            {
                stringBuilder.AppendLine($"{options.Indent}{propertyName} = {value}");
            }
        }
    }

    public struct ExceptionOptions
    {
        public static readonly ExceptionOptions Default = new ExceptionOptions()
        {
            CurrentIndentLevel = 0,
            IndentSpaces = 4,
            OmitNullProperties = true
        };

        internal ExceptionOptions(ExceptionOptions options, int currentIndent)
        {
            this.CurrentIndentLevel = currentIndent;
            this.IndentSpaces = options.IndentSpaces;
            this.OmitNullProperties = options.OmitNullProperties;
        }

        internal string Indent { get { return new string(' ', this.IndentSpaces * this.CurrentIndentLevel); } }

        internal int CurrentIndentLevel { get; set; }

        public int IndentSpaces { get; set; }

        public bool OmitNullProperties { get; set; }
    }

}
