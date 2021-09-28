using System;
using FluentAssertions;
using MFilesAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VaultApplicationCleanArchitecture.Application;
using VaultApplicationCleanArchitecture.Application.Interfaces;

namespace vaultapplication_cleanarch_tests
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
            //var lastModified            = DateTime.Now;
            //var lastModifiedUTC         = lastModified.ToUniversalTime();
            var lastModifiedUTC         = DateTime.Parse("2021-09-27 21:15:10").ToUniversalTime();
            var userId                  = 1;

            //var expectedUpdatedTitle    = $"{objectTitle} {lastModified:yyyy-MM-dd}";

            var objectRepositoryMock = new Mock<IObjectRepository>();
            objectRepositoryMock.Setup(m=>m.GetObjectTitle(It.IsAny<ObjVer>())).Returns(objectTitle);
            objectRepositoryMock.Setup(m=>m.GetObjectLastModified(It.IsAny<ObjVer>())).Returns(lastModifiedUTC);

            var useCase = new AddModificationDateToTitleUseCase(objectRepositoryMock.Object);
            var objVer  = new ObjVer();
            useCase.UpdateObjectTitle(objVer, userId);

            objectRepositoryMock.VerifyAll();
            objectRepositoryMock.Verify(m=>m.GetObjectTitle(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.GetObjectLastModified(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.UpdateObjectTitle(objVer, expectedUpdatedTitle, userId), Times.Once);
        }


        [TestMethod]
        public void WhenTitleHasPriorDate_ExpectItReplacedByLastModifiedsDate()
        {
            var objectTitle             = "A document title 1984-01-01"; // an old date
            var lastModified            = DateTime.Now;
            var lastModifiedUTC         = lastModified.ToUniversalTime();
            var userId                  = 1;

            var expectedUpdatedTitle    = $"A document title {lastModified:yyyy-MM-dd}";

            var objectRepositoryMock = new Mock<IObjectRepository>();
            objectRepositoryMock.Setup(m=>m.GetObjectTitle(It.IsAny<ObjVer>())).Returns(objectTitle);
            objectRepositoryMock.Setup(m=>m.GetObjectLastModified(It.IsAny<ObjVer>())).Returns(lastModifiedUTC);

            var useCase = new AddModificationDateToTitleUseCase(objectRepositoryMock.Object);
            var objVer  = new ObjVer();
            useCase.UpdateObjectTitle(objVer, userId);

            objectRepositoryMock.VerifyAll();
            objectRepositoryMock.Verify(m=>m.GetObjectTitle(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.GetObjectLastModified(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.UpdateObjectTitle(objVer, expectedUpdatedTitle, userId));
        }


        [TestMethod]
        public void WhenTitleHasPriorDateWithTrailingSpaces_ExpectItReplacedByLastModifiedsDate()
        {
            var objectTitle             = "A document title 1984-01-01 "; // an old date with trailing space
            var lastModified            = DateTime.Now;
            var lastModifiedUTC         = lastModified.ToUniversalTime();
            var userId                  = 1;

            var expectedUpdatedTitle    = $"A document title {lastModified:yyyy-MM-dd}";

            var objectRepositoryMock = new Mock<IObjectRepository>();
            objectRepositoryMock.Setup(m=>m.GetObjectTitle(It.IsAny<ObjVer>())).Returns(objectTitle);
            objectRepositoryMock.Setup(m=>m.GetObjectLastModified(It.IsAny<ObjVer>())).Returns(lastModifiedUTC);

            var useCase = new AddModificationDateToTitleUseCase(objectRepositoryMock.Object);
            var objVer  = new ObjVer();
            useCase.UpdateObjectTitle(objVer, userId);

            objectRepositoryMock.VerifyAll();
            objectRepositoryMock.Verify(m=>m.GetObjectTitle(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.GetObjectLastModified(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.UpdateObjectTitle(objVer, expectedUpdatedTitle, userId));
        }


        [TestMethod]
        public void WhenTitleHasLastModifiedsDate_ExpectNoChangeToDateInTitle()
        {
            var lastModified            = DateTime.Now;
            var objectTitle             = $"A document title {lastModified:yyyy-MM-dd}";
            var lastModifiedUTC         = lastModified.ToUniversalTime();
            var userId                  = 1;

            var objectRepositoryMock = new Mock<IObjectRepository>();
            objectRepositoryMock.Setup(m=>m.GetObjectTitle(It.IsAny<ObjVer>())).Returns(objectTitle);
            objectRepositoryMock.Setup(m=>m.GetObjectLastModified(It.IsAny<ObjVer>())).Returns(lastModifiedUTC);

            var useCase = new AddModificationDateToTitleUseCase(objectRepositoryMock.Object);
            var objVer  = new ObjVer();
            useCase.UpdateObjectTitle(objVer, userId);

            objectRepositoryMock.VerifyAll();
            objectRepositoryMock.Verify(m=>m.GetObjectTitle(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.GetObjectLastModified(objVer), Times.Once);
            objectRepositoryMock.Verify(m=>m.UpdateObjectTitle(It.IsAny<ObjVer>(), It.IsAny<string>(), It.IsAny<int>()), Times.Never);
        }


        // --------------------------------------------------------------------------------------------------
        // Tests with STUB

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
        public void WhenTitleHasNoModifiedDate_ExpectLastModifiedDateAppendedToTitle2(string objectTitle, string expectedUpdatedTitle)
        {
            var lastModifiedUTC         = DateTime.Parse("2021-09-27 21:15:10").ToUniversalTime();
            var objVer                  = new ObjVer();
            var userId                  = 1;

            var objectRepository        = new ObjectRepositoryStub();
            objectRepository.AddObjectStub(objVer, objectTitle, lastModifiedUTC);

            // ACTION
            var useCase = new AddModificationDateToTitleUseCase(objectRepository);
            useCase.UpdateObjectTitle(objVer, userId);

            // ASSERT
            objectRepository.GetObjectTitle(objVer).Should().Be(expectedUpdatedTitle);
        }


        [TestMethod]
        public void WhenTitleHasPriorDate_ExpectItReplacedByLastModifiedsDate2()
        {
            var objectTitle             = "A document title 1984-01-01"; // an old date
            var lastModified            = DateTime.Now;
            var lastModifiedUTC         = lastModified.ToUniversalTime();
            var objVer                  = new ObjVer();
            var userId                  = 1;

            var expectedUpdatedTitle    = $"A document title {lastModified:yyyy-MM-dd}";

            var objectRepository        = new ObjectRepositoryStub();
            objectRepository.AddObjectStub(objVer, objectTitle, lastModifiedUTC);

            var useCase = new AddModificationDateToTitleUseCase(objectRepository);
            useCase.UpdateObjectTitle(objVer, userId);

            // ASSERT
            objectRepository.GetObjectTitle(objVer).Should().Be(expectedUpdatedTitle);
        }


        [TestMethod]
        public void WhenTitleHasPriorDateWithTrailingSpaces_ExpectItReplacedByLastModifiedsDate2()
        {
            var objectTitle             = "A document title 1984-01-01 "; // an old date with trailing space
            var lastModified            = DateTime.Now;
            var lastModifiedUTC         = lastModified.ToUniversalTime();
            var objVer                  = new ObjVer();
            var userId                  = 1;

            var expectedUpdatedTitle    = $"A document title {lastModified:yyyy-MM-dd}";

            var objectRepository        = new ObjectRepositoryStub();
            objectRepository.AddObjectStub(objVer, objectTitle, lastModifiedUTC);

            var useCase = new AddModificationDateToTitleUseCase(objectRepository);
            useCase.UpdateObjectTitle(objVer, userId);

            // ASSERT
            objectRepository.GetObjectTitle(objVer).Should().Be(expectedUpdatedTitle);
        }


        [TestMethod]
        public void WhenTitleHasLastModifiedsDate_ExpectNoChangeToDateInTitle2()
        {
            var lastModified            = DateTime.Now;
            var objectTitle             = $"A document title {lastModified:yyyy-MM-dd}";
            var lastModifiedUTC         = lastModified.ToUniversalTime();
            var objVer  = new ObjVer();
            var userId                  = 1;

            var objectRepository        = new ObjectRepositoryStub();
            objectRepository.AddObjectStub(objVer, objectTitle, lastModifiedUTC);

            var useCase = new AddModificationDateToTitleUseCase(objectRepository);
            useCase.UpdateObjectTitle(objVer, userId);

            // ASSERT
            objectRepository.GetObjectTitle(objVer).Should().Be(objectTitle);
        }
    }
}
