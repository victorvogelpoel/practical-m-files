# M-Files Vault Application with clean architecture setup

This vault application demonstrates a clean architecture setup where the use case code is testable without the need of a vault (or other "infrastructure").

A vault with specific structure and contents, webservices, files, databases are 'infrastructure' that you don't want to directly depend upon in your use-case code. The essence of clean architecture is that the use case code depends upon abstractions of the infrastructure and thus the infrastructure can be mocked or stubbed for the use-case tests. 
Abstract the Vault data access that you need in the use case into interfaces; the use-case code depends upon these interfaces. The actual vault data access code are implementations of those interfaces and are injected into the use-case code class by the vault application.
A unit test for the use-case code would simply use mocked interfaces or a stub implementation.

For more information about clean architecture, see

* [The Clean Architecture - Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
* [How to Structure Your Application Code (The Easy Way) - Nicklas Millard](https://medium.com/geekculture/how-to-structure-your-application-code-the-easy-way-e4107e2e5e86)

## Sample Use Case

The use case for this sample vault application is that *the NameOrTitle of every document file will be appended with the object "modified" date*.

The vault application will trigger upon creating or modifying a document file. It will then update the document object title.

The use case is called AddModificationDateToTitleUseCase. The solution also contains unit tests where scenario's in the use case are tested with mocks and stubs.

The clean architecture setup for this usecase is:

```
/Application
  /Interfaces
    IObjectRepository.cs
  AddModificationDateToTitleUseCase.cs
/Infrastructure
  ObjectRepository.cs
```

The use case code is very simple (abbreviated for clarity) where an object title needs to be updated.

```csharp
public class AddModificationDateToTitleUseCase
{
    public AddModificationDateToTitleUseCase(IObjectRepository objectRepository)
    {
        _objectRepository  = objectRepository;
    }

    public void UpdateObjectTitle(ObjVer objVer, int userID)
    {
        // implementation removed for clarity
    }
}
```


The `AddModificationDateToTitleUseCase` code depends upon an interface for an *abstracted* object repository and has no direct calls to the M-Files vault. Instead it contains 'solution domain' methods to get the object title, modified date and update the object title for the use case.

```csharp
public interface IObjectRepository
{
    string GetObjectTitle(ObjVer objVer);
    DateTime GetObjectLastModified(ObjVer objVer);
    void UpdateObjectTitle(ObjVer objVer, string updatedTitle, int lastModifiedByUserId);
}
```

By mocking or stubbing this interface, we can now **unit test** the use case scenarios without even a vault present!

The infrastructure implementation `ObjectRepository.cs` has the full implementation of these methods and interact with the M-Files vault using the COM API.

The event trigger in `vaultapplication.cs` finally instantiates the ObjectRepository with the event vault reference, feeds it to the AddModificationDateToTitleUseCase and calls the use case method to handle the event.

```csharp
[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
[EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
public void EventHandler_BeforeCheckInChangesFinalizeObject(EventHandlerEnvironment env)
{
    var vaultData   = new MFObjectRepository(env.Vault);
    var useCase     = new AddModificationDateToTitleUseCase(vaultData);

    useCase.UpdateObjectTitle(env.ObjVer, env.CurrentUserID);
}
```

## Use case tests

Use case tests can be found in [the test project](https://github.com/victorvogelpoel/practical-m-files/tree/main/src/vaultapplication/vaultapplication-cleanarch/vaultapplication-cleanarch-tests).

* When a document object title has no modified date, then add the modified date to the title
* When a document object title already contains a modified date, then replace it with the new modified date
* When a document object title already contains a modified date and trailing spaces, then replace it with the new modified date
* When a document object title already contains a modified date and it is the current modified date, then no change to the title

Tests are worked out with mocks and stubs variants. This  is a stub variant:

```csharp
[DataTestMethod]
[DataRow("A document title")]
[DataRow("A")]
public void WhenTitleHasNoModifiedDate_ExpectLastModifiedDateAppendedToTitle2(string objectTitle)
{
    var lastModified            = DateTime.Now;
    var lastModifiedUTC         = lastModified.ToUniversalTime();
    var objVer                  = new ObjVer();
    var userId                  = 1;

    var expectedUpdatedTitle    = $"{objectTitle} {lastModified:yyyy-MM-dd}";

    var objectRepository        = new ObjectRepositoryStub();
    objectRepository.AddObjectStub(objVer, objectTitle, lastModifiedUTC);

    // ACTION
    var useCase = new AddModificationDateToTitleUseCase(objectRepository);
    useCase.UpdateObjectTitle(objVer, userId);

    // ASSERT
    objectRepository.GetObjectTitle(objVer).Should().Be(expectedUpdatedTitle);
}

```
