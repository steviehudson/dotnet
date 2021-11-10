using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestingTest;

namespace UrlShortener.Tests.HelpersTests
{
    [TestClass]
    public class CacheHelperTests : TestBase
    {
        [ClassInitialize()]
        public static void ClassInitialization(TestContext tc)
        {
            //TODO: Initialization for all tests in class
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
        public void Test()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
