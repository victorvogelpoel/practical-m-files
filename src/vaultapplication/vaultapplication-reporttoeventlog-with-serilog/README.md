# M-Files sample vault application using event logging with Serilog.Sinks.MFilesSysUtilsEventLog

This sample Vault Application demonstraties logging to the EventLog using the SysUtils.ReportToEventLog() functions through Serilog structured logging using the Serilog.Sinks.MFilesSysUtilsEventLog nuget package.

**Two** Serilog.Sinks.MFilesSysUtilsEventLog sinks are configured in this vault application, where the first logs flat text and the other logs events in compact JSON.

```csharp
[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
public void BeforeCheckInChangesFinalizeUpdateLogDemo(EventHandlerEnvironment env)
{
    Log.Information("User {UserID} has checked in document {DisplayID} at {TimeStamp}", env.CurrentUserID, env.DisplayID, DateTime.Now);
}
```

The **result** of a logging statement in this sample vault application are two event log entries:

```
practical-m-files {316EE0B8-8752-40CB-A03D-22B9F647B521}
vaultapplication-reporttoeventlog-with-serilog 0.2 (Process ID: 55780)
----
User 1 has checked in document 3 at 09/23/2021 11:05:06
```

```
practical-m-files {316EE0B8-8752-40CB-A03D-22B9F647B521}
vaultapplication-reporttoeventlog-with-serilog 0.2 (Process ID: 55780)
----
{"@t":"2021-09-23T09:05:06.7822744Z","@m":" User 1 has checked in document 3 at 09/23/2021 11:05:06","@i":"b23db56b","UserID":1,"DisplayID":3,"TimeStamp":"2021-09-23T11:05:06.7822744+02:00"}
```


For more information about Serilog, see [https://serilog.net/](https://serilog.net/) and [https://github.com/serilog/](https://github.com/serilog/)

For more information about the Serilog sink for M-Files SysUtils ReportToEventLog() functions, see [https://github.com/serilog-contrib/Serilog.Sinks.MFilesSysUtilsEventLog](https://github.com/serilog-contrib/Serilog.Sinks.MFilesSysUtilsEventLog)
