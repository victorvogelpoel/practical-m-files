# Practical M-Files - vault applications

This directory contains the practical samples for M-Files vault applications.

|Project|Description|
|:---|:---|
| [vaultapplication-cleanarch](../../../../tree/main/src/vaultapplication/vaultapplication-cleanarch)  | Sample M-Files vault application with clean architecture setup where the use case code is testable without vault present. |
| ~~[vaultapplication-mediatr](../../../../tree/main/src/vaultapplication/vaultapplication-mediatr)~~ | A **FAILED** experiment on using broker [Mediatr](https://github.com/jbogard/MediatR) in a vault application to create a vertical sliced application that can be unit tested. The gist is that a vault application event handler uses the mediator to send a `AddModificationDateToTitle` command that is handled by the command's handler.  What fails is that we need a valid vault reference at initialization, and I cannot find one that lives long enough. |
| [vaultapplication-net48](../../../../tree/main/src/vaultapplication/vaultapplication-net48) | Sample to demonstrate that a vault Application with target .NET framework 4.8 runs as wel in the vault. |
| [vaultapplication-unittestable-usecase](../../../../tree/main/src/vaultapplication/vaultapplication-unittestable-usecase)  | This vault application demonstrates a *much lesser* clean setup where the use case code is unit-testable without the need of a vault (or other "infrastructure"). TODO: get mock verify to work |
