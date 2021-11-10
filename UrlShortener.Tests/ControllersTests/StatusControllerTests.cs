using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTestingTest;
using UrlShortener.Controllers;

namespace UrlShortener.Tests.ControllersTests
{
    [TestClass]
    public class StatusControllerTests : TestBase
    {
        [ClassInitialize()]
        public static void ClassInitialization(TestContext tc)
        {
            tc.WriteLine("In ClassInitialization() method");
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            //TODO: Clean up after all tests in class
        }

        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.WriteLine("In TestInitialize() method");

            WriteDescription(GetType());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine("In TestCleanup() method");
        }

        [TestMethod]
        public void ReturnsOk()
        {
            //Arrange
            var logger = Mock.Of<ILogger<StatusController>>();
            var sut = new StatusController(logger);

            //Act
            var status = sut.Get();

            //Assert
            Assert.AreEqual("OK", status);
        }
    }
}
