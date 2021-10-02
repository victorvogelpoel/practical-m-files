# Practical M-Files - vault applications

These directory contains the practical samples for M-Files vault applications.

|Project|Description|
|:---|:---|
| [vaultapplication-cleanarch](../../../../tree/main/src/vaultapplication/vaultapplication-cleanarch)  | Sample M-Files vault application with clean architecture setup where the use case code is testable without vault present. |
| [vaultapplication-logtomsteams-with-serilog](../../../../tree/main/src/vaultapplication/vaultapplication-logtomsteams-with-serilog) | Sample M-Files vault application for demonstrating logging to *a Microsoft Teams channel* the Serilog.Sinks.MicrosoftTeams.Utf8Json package. Each log statement is now visualized in the configured Teams channel as a card. :-) |
| ~~[vaultapplication-mediatr](../../../../tree/main/src/vaultapplication/vaultapplication-mediatr)~~ | A **FAILED** experiment on using [Mediatr](https://github.com/jbogard/MediatR) in a vault application. The gist is that a vault application event handler uses the mediator to send a `AddModiticationDateToTitle` command that is handled by the command's handler. |
| [vaultapplication-net48](../../../../tree/main/src/vaultapplication/vaultapplication-net48) | Sample to demonstrate that a vault Application with target .NET framework 4.8 runs as wel in the vault. |
| [vaultapplication-reporttoeventlog-with-serilog](../../../../tree/main/src/vaultapplication/vaultapplication-reporttoeventlog-with-serilog) | Sample Vault Application for demonstrating logging to the EventLog using the SysUtils.ReportToEventLog() functions through Serilog structured logging with the Serilog.Sinks.MFilesSysUtilsEventLog nuget package. |
