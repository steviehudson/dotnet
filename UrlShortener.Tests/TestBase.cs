using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace UnitTestingTest
{
    public class TestBase
    {
        public TestContext TestContext { get; set; }
        
        protected void WriteDescription(Type typ)
        {
            string testName = TestContext.TestName;

            MemberInfo method = typ.GetMethod(testName);
            if (method != null)
            {
                Attribute attr = method.GetCustomAttribute(typeof(DescriptionAttribute));
                if (attr != null)
                {
                    DescriptionAttribute dattr = (DescriptionAttribute)attr;
                    TestContext.WriteLine("Test Description: " + dattr.Description);
                }
            }
        }

    }
}
