# Practical M-Files - Logging

This directory contains the practical samples for M-files console logging applications.

|Project|Description|
|:---|:---|
| [console-logtomfilesobject-with-serilog](../../../../tree/main/src/logging/console-logtomfilesobject-with-serilog)  | Sample **console** application that connects to the `practical-m-files` M-Files vault and logs some sample messages, which the `Serilog.Sinks.MFilesObject` saves to the vault. |
| **[vaultapplication-logtomfilesobject-with-serilog](../../../../tree/main/src/logging/vaultapplication-logtomfilesobject-with-serilog)**  | **Sample M-Files vault application that uses the Serilog structured logging sink `Serilog.Sinks.MFilesObject` to log to the "`practical-m-files`" M-Files vault.** |
| [vaultapplication-logtolocalfile-with-serilog](../../../../tree/main/src/logging/vaultapplication-logtolocalfile-with-serilog)  | Sample M-Files vault application that uses Serilog structured logging to log to a local file (on-prem scenarios). |
| [vaultapplication-reporttoeventlog-with-serilog](../../../../tree/main/src/logging/vaultapplication-reporttoeventlog-with-serilog) | Sample Vault Application for demonstrating logging to the EventLog using the SysUtils.ReportToEventLog() functions through Serilog structured logging with the Serilog.Sinks.MFilesSysUtilsEventLog nuget package. |
| [vaultapplication-logtomsteams-with-serilog](../../../../tree/main/src/logging/vaultapplication-logtomsteams-with-serilog) | Sample M-Files vault application for demonstrating logging to *a Microsoft Teams channel* the Serilog.Sinks.MicrosoftTeams.Utf8Json package. Each log statement is now visualized in the configured Teams channel as a card. :-) |
