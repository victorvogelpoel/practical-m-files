# vaultapplication-logtolocalfile-with-serilog

This sample Vault Application demonstrates structured logging with Serilog where log messages are logged to a local Log file. *Of course, this is a scenario is only applicable to a vault application that is installed in a on-premise vault.*

 Please note, that if you are writing a vault application for a cloud vault, this scenario is not of use to you. Logging to the local file system is not permitted, let alone that you can retrieve the logging. You could look at alternatives, like using Serilog structured logging with my [MFilesSysUtilsEventLogSink](../../../../../tree/main/src/vaultapplication/vaultapplication-reporttoeventlog-with-serilog) to log to the Windows event log or even to an [MS Teams channel](../../../../../tree/main/src/vaultapplication/vaultapplication-logtomsteams-with-serilog). :-)

## Running this sample

Build and install this sample vault application to the `practical-m-files` vault.

 This `vaultapplication-logtolocalfile-with-serilog` vault application logs the startup, powerdown and each document check-in to a 'rolling date' log file at (hardcoded path) `C:\TEMP\VaultApp-e461367b-4a4f-498b-a009-fa8d4ccc6085\`. For each day, the Serilog File sink will create a log txt file with the date in its name and remove a log file after 31 days.
 
 The contents of the log file `C:\TEMP\VaultApp-e461367b-4a4f-498b-a009-fa8d4ccc6085\Log-20211019.txt` roughly this:

```text
[21:30:42 INF] Starting up VaultApp "vaultapplication-logtolocalfile-with-serilog" build version 2021.10.19.2111 in vault "practical-m-files"
[21:31:11 INF] User 1 has checked in document 5 at "2021-10-19T21:31:11.7225550+02:00"
```
