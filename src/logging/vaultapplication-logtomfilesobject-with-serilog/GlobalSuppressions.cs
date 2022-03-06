// GlobalSuppressions.cs
// 17-6-2021
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

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:DemoVaultApplication.VaultApplication.BeforeCheckInChangesFinalizeUpdateLogDemo(MFiles.VAF.Common.EventHandlerEnvironment)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>", Scope = "member", Target = "~M:DemoVaultApplication.VaultApplication.LogInformation(MFiles.VAF.Common.EventHandlerEnvironment)~System.String")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>", Scope = "member", Target = "~M:DemoVaultApplication.VaultApplication.LogWarning(MFiles.VAF.Common.EventHandlerEnvironment)~System.String")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>", Scope = "member", Target = "~M:DemoVaultApplication.VaultApplication.LogError(MFiles.VAF.Common.EventHandlerEnvironment)~System.String")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>", Scope = "type", Target = "~T:DemoVaultApplication.VaultApplication")]
[assembly: SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification = "<Pending>", Scope = "member", Target = "~M:DemoVaultApplication.VaultApplication.GetDashboardContent(MFiles.VAF.Configuration.AdminConfigurations.IConfigurationRequestContext)~System.String")]
