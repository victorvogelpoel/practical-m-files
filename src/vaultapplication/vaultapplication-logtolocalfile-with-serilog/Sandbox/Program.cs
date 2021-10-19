using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF.Common;
using Serilog.Core;
using Serilog;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {

            string logFolder = Path.Combine(Path.GetTempPath(), $"VaultApp-dab9a4ae-0ee9-40b7-92f1-3a042a671598\\");
            Directory.CreateDirectory(logFolder);

        // Configure logging
            Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.ControlledBy(_loggingLevelSwitch)

                .WriteTo.File($"{logFolder}Log-.txt", outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: 31, rollingInterval: RollingInterval.Day )

                .CreateLogger();


            Log.Information("Test");
        }
    }
}
