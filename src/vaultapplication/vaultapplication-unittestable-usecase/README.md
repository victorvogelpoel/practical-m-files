# vaultapplication-unittestable-usecase

This sample demonstrates a unittestable setup for a vault application. The use case code is unit-testable without the need for infrastructure like a vault. This is accompliced by injecting an interface into the use-case code and its methods are used by the usecase: the used M-Files vault API methods have been captured in an `IVaultMethods` interface that redirects the call to the actual API.

## TL;DR;

This 'unittestable usecase' is much more low level to the infrastructure details, eg the M-Files API; you see that in the unit tests, where the use case is verifying M-Files API calls and not more business-like methods.

**I must say I greatly prefer the [clean architecture vault application sample](../vaultapplication-cleanarch), where the **intent** of the code is much clearer and closer to business language.**

What would you prefer to read the title of an object in the use case code? API methods or business-like methods?

```csharp
    # From the 'vaultapplication-unittestable-usecase' vault application code:
    var objectTitle   = _vault.ObjectPropertyOperationsGetProperty(objVer, (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).TypedValue.DisplayValue;

    # From the vaultapplication-cleanarch vault application code:
    var objectTitle   = _objectRepository.GetObjectTitle(objVer); 
```

## Implementation of the vaultapplication-unittestable-usecase

I add an interface `IVaultMethods` that exposes the M-Files API methods that we are using in this sample. You may recognize the `operations` prefixes from the M-Files API.

```csharp
    public interface IVaultMethods
    {
        PropertyValue ObjectPropertyOperationsGetProperty(ObjVer ObjVer, int Property);
        ObjectVersionAndProperties ObjectPropertyOperationsSetProperty(ObjVer ObjVer, PropertyValue PropertyValue);
        ObjectVersionAndProperties ObjectPropertyOperationsSetLastModificationInfoAdmin(ObjVer ObjVer, bool UpdateLastModifiedBy, TypedValue LastModifiedBy, bool UpdateLastModified, TypedValue LastModifiedUtc);
    }
```

The implementation for this interface is just redirecting the methods to the actual M-Files API methods:

```csharp
public class VaultMethods : IVaultMethods
{
    private readonly IVault _vault;

    public VaultMethods(IVault vault)
    {
        _vault = vault ?? throw new ArgumentNullException(nameof(vault));
    }

    public PropertyValue ObjectPropertyOperationsGetProperty(ObjVer ObjVer, int Property)
    {
        return _vault.ObjectPropertyOperations.GetProperty(ObjVer, Property);
    }

    public ObjectVersionAndProperties ObjectPropertyOperationsSetLastModificationInfoAdmin(ObjVer ObjVer, bool UpdateLastModifiedBy, TypedValue LastModifiedBy, bool UpdateLastModified, TypedValue LastModifiedUtc)
    {
        return _vault.ObjectPropertyOperations.SetLastModificationInfoAdmin(ObjVer, UpdateLastModifiedBy, LastModifiedBy, UpdateLastModified, LastModifiedUtc);
    }

    public ObjectVersionAndProperties ObjectPropertyOperationsSetProperty(ObjVer ObjVer, PropertyValue PropertyValue)
    {
        return _vault.ObjectPropertyOperations.SetProperty(ObjVer, PropertyValue);
    }
}
```

And the `IVaultMethods` interface is injected into the use case `AddModificationDateToTitleUseCase` and its `UpdateObjectTitle` method is using its members.

```csharp
public class AddModificationDateToTitleUseCase
{
      public AddModificationDateToTitleUseCase(IVaultMethods vault)
      {
          _vault = vault;
      }

      public void UpdateObjectTitle(ObjVer objVer, int userID)
      {
          // implementation withheld for clarity...
      }
}
```



The `AddModificationDateToTitleUseCase` use case is now unit testable, without vault, because of the injected interface. However, mocking the interface requires assignments and setups that are very close to how M-Files implements things and makes this much less desirable than a true clean architecture where the use case employs methods that are closer to how humans think; see the other clean architecture vault application implementation.

Again, I prefer the vaultapplication-cleanarch setup, because intent and code is much more readable.

## TODO

**getting the mock verify working for ObjectPropertyOperationsSetProperty and ObjectPropertyOperationsSetLastModificationInfoAdmin**