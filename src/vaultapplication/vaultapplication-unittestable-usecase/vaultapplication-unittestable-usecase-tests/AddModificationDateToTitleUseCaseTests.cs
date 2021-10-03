using System;
using FluentAssertions;
using MFilesAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VaultApplicationUnittestableUseCase.Infrastructure;
using VaultApplicationUnittestableUseCase.UseCases;

namespace vaultapplication_unittestable_usecase_tests
{

    [TestClass]
    public class AddModificationDateToTitleUseCaseTests
    {
        [DataTestMethod]
        // objectTitle,                 expectedUpdatedTitle
        [DataRow("A document title",    "A document title 2021-09-27")]
        [DataRow("A",                   "A 2021-09-27")]
        [DataRow("AB",                  "AB 2021-09-27")]
        [DataRow("ABC",                 "ABC 2021-09-27")]
        [DataRow("ABCD",                "ABCD 2021-09-27")]
        [DataRow("ABCDE",               "ABCDE 2021-09-27")]
        [DataRow("ABCDEF",              "ABCDEF 2021-09-27")]
        [DataRow("ABCDEFG",             "ABCDEFG 2021-09-27")]
        [DataRow("ABCDEFGH",            "ABCDEFGH 2021-09-27")]
        [DataRow("ABCDEFGHI",           "ABCDEFGHI 2021-09-27")]
        public void WhenTitleHasNoModifiedDate_ExpectLastModifiedDateAppendedToTitle(string objectTitle, string expectedUpdatedTitle)
        {
            var lastModifiedUTC         = DateTime.Parse("2021-09-27 21:15:10").ToUniversalTime();
            var userId                  = 1;

            var objectTitlePV = new PropertyValueClass { PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle };
            objectTitlePV.Value.SetValue(MFDataType.MFDatatypeText, objectTitle);

            var lastModifiedPV = new PropertyValueClass { PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefLastModified };
            lastModifiedPV.Value.SetValue(MFDataType.MFDatatypeDate, lastModifiedUTC);

            var updatedTitlePV = new PropertyValueClass { PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle };
            updatedTitlePV.Value.SetValue(MFDataType.MFDatatypeText, expectedUpdatedTitle);

            var lastModifiedByValue = new TypedValueClass();
            lastModifiedByValue.SetValue(MFDataType.MFDatatypeLookup, userId);


            var vaultMethodsMock = new Mock<IVaultMethods>();
            vaultMethodsMock.Setup(m=>m.ObjectPropertyOperationsGetProperty(It.IsAny<ObjVer>(), (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle)).Returns(objectTitlePV);
            vaultMethodsMock.Setup(m=>m.ObjectPropertyOperationsGetProperty(It.IsAny<ObjVer>(), (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefLastModified)).Returns(lastModifiedPV);
            vaultMethodsMock.Setup(m=>m.ObjectPropertyOperationsSetProperty(It.IsAny<ObjVer>(), It.IsAny<PropertyValueClass>())).Returns(It.IsAny<ObjectVersionAndPropertiesClass>);
            vaultMethodsMock.Setup(m=>m.ObjectPropertyOperationsSetLastModificationInfoAdmin(It.IsAny<ObjVer>(), true, lastModifiedByValue, false, null)).Returns(It.IsAny<ObjectVersionAndPropertiesClass>);

            var useCase = new AddModificationDateToTitleUseCase(vaultMethodsMock.Object);
            var objVer  = new ObjVerClass();

            // ACTION
            useCase.UpdateObjectTitle(objVer, userId);

            // ASSERT
            //vaultMethodsMock.VerifyAll();
            vaultMethodsMock.Verify(m=>m.ObjectPropertyOperationsGetProperty(objVer, (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle), Times.Once);
            vaultMethodsMock.Verify(m=>m.ObjectPropertyOperationsGetProperty(objVer, (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefLastModified), Times.Once);
            vaultMethodsMock.Verify(m=>m.ObjectPropertyOperationsSetProperty(objVer, updatedTitlePV), Times.Once);
            vaultMethodsMock.Verify(m=>m.ObjectPropertyOperationsSetLastModificationInfoAdmin(objVer, true, lastModifiedByValue, false, null), Times.Once);

        }
    }
}
