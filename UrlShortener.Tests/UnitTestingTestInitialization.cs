using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestingTest
{
    [TestClass]
    public class UnitTestingTestInitialization
    {
        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext tc)
        {
            //TODO: Initialise for all tests within an assembly
            tc.WriteLine("In AssemblyInitialize() method");
        }
        
        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            //TODO: Clean up after all
            // tests in an assembly
        }
    }
}
