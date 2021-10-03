# vaultapplication-unittestable-usecase

This vault application demonstrates a *much lesser* clean setup where the use case code is unit-testable without the need of a vault (or other "infrastructure").

The used M-Files vault API methods have been captured in a `IVaultMethods` that redirects the call to the actual API.

```csharp
    public interface IVaultMethods
    {
        PropertyValue ObjectPropertyOperationsGetProperty(ObjVer ObjVer, int Property);
        ObjectVersionAndProperties ObjectPropertyOperationsSetProperty(ObjVer ObjVer, PropertyValue PropertyValue);
        ObjectVersionAndProperties ObjectPropertyOperationsSetLastModificationInfoAdmin(ObjVer ObjVer, bool UpdateLastModifiedBy, TypedValue LastModifiedBy, bool UpdateLastModified, TypedValue LastModifiedUtc);
    }
```

An implementation would just redirect the methods to the actual M-Files API methods:

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

The `IVaultMethods` interface is injected into the use case and `UpdateObjectTitle` is using its members.

```csharp
public class AddModificationDateToTitleUseCase
{
      public AddModificationDateToTitleUseCase(IVaultMethods vault)
      {
          _vault = vault;
      }

      /// <summary>
      /// Add or update the object's modification date to the object's title property.
      /// </summary>
      /// <param name="objVer">objVer of the object to update</param>
      /// <param name="userID">ID of M-Files user to set as LastModifiedBy property (otherwise it will show "(M-Files Server)")</param>
      public void UpdateObjectTitle(ObjVer objVer, int userID)
      {
          // implementation withheld for clarity...
      }
}
```
The `AddModificationDateToTitleUseCase` use case is now unit testable, without vault, because of the `IVaultMethods` interface. However, mocking the interface requires assignments and setups that are very close to how M-Files implemtents things and makes this much less desirable than a true clean architecture where the use case employs methods that are closer to how humans think; see the other clean architecture vault application implementation.

## TODO

**getting the mock verify working for ObjectPropertyOperationsSetProperty and ObjectPropertyOperationsSetLastModificationInfoAdmin**