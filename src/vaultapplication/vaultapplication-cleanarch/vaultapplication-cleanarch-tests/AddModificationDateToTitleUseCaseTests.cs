using System;
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
        [DataRow("A document title")]
        [DataRow("A")]
        [DataRow("AB")]
        [DataRow("ABC")]
        [DataRow("ABCD")]
        [DataRow("ABCDE")]
        [DataRow("ABCDEF")]
        [DataRow("ABCDEFG")]
        [DataRow("ABCDEFGH")]
        [DataRow("ABCDEFGHI")]
        public void WhenTitleHasNoModifiedDate_ExpectLastModifiedDateAppendedToTitle(string objectTitle)
        {
            var lastModified            = DateTime.Now;
            var lastModifiedUTC         = lastModified.ToUniversalTime();
            var userId                  = 1;

            var expectedUpdatedTitle    = $"{objectTitle} {lastModified:yyyy-MM-dd}";

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
    }
}
