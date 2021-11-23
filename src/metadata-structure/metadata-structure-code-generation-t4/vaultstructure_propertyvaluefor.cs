﻿
using System;
using MFilesAPI;

// This M-Files vault structure file was generated at 2021-11-23 16:37:37 by a T4 Text Template that read the following vault:
// Vault name:   practical-m-files
// Vault ID:     {316EE0B8-8752-40CB-A03D-22B9F647B521}
// Vault server: localhost:2266
//
// If this works, it was generated by a T4 tool by Victor Vogelpoel (victor@victorvogelpoel.nl).
// If it doesn't, I don't know who wrote it.

namespace Dramatic.Conversational.MFilesAPI
{
    public static class PropertyValueFor
    {
        // --------------------------------------------------------------------------------------------------------------------------------------------------
        // Building blocks
        public static PropertyValue TextPropDef(MFBuiltInPropertyDef propDefID, string value) => TextPropDef((int)propDefID, value);
        public static PropertyValue TextPropDef(int propDefID, string value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeText, value);
            return pv;
        }

        public static PropertyValue MultiLineTextPropDef(MFBuiltInPropertyDef propDefID, string value) => MultiLineTextPropDef((int)propDefID, value);
        public static PropertyValue MultiLineTextPropDef(int propDefID, string value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeMultiLineText, value);
            return pv;
        }

        public static PropertyValue IntegerPropDef(MFBuiltInPropertyDef propDefID, int value) => IntegerPropDef((int)propDefID, value);
        public static PropertyValue IntegerPropDef(int propDefID, int value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeInteger, value);
            return pv;
        }

        public static PropertyValue Integer64PropDef(MFBuiltInPropertyDef propDefID, Int64 value) => Integer64PropDef((int)propDefID, value);
        public static PropertyValue Integer64PropDef(int propDefID, Int64 value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeInteger64, value);
            return pv;
        }

        public static PropertyValue BooleanPropDef(MFBuiltInPropertyDef propDefID, bool value) => BooleanPropDef((int)propDefID, value);
        public static PropertyValue BooleanPropDef(int propDefID, bool value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeBoolean, value);
            return pv;
        }

        public static PropertyValue DatePropDef(MFBuiltInPropertyDef propDefID, DateTime value) => DatePropDef((int)propDefID, value);
        public static PropertyValue DatePropDef(int propDefID, DateTime value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeDate, value);
            return pv;
        }

        public static PropertyValue TimePropDef(MFBuiltInPropertyDef propDefID, DateTime value) => TimePropDef((int)propDefID, value);
        public static PropertyValue TimePropDef(int propDefID, DateTime value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeTime, value);
            return pv;
        }

        public static PropertyValue TimestampPropDef(MFBuiltInPropertyDef propDefID, DateTime value) => TimestampPropDef((int)propDefID, value);
        public static PropertyValue TimestampPropDef(int propDefID, DateTime value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeTimestamp, value);
            return pv;

        }

        public static PropertyValue LookupPropDef(MFBuiltInPropertyDef propDefID, int value) => LookupPropDef((int)propDefID, value);
        public static PropertyValue LookupPropDef(int propDefID, int value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValue(MFDataType.MFDatatypeLookup, value);
            return pv;
        }

        public static PropertyValue LookupPropDef(int propDefID, Lookup value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValueToLookup(value);
            return pv;
        }

        public static PropertyValue MultiSelectLookupPropDef(MFBuiltInPropertyDef propDefID, Lookups value) => MultiSelectLookupPropDef((int)propDefID, value);
        public static PropertyValue MultiSelectLookupPropDef(int propDefID, Lookups value)
        {
            var pv = new PropertyValue { PropertyDef = propDefID };
            pv.TypedValue.SetValueToMultiSelectLookup(value);
            return pv;
        }

        /// <summary>Construct a PropertyValue for "Owner (Class)" (ID:1001), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerClass(Lookup pdownerClass) => LookupPropDef(1001, pdownerClass);

        /// <summary>Construct a PropertyValue for "Owner (Class group)" (ID:1002), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerClassgroup(Lookup pdownerClassgroup) => LookupPropDef(1002, pdownerClassgroup);

        /// <summary>Construct a PropertyValue for "Owner (User)" (ID:1003), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerUser(Lookup pdownerUser) => LookupPropDef(1003, pdownerUser);

        /// <summary>Construct a PropertyValue for "Owner (User group)" (ID:1004), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerUsergroup(Lookup pdownerUsergroup) => LookupPropDef(1004, pdownerUsergroup);

        /// <summary>Construct a PropertyValue for "Owner (Version label)" (ID:1005), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerVersionlabel(Lookup pdownerVersionlabel) => LookupPropDef(1005, pdownerVersionlabel);

        /// <summary>Construct a PropertyValue for "Owner (Traditional folder)" (ID:1006), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerTraditionalfolder(Lookup pdownerTraditionalfolder) => LookupPropDef(1006, pdownerTraditionalfolder);

        /// <summary>Construct a PropertyValue for "Owner (External source)" (ID:1007), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerExternalsource(Lookup pdownerExternalsource) => LookupPropDef(1007, pdownerExternalsource);

        /// <summary>Construct a PropertyValue for "Owner (Workflow)" (ID:1008), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerWorkflow(Lookup pdownerWorkflow) => LookupPropDef(1008, pdownerWorkflow);

        /// <summary>Construct a PropertyValue for "Owner (State)" (ID:1009), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerState(Lookup pdownerState) => LookupPropDef(1009, pdownerState);

        /// <summary>Construct a PropertyValue for "Owner (State transition)" (ID:1010), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerStatetransition(Lookup pdownerStatetransition) => LookupPropDef(1010, pdownerStatetransition);

        /// <summary>Construct a PropertyValue for "Owner (Source)" (ID:1011), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerSource(Lookup pdownerSource) => LookupPropDef(1011, pdownerSource);

        /// <summary>Construct a PropertyValue for "Owner (Document)" (ID:1012), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerDocument(Lookup pdownerDocument) => LookupPropDef(1012, pdownerDocument);

        /// <summary>Construct a PropertyValue for "Document" (ID:1013), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDDocument(Lookups pddocument) => MultiSelectLookupPropDef(1013, pddocument);

        /// <summary>Construct a PropertyValue for "Owner (Document collection)" (ID:1014), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerDocumentcollection(Lookup pdownerDocumentcollection) => LookupPropDef(1014, pdownerDocumentcollection);

        /// <summary>Construct a PropertyValue for "Document collection" (ID:1015), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDDocumentcollection(Lookups pddocumentcollection) => MultiSelectLookupPropDef(1015, pddocumentcollection);

        /// <summary>Construct a PropertyValue for "Owner (Assignment)" (ID:1016), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerAssignment(Lookup pdownerAssignment) => LookupPropDef(1016, pdownerAssignment);

        /// <summary>Construct a PropertyValue for "Assignment" (ID:1017), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDAssignment(Lookups pdassignment) => MultiSelectLookupPropDef(1017, pdassignment);

        /// <summary>Construct a PropertyValue for "Owner (Report)" (ID:1018), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDOwnerReport(Lookup pdownerReport) => LookupPropDef(1018, pdownerReport);

        /// <summary>Construct a PropertyValue for "Report" (ID:1019), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDReport(Lookups pdreport) => MultiSelectLookupPropDef(1019, pdreport);

        /// <summary>Construct a PropertyValue for "Name or title" (ID:0), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDNameortitle(string pdnameortitle) => TextPropDef(0, pdnameortitle);

        /// <summary>Construct a PropertyValue for "Created" (ID:20), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDCreated(DateTime pdcreated) => TimestampPropDef(20, pdcreated);

        /// <summary>Construct a PropertyValue for "Created by" (ID:25), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDCreatedby(Lookup pdcreatedby) => LookupPropDef(25, pdcreatedby);

        /// <summary>Construct a PropertyValue for "Last modified" (ID:21), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDLastmodified(DateTime pdlastmodified) => TimestampPropDef(21, pdlastmodified);

        /// <summary>Construct a PropertyValue for "Last modified by" (ID:23), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDLastmodifiedby(Lookup pdlastmodifiedby) => LookupPropDef(23, pdlastmodifiedby);

        /// <summary>Construct a PropertyValue for "Status changed" (ID:24), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDStatuschanged(DateTime pdstatuschanged) => TimestampPropDef(24, pdstatuschanged);

        /// <summary>Construct a PropertyValue for "Single file" (ID:22), Type:Boolean, Aliases:(none)</summary>
        public static PropertyValue PDSinglefile(bool pdsinglefile) => BooleanPropDef(22, pdsinglefile);

        /// <summary>Construct a PropertyValue for "Class" (ID:100), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDClass(Lookup pdclass) => LookupPropDef(100, pdclass);

        /// <summary>Construct a PropertyValue for "Class groups" (ID:101), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDClassgroups(Lookups pdclassgroups) => MultiSelectLookupPropDef(101, pdclassgroups);

        /// <summary>Construct a PropertyValue for "Deleted" (ID:27), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDDeleted(DateTime pddeleted) => TimestampPropDef(27, pddeleted);

        /// <summary>Construct a PropertyValue for "Deleted by" (ID:28), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDDeletedby(Lookup pddeletedby) => LookupPropDef(28, pddeletedby);

        /// <summary>Construct a PropertyValue for "Version label" (ID:29), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDVersionlabel(Lookups pdversionlabel) => MultiSelectLookupPropDef(29, pdversionlabel);

        /// <summary>Construct a PropertyValue for "Size on server (this version)" (ID:30), Type:Integer, Aliases:(none)</summary>
        public static PropertyValue PDSizeonserverthisversion(int pdsizeonserverthisversion) => IntegerPropDef(30, pdsizeonserverthisversion);

        /// <summary>Construct a PropertyValue for "Size on server (all versions)" (ID:31), Type:Integer, Aliases:(none)</summary>
        public static PropertyValue PDSizeonserverallversions(int pdsizeonserverallversions) => IntegerPropDef(31, pdsizeonserverallversions);

        /// <summary>Construct a PropertyValue for "Marked for archiving" (ID:32), Type:Boolean, Aliases:(none)</summary>
        public static PropertyValue PDMarkedforarchiving(bool pdmarkedforarchiving) => BooleanPropDef(32, pdmarkedforarchiving);

        /// <summary>Construct a PropertyValue for "Comment" (ID:33), Type:MultiLineText, Aliases:(none)</summary>
        public static PropertyValue PDComment(string pdcomment) => MultiLineTextPropDef(33, pdcomment);

        /// <summary>Construct a PropertyValue for "Traditional folder" (ID:34), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDTraditionalfolder(Lookups pdtraditionalfolder) => MultiSelectLookupPropDef(34, pdtraditionalfolder);

        /// <summary>Construct a PropertyValue for "Created from external source" (ID:35), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDCreatedfromexternalsource(Lookup pdcreatedfromexternalsource) => LookupPropDef(35, pdcreatedfromexternalsource);

        /// <summary>Construct a PropertyValue for "Is template" (ID:37), Type:Boolean, Aliases:(none)</summary>
        public static PropertyValue PDIstemplate(bool pdistemplate) => BooleanPropDef(37, pdistemplate);

        /// <summary>Construct a PropertyValue for "Additional classes" (ID:36), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDAdditionalclasses(Lookups pdadditionalclasses) => MultiSelectLookupPropDef(36, pdadditionalclasses);

        /// <summary>Construct a PropertyValue for "Workflow" (ID:38), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDWorkflow(Lookup pdworkflow) => LookupPropDef(38, pdworkflow);

        /// <summary>Construct a PropertyValue for "State" (ID:39), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDState(Lookup pdstate) => LookupPropDef(39, pdstate);

        /// <summary>Construct a PropertyValue for "Moved into current state" (ID:40), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDMovedintocurrentstate(DateTime pdmovedintocurrentstate) => TimestampPropDef(40, pdmovedintocurrentstate);

        /// <summary>Construct a PropertyValue for "Assignment description" (ID:41), Type:MultiLineText, Aliases:(none)</summary>
        public static PropertyValue PDAssignmentdescription(string pdassignmentdescription) => MultiLineTextPropDef(41, pdassignmentdescription);

        /// <summary>Construct a PropertyValue for "Deadline" (ID:42), Type:Date, Aliases:(none)</summary>
        public static PropertyValue PDDeadline(DateTime pddeadline) => DatePropDef(42, pddeadline);

        /// <summary>Construct a PropertyValue for "Monitored by" (ID:43), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDMonitoredby(Lookups pdmonitoredby) => MultiSelectLookupPropDef(43, pdmonitoredby);

        /// <summary>Construct a PropertyValue for "Assigned to" (ID:44), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDAssignedto(Lookups pdassignedto) => MultiSelectLookupPropDef(44, pdassignedto);

        /// <summary>Construct a PropertyValue for "Marked as complete by" (ID:45), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDMarkedascompleteby(Lookups pdmarkedascompleteby) => MultiSelectLookupPropDef(45, pdmarkedascompleteby);

        /// <summary>Construct a PropertyValue for "Collection members (documents)" (ID:46), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDCollectionmembersdocuments(Lookups pdcollectionmembersdocuments) => MultiSelectLookupPropDef(46, pdcollectionmembersdocuments);

        /// <summary>Construct a PropertyValue for "Collection members (document collections)" (ID:47), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDCollectionmembersdocumentcollections(Lookups pdcollectionmembersdocumentcollections) => MultiSelectLookupPropDef(47, pdcollectionmembersdocumentcollections);

        /// <summary>Construct a PropertyValue for "Original path (1/3)" (ID:75), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDOriginalpath13(string pdoriginalpath13) => TextPropDef(75, pdoriginalpath13);

        /// <summary>Construct a PropertyValue for "Original path (2/3)" (ID:77), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDOriginalpath23(string pdoriginalpath23) => TextPropDef(77, pdoriginalpath23);

        /// <summary>Construct a PropertyValue for "Original path (3/3)" (ID:78), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDOriginalpath33(string pdoriginalpath33) => TextPropDef(78, pdoriginalpath33);

        /// <summary>Construct a PropertyValue for "Reference" (ID:76), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDReference(Lookups pdreference) => MultiSelectLookupPropDef(76, pdreference);

        /// <summary>Construct a PropertyValue for "Workflow Assignment" (ID:79), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDWorkflowAssignment(Lookups pdworkflowAssignment) => MultiSelectLookupPropDef(79, pdworkflowAssignment);

        /// <summary>Construct a PropertyValue for "Accessed by me" (ID:81), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDAccessedbyme(DateTime pdaccessedbyme) => TimestampPropDef(81, pdaccessedbyme);

        /// <summary>Construct a PropertyValue for "Favorite view" (ID:82), Type:Integer, Aliases:(none)</summary>
        public static PropertyValue PDFavoriteview(int pdfavoriteview) => IntegerPropDef(82, pdfavoriteview);

        /// <summary>Construct a PropertyValue for "Message ID" (ID:83), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDMessageID(string pdmessageID) => TextPropDef(83, pdmessageID);

        /// <summary>Construct a PropertyValue for "Reply to (ID)" (ID:84), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDReplytoID(string pdreplytoID) => TextPropDef(84, pdreplytoID);

        /// <summary>Construct a PropertyValue for "Reply to" (ID:85), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDReplyto(Lookups pdreplyto) => MultiSelectLookupPropDef(85, pdreplyto);

        /// <summary>Construct a PropertyValue for "Signature manifestation" (ID:86), Type:MultiLineText, Aliases:(none)</summary>
        public static PropertyValue PDSignaturemanifestation(string pdsignaturemanifestation) => MultiLineTextPropDef(86, pdsignaturemanifestation);

        /// <summary>Construct a PropertyValue for "Report URL" (ID:87), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDReportURL(string pdreportURL) => TextPropDef(87, pdreportURL);

        /// <summary>Construct a PropertyValue for "Report placement" (ID:88), Type:Integer, Aliases:(none)</summary>
        public static PropertyValue PDReportplacement(int pdreportplacement) => IntegerPropDef(88, pdreportplacement);

        /// <summary>Construct a PropertyValue for "Object changed" (ID:89), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDObjectchanged(DateTime pdobjectchanged) => TimestampPropDef(89, pdobjectchanged);

        /// <summary>Construct a PropertyValue for "Permissions changed" (ID:90), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDPermissionschanged(DateTime pdpermissionschanged) => TimestampPropDef(90, pdpermissionschanged);

        /// <summary>Construct a PropertyValue for "Version label changed" (ID:91), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDVersionlabelchanged(DateTime pdversionlabelchanged) => TimestampPropDef(91, pdversionlabelchanged);

        /// <summary>Construct a PropertyValue for "Version comment changed" (ID:92), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDVersioncommentchanged(DateTime pdversioncommentchanged) => TimestampPropDef(92, pdversioncommentchanged);

        /// <summary>Construct a PropertyValue for "Deletion status changed" (ID:93), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDDeletionstatuschanged(DateTime pddeletionstatuschanged) => TimestampPropDef(93, pddeletionstatuschanged);

        /// <summary>Construct a PropertyValue for "Conflict resolved" (ID:96), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDConflictresolved(DateTime pdconflictresolved) => TimestampPropDef(96, pdconflictresolved);

        /// <summary>Construct a PropertyValue for "Remote vault GUID" (ID:94), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDRemotevaultGUID(string pdremotevaultGUID) => TextPropDef(94, pdremotevaultGUID);

        /// <summary>Construct a PropertyValue for "Shared files" (ID:95), Type:MultiLineText, Aliases:(none)</summary>
        public static PropertyValue PDSharedfiles(string pdsharedfiles) => MultiLineTextPropDef(95, pdsharedfiles);

        /// <summary>Construct a PropertyValue for "Completed" (ID:98), Type:Boolean, Aliases:(none)</summary>
        public static PropertyValue PDCompleted(bool pdcompleted) => BooleanPropDef(98, pdcompleted);

        /// <summary>Construct a PropertyValue for "Marked as rejected by" (ID:97), Type:MultiSelectLookup, Aliases:(none)</summary>
        public static PropertyValue PDMarkedasrejectedby(Lookups pdmarkedasrejectedby) => MultiSelectLookupPropDef(97, pdmarkedasrejectedby);

        /// <summary>Construct a PropertyValue for "State transition" (ID:99), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDStatetransition(Lookup pdstatetransition) => LookupPropDef(99, pdstatetransition);

        /// <summary>Construct a PropertyValue for "Repository" (ID:102), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDRepository(Lookup pdrepository) => LookupPropDef(102, pdrepository);

        /// <summary>Construct a PropertyValue for "Location" (ID:103), Type:Lookup, Aliases:(none)</summary>
        public static PropertyValue PDLocation(Lookup pdlocation) => LookupPropDef(103, pdlocation);

        /// <summary>Construct a PropertyValue for "Incomplete metadata" (ID:104), Type:Boolean, Aliases:(none)</summary>
        public static PropertyValue PDIncompletemetadata(bool pdincompletemetadata) => BooleanPropDef(104, pdincompletemetadata);

        /// <summary>Construct a PropertyValue for "Object changed for export" (ID:105), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDObjectchangedforexport(DateTime pdobjectchangedforexport) => TimestampPropDef(105, pdobjectchangedforexport);

        /// <summary>Construct a PropertyValue for "Object version changed for export" (ID:106), Type:Timestamp, Aliases:(none)</summary>
        public static PropertyValue PDObjectversionchangedforexport(DateTime pdobjectversionchangedforexport) => TimestampPropDef(106, pdobjectversionchangedforexport);

        /// <summary>Construct a PropertyValue for "Conflicted version" (ID:107), Type:Integer, Aliases:(none)</summary>
        public static PropertyValue PDConflictedversion(int pdconflictedversion) => IntegerPropDef(107, pdconflictedversion);

        /// <summary>Construct a PropertyValue for "Keywords" (ID:26), Type:Text, Aliases:(none)</summary>
        public static PropertyValue PDKeywords(string pdkeywords) => TextPropDef(26, pdkeywords);


    }
}
