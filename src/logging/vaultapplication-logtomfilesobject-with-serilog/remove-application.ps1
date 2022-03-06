param(

    [Parameter(Mandatory=$False)]
    [string[]] $InstallGroups=@()

)

# Load M-Files API.
$null = [System.Reflection.Assembly]::LoadWithPartialName("Interop.MFilesAPI")

# Application details
$appFilePath = "bin\Debug\vaultapplication-logtomfilesobject-with-serilog.mfappx"
$appGuid = "B2790DFD-21D2-4315-8368-146F83F23CF8"

# Target vault
$vaultName = "practical-m-files"

# Connection details
$authType = [MFilesAPI.MFAuthType]::MFAuthTypeLoggedOnWindowsUser
$userName = ""
$password = ""
$domain = ""
$spn = ""
$protocolSequence = "ncacn_ip_tcp"
$networkAddress = "localhost"
$endpoint = 2266
$encryptedConnection = $false
$localComputerName = ""

# Default to reporting an error if the script fails.
$ErrorActionPreference = 'Stop'

# Append the current path so we have the full location (required in some situations).
$currentDir = Get-Location
$appFilePath = Join-Path $currentDir $appFilePath

$vaultConnections = @()

# If we are not using an external JSON file
# then use the connection/authentication information defined at the top of the file.
$vaultConnections += [PSCustomObject]@{
    vaultName = $vaultName
    authType = $authType
    userName = $userName
    password = $password
    domain = $domain
    spn = $spn
    protocolSequence = $protocolSequence
    networkAddress = $networkAddress
    endpoint = $endpoint
    encryptedConnection = $encryptedConnection
    localComputerName = $localComputerName
}

ForEach($vc in $vaultConnections) {
    # Connect to M-Files Server.
    Write-Host "  Connecting to '$($vc.vaultName)' on $($vc.networkAddress)..."
    $server = new-object MFilesAPI.MFilesServerApplicationClass
    $tzi = new-object MFilesAPI.TimeZoneInformationClass
    $tzi.LoadWithCurrentTimeZone()
    $null = $server.ConnectAdministrativeEx( $tzi, $vc.authType, $vc.userName, $vc.password, $vc.domain, $vc.spn, $vc.protocolSequence, $vc.networkAddress, $vc.endpoint, $vc.encryptedConnection, $vc.localComputerName )
    # Get the target vault.
    $vaultOnServer = $server.GetOnlineVaults().GetVaultByName( $vc.vaultName )
    # Login to vault.
    $vault = $vaultOnServer.LogIn()
    # Install application.
    Write-Host "    Removing application..."
    try {
        $vault.CustomApplicationManagementOperations.UninstallCustomApplication($appGuid)
        # Restart vault.
        Write-Host "    Restarting vault..."
        $server.VaultManagementOperations.TakeVaultOffline( $vaultOnServer.GUID, $true )
        $server.VaultManagementOperations.BringVaultOnline( $vaultOnServer.GUID )
        # Short sleep to prevent SQL errors on logging in.
        Start-Sleep -Milliseconds 500
        # Login to vault again.
        $vault = $vaultOnServer.LogIn()
    } catch {
        # Already exists
        if($_.Exception -Match "0x80040031") {
            Write-Host "    This application version already exists on the vault, installation skipped"
        }
        else {
            throw
        }
    }
}