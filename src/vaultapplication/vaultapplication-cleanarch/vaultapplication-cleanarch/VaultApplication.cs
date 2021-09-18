using System;
using System.Diagnostics;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFilesAPI;
using VaultApplicationCleanArchitecture.Application;
using VaultApplicationCleanArchitecture.Infrastructure;

namespace VaultApplicationCleanArchitecture
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication : ConfigurableVaultApplicationBase<Configuration>
    {

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        public void EventHandler_BeforeCheckInChangesFinalizeObject(EventHandlerEnvironment env)
        {
            var vaultData   = new MFObjectRepository(env.Vault);
            var useCase     = new AddModificationDateToTitleUseCase(vaultData);

            useCase.UpdateObjectTitle(env.ObjVer, env.CurrentUserID);
        }
    }
}