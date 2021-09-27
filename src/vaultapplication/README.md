# Practical M-Files - vault applications

These directory contains the practical samples for M-Files vault applications.

|Project|Description|
|:---|:---|
| [vaultapplication-cleanarch](../../../../tree/main/src/vaultapplication/vaultapplication-cleanarch)  | Sample M-Files vault application with clean architecture setup where the use case code is testable without vault present. |
| [vaultapplication-logtomsteams-with-serilog](../../../../tree/main/src/vaultapplication/vaultapplication-logtomsteams-with-serilog) | Sample M-Files vault application for demonstrating logging to *a Microsoft Teams channel* using a somewhat customized Serilog.Sinks.MicrosoftTeams.Alternative package. Each log statement is now visualized in the configured Teams channel as a card. |
| [vaultapplication-reporttoeventlog-with-serilog](../../../../tree/main/src/vaultapplication/vaultapplication-reporttoeventlog-with-serilog) | Sample Vault Application for demonstrating logging to the EventLog using the SysUtils.ReportToEventLog() functions through Serilog structured logging with the Serilog.Sinks.MFilesSysUtilsEventLog nuget package. |
