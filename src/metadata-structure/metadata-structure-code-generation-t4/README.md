# Metadata/vaultstructure - autogeneration of C# file with vault structure IDs, names, etc

This console application demonstrates that a C# file can be generated automatically from vault structure using a Visual Studio T4 template .

T4 is a text template solution in Visual Studio to generate another file on the fly. The T4 file `vaultstructure.tt` in this solution `code-generation-t4` creates a connection to the local `practical-mfiles` vault, reads its vault structure and generates a C# file with IDs, names, aliases for propertyDefs, objectTypes and classes. You can use this C# file with vault structure IDs in your program.

**Sample result C# file with vault structure IDs** - see [vaultstructure.cs](https://github.com/victorvogelpoel/practical-m-files/blob/main/src/metadata-structure/metadata-structure-code-generation-t4/vaultstructure.cs), created on the fly from [vaultstructure.tt](https://github.com/victorvogelpoel/practical-m-files/blob/main/src/metadata-structure/metadata-structure-code-generation-t4/vaultstructure.tt) 
```csharp
using System;
using MFilesAPI;

// This M-Files vault structure file was generated at 2021-11-10 21:38:49 by a T4 Text Template that read the following vault:
// Vault name:   practical-m-files
// Vault ID:     {316EE0B8-8752-40CB-A03D-22B9F647B521}
// Vault server: localhost:2266
//
// If this works, it was written by Victor Vogelpoel (victor@victorvogelpoel.nl).
// If it doesn't, I don't know who wrote it.

namespace Dramatic.MFiles
{
	public static partial class VaultStructure
    {

        // -----------------------------------------------------------------------------------------------------------------------------------------
        // practical-m-files PropertyDefs

        /// <summary>PropertyDef "Name or title" (ID:0), Type:Text, Aliases:(none)</summary>
        public static readonly int PDNameortitleID = 0;
        /// <summary>PropertyDef "Name or title" (ID:0), Type:Text, Aliases:(none)</summary>
        public static readonly string PDNameortitleName = "Name or title";
        /// <summary>PropertyDef "Name or title" (ID:0), Type:Text, Aliases:(none)</summary>
        public static readonly MFDataType PDNameortitleDataType = MFDataType.MFDatatypeText;

        /// <summary>PropertyDef "Created" (ID:20), Type:Timestamp, Aliases:(none)</summary>
        public static readonly int PDCreatedID = 20;
        /// <summary>PropertyDef "Created" (ID:20), Type:Timestamp, Aliases:(none)</summary>
        public static readonly string PDCreatedName = "Created";
        /// <summary>PropertyDef "Created" (ID:20), Type:Timestamp, Aliases:(none)</summary>
        public static readonly MFDataType PDCreatedDataType = MFDataType.MFDatatypeTimestamp;

        /// <summary>PropertyDef "Created by" (ID:25), Type:Lookup, Aliases:(none)</summary>
        public static readonly int PDCreatedbyID = 25;
        /// <summary>PropertyDef "Created by" (ID:25), Type:Lookup, Aliases:(none)</summary>
        public static readonly string PDCreatedbyName = "Created by";
        /// <summary>PropertyDef "Created by" (ID:25), Type:Lookup, Aliases:(none)</summary>
        public static readonly MFDataType PDCreatedbyDataType = MFDataType.MFDatatypeLookup;

        /// <summary>PropertyDef "Last modified" (ID:21), Type:Timestamp, Aliases:(none)</summary>
        public static readonly int PDLastmodifiedID = 21;
        /// <summary>PropertyDef "Last modified" (ID:21), Type:Timestamp, Aliases:(none)</summary>
        public static readonly string PDLastmodifiedName = "Last modified";
        /// <summary>PropertyDef "Last modified" (ID:21), Type:Timestamp, Aliases:(none)</summary>
        public static readonly MFDataType PDLastmodifiedDataType = MFDataType.MFDatatypeTimestamp;

        /// <summary>PropertyDef "Last modified by" (ID:23), Type:Lookup, Aliases:(none)</summary>
        public static readonly int PDLastmodifiedbyID = 23;
        /// <summary>PropertyDef "Last modified by" (ID:23), Type:Lookup, Aliases:(none)</summary>
        public static readonly string PDLastmodifiedbyName = "Last modified by";
        /// <summary>PropertyDef "Last modified by" (ID:23), Type:Lookup, Aliases:(none)</summary>
        public static readonly MFDataType PDLastmodifiedbyDataType = MFDataType.MFDatatypeLookup;

        /// <summary>PropertyDef "Status changed" (ID:24), Type:Timestamp, Aliases:(none)</summary>
        public static readonly int PDStatuschangedID = 24;
        /// <summary>PropertyDef "Status changed" (ID:24), Type:Timestamp, Aliases:(none)</summary>
        public static readonly string PDStatuschangedName = "Status changed";
        /// <summary>PropertyDef "Status changed" (ID:24), Type:Timestamp, Aliases:(none)</summary>
        public static readonly MFDataType PDStatuschangedDataType = MFDataType.MFDatatypeTimestamp;

        /// <summary>PropertyDef "Single file" (ID:22), Type:Boolean, Aliases:(none)</summary>
        public static readonly int PDSinglefileID = 22;
        /// <summary>PropertyDef "Single file" (ID:22), Type:Boolean, Aliases:(none)</summary>
        public static readonly string PDSinglefileName = "Single file";
        /// <summary>PropertyDef "Single file" (ID:22), Type:Boolean, Aliases:(none)</summary>
        public static readonly MFDataType PDSinglefileDataType = MFDataType.MFDatatypeBoolean;

        /// <summary>PropertyDef "Class" (ID:100), Type:Lookup, Aliases:(none)</summary>
        public static readonly int PDClassID = 100;
        /// <summary>PropertyDef "Class" (ID:100), Type:Lookup, Aliases:(none)</summary>
        public static readonly string PDClassName = "Class";
        /// <summary>PropertyDef "Class" (ID:100), Type:Lookup, Aliases:(none)</summary>
        public static readonly MFDataType PDClassDataType = MFDataType.MFDatatypeLookup;

        /// <summary>PropertyDef "Class groups" (ID:101), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static readonly int PDClassgroupsID = 101;
        /// <summary>PropertyDef "Class groups" (ID:101), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static readonly string PDClassgroupsName = "Class groups";
        /// <summary>PropertyDef "Class groups" (ID:101), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static readonly MFDataType PDClassgroupsDataType = MFDataType.MFDatatypeMultiSelectLookup;


        // Many propertyDefs removed for brevity...


        // -----------------------------------------------------------------------------------------------------------------------------------------
        // practical-m-files ObjectTypes

        /// <summary>ObjectType ID:0 Name:"Document" / "Documents"</summary>
        public static readonly int OTDocumentsID = 0;
        /// <summary>ObjectType ID:0 Name:"Document" / "Documents"</summary>
        public static readonly string OTDocumentsName = "Documents";

        /// <summary>ObjectType ID:9 Name:"Document collection" / "Document collections"</summary>
        public static readonly int OTDocumentcollectionsID = 9;
        /// <summary>ObjectType ID:9 Name:"Document collection" / "Document collections"</summary>
        public static readonly string OTDocumentcollectionsName = "Document collections";

        /// <summary>ObjectType ID:10 Name:"Assignment" / "Assignments"</summary>
        public static readonly int OTAssignmentsID = 10;
        /// <summary>ObjectType ID:10 Name:"Assignment" / "Assignments"</summary>
        public static readonly string OTAssignmentsName = "Assignments";

        /// <summary>ObjectType ID:15 Name:"Report" / "Reports"</summary>
        public static readonly int OTReportsID = 15;
        /// <summary>ObjectType ID:15 Name:"Report" / "Reports"</summary>
        public static readonly string OTReportsName = "Reports";


        // -----------------------------------------------------------------------------------------------------------------------------------------
        // practical-m-files Classes

        /// <summary>Class "Assignment" (ID:-100, OT:10), Aliases:(none)</summary>
        public static readonly int CLAssignmentID = -100;
        /// <summary>Class "Assignment" (ID:-100, OT:10), Aliases:(none)</summary>
        public static readonly string CLAssignmentName = "Assignment";
        /// <summary>Class "Assignment" (ID:-100, OT:10), Aliases:(none)</summary>
        public static readonly int CLAssignmentObjectType = 10;
        /// <summary>Class "Assignment" (ID:-100, OT:10), Aliases:(none)</summary>
        public static readonly int CLAssignmentNamePropertyDef = 0;

        /// <summary>Class "Document" (ID:0, OT:0), Aliases:(none)</summary>
        public static readonly int CLDocumentID = 0;
        /// <summary>Class "Document" (ID:0, OT:0), Aliases:(none)</summary>
        public static readonly string CLDocumentName = "Document";
        /// <summary>Class "Document" (ID:0, OT:0), Aliases:(none)</summary>
        public static readonly int CLDocumentObjectType = 0;
        /// <summary>Class "Document" (ID:0, OT:0), Aliases:(none)</summary>
        public static readonly int CLDocumentNamePropertyDef = 0;

        /// <summary>Class "Other document" (ID:1, OT:0), Aliases:(none)</summary>
        public static readonly int CLOtherdocumentID = 1;
        /// <summary>Class "Other document" (ID:1, OT:0), Aliases:(none)</summary>
        public static readonly string CLOtherdocumentName = "Other document";
        /// <summary>Class "Other document" (ID:1, OT:0), Aliases:(none)</summary>
        public static readonly int CLOtherdocumentObjectType = 0;
        /// <summary>Class "Other document" (ID:1, OT:0), Aliases:(none)</summary>
        public static readonly int CLOtherdocumentNamePropertyDef = 0;

        /// <summary>Class "Report" (ID:-101, OT:15), Aliases:(none)</summary>
        public static readonly int CLReportID = -101;
        /// <summary>Class "Report" (ID:-101, OT:15), Aliases:(none)</summary>
        public static readonly string CLReportName = "Report";
        /// <summary>Class "Report" (ID:-101, OT:15), Aliases:(none)</summary>
        public static readonly int CLReportObjectType = 15;
        /// <summary>Class "Report" (ID:-101, OT:15), Aliases:(none)</summary>
        public static readonly int CLReportNamePropertyDef = 0;



    }
}



```