' Turn typelib constants loading off.
' MFILESAPI_CONSTANTS_OFF

'********************************************************
'
'	M-Files Vault Application Event Handler Script.
'	Transfers the event handling to compiled vault application assembly.
'
'	DO NOT MODIFY!
'
'********************************************************

' Initialize the event handler entry point
Set extension = GetExtensionObject("SampleVaultApplication", "45472745-9d80-4b42-b092-463f3c38b6f1")

' Initialize a new Script Environment
Set oEnv = extension.NewEnvironment()
Call SetupEnvironment(oEnv)


' Trigger the handler
Set oResult = extension.Run(oEnv)
If oResult.ScriptCancelled Then
	Err.Raise MFScriptCancel, oResult.Message
End If

' Pass on any return value.
Call SetReturnValue(oResult)


' =========================================
'
'	SetupEnvironment
'
'  Transfers all script variables to the environment object.
' =========================================
Sub SetupEnvironment(byRef oEnvironment)

	' IGNORE ERRORS
	' (they should only come frome trying to set variables that don't
	' exist in this script type, which is expected)
	On Error Resume Next

	' Copy all possible auto-set script variables to our ScriptEnvironment object

	' Primitives
    oEnvironment.Type = 35
	oEnvironment.CurrentUserID = CurrentUserID.Value
	oEnvironment.DisplayID = DisplayID.Value
	oEnvironment.LoggedOutUserID = LoggedOutUserID.Value
	oEnvironment.Input = Input.Value
	oEnvironment.FileTransferSession = FileTransferSession.Value
	oEnvironment.IsCancellable = IsCancellable.Value

	'Complex
	Set oEnvironment.FileVer = FileVer
	Set oEnvironment.LoginAccount = LoginAccount
	Set oEnvironment.ObjectAccessControlList = ObjectAccessControlList
	Set oEnvironment.ObjVer = ObjVer
	Set oEnvironment.PropertyValues = PropertyValues
	Set oEnvironment.ValueListItem = ValueListItem
	Set oEnvironment.Vault = Vault
	Set oEnvironment.VaultSharedVariables = VaultSharedVariables
	Set oEnvironment.UserAccount = UserAccount
	Set oEnvironment.UserGroupAdmin = UserGroupAdmin
	Set oEnvironment.CurrentTransactionID = CurrentTransactionID
	Set oEnvironment.ParentTransactionID = ParentTransactionID
	Set oEnvironment.MasterTransactionID = MasterTransactionID
	Set oEnvironment.ActivityID = ActivityID
	Set oEnvironment.TransactionCache = TransactionCache
	Set oEnvironment.View = View
	If IsEmpty( CurrentUserSessionInfo ) Then
		Set oEnvironment.CurrentUserSessionInfo = Null
	Else
		Set oEnvironment.CurrentUserSessionInfo = CurrentUserSessionInfo
	End If

End Sub


' =========================================
'
'	SetReturnValue
'
' =========================================
Sub SetReturnValue(byRef oResult)

	' IGNORE ERRORS
	' (they should only come frome trying to set variables that don't
	' exist in this script type, which is expected)
	On Error Resume Next

	Output = oResult.VaultExtensionMethodOutput
End Sub