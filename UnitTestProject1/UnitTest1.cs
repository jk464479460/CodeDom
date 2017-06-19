using ConsoleApplication7;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        IUtility _Utility = new Utility();

        [TestMethod]
        public void TestGenerateCSCode()
        {
            _Utility.FileSourcePath = ConfigurationManager.AppSettings["filePath"];
            var propertiesinFile = _Utility.ReadPropertiesFromSource();
            //var properties = _Utility.ProcessPropertyType(propertiesinFile);
            var dynamic = new DynamicCode();
            dynamic.GenerateCSCode(propertiesinFile);
        }
    }
}
