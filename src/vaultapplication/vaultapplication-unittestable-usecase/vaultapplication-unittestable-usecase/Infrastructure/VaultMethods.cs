﻿// VaultMethods.cs
// 3-10-2021
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFilesAPI;

namespace VaultApplicationUnittestableUseCase.Infrastructure
{
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
}
