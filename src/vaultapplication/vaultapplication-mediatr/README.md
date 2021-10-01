# vaultapplication-mediatr

A **FAILED** experiment on using [Mediatr](https://github.com/jbogard/MediatR) in a vault application. The gist is that a vault application event handler uses the mediator to send a `AddModiticationDateToTitle` command that is handled by the command's handler.
The handler has been setup in a clean architecture fashion, which makes the AddModiticationDateToTitle use case unit testable without a vault or other infrastructure.

```csharp
[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
[EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
public void EventHandler_BeforeCheckInChangesFinalizeObject(EventHandlerEnvironment env)
{
	var request = new AddModiticationDateToTitle
	{
		ObjVer = env.ObjVer,
		UserID = env.CurrentUserID
	};

	string updatedTitle = _mediator.Send(request).Result;
}

```

Unfortunately, the handler uses an injected IObjectRepository and at vault application startup its implementation, *and a vault reference* (a COM object), must be registered in the SimpleInjector dependency injection container.
When the AddModiticationDateToTitle handler executes, the vault reference has been released already, resulting in a error "COM object that has been separated from its underlying RCW cannot be used.".

I cannot figure out on what Vault reference to use at registration time. Still uploading this sample, perhaps someone else has better luck with it.


