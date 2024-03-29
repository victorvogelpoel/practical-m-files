Option Explicit

' Find the first name and last name properties.
' The corresponding property definition IDs are 1105 and 1106.
Dim szFirstName
szFirstName = PropertyValues.SearchForProperty( 1105 ).TypedValue.Value
Dim szLastName
szLastName = PropertyValues.SearchForProperty( 1106 ).TypedValue.Value

' Combine to full name.
Dim szFullName
If IsNull( szFirstName ) Then
	szFullName = szLastName  ' First name not entered.
Else
	szFullName = szFirstName & " " & szLastName
End If

' Store the full name in the property.
Output = szFullName
