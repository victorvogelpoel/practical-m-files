// VaultApplication.cs
// 16-9-2021
// Copyright 2021 Dramatic Development - Victor Vogelpoel
// If this works, it was written by Victor Vogelpoel (victor@victorvogelpoel.nl).
// If it doesn't, I don't know who wrote it.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
using System;
using MFiles.VAF.Common;
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