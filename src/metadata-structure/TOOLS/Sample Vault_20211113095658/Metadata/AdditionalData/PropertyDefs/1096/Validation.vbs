' Checks that effective through date is not earlier than document date (Property ID 1002)

Option Explicit

' Get Effective through property value
Dim dEffectiveThrough: dEffectiveThrough = PropertyValue.Value

' Get properties for this object
Dim oPropVals: Set oPropVals = CreateObject("MFilesApi.PropertyValues")
set oPropVals = Vault.ObjectPropertyOperations.GetProperties(ObjVer)

' Check if document date property exists on this object
If oPropVals.IndexOf( 1002 ) <> - 1 Then

	' Get document date property value
	Dim dDocumentDate: dDocumentDate = oPropVals.SearchForProperty( 1002 ).TypedValue.Value
	
	' Check that both Effective through and Document date properties have value
	If isDate(dEffectiveThrough) And isDate(dDocumentDate) Then

		If dEffectiveThrough < dDocumentDate Then
			Err.Raise MFScriptCancel, "The expiry date cannot be earlier than document date"
		End If

	End If

End If