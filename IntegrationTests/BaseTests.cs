using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTests
{
    public class BaseTests
    {
        protected string BuildJsonWith()
        {
            return JsonConvert.SerializeObject("test");
        }

        protected void CompareObjects(string recordedFile, string actualFile)
        {
            Assert.That(recordedFile, Is.EqualTo(actualFile));
        }

        protected string GetFileByName(string fileName)
        {
            return "";
        }


    }
}
