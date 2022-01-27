Option Explicit
Dim szAgreementType, szCustomer, dDocumentDate

szAgreementType = PropertyValues.SearchForProperty( 1143 ).TypedValue.DisplayValue
szCustomer = PropertyValues.SearchForProperty( 1079 ).TypedValue.DisplayValue
dDocumentDate = PropertyValues.SearchForProperty( 1002 ).TypedValue.Value


Output = szAgreementType & " - " & szCustomer & " (" & DatePart("m", dDocumentDate) & "/" & DatePart("yyyy", dDocumentDate) & ")"