using System;
using MFilesAPI;

namespace VaultApplicationCleanArchitecture.Application.Interfaces
{
    public interface IObjectRepository
    {
        string GetObjectTitle(ObjVer objVer);
        DateTime GetObjectLastModified(ObjVer objVer);
        void UpdateObjectTitle(ObjVer objVer, string updatedTitle, int lastModifiedByUserId);

    }
}