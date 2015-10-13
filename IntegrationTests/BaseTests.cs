using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public abstract class BaseTests
    {
        public abstract string FileName { get; set; }
        public abstract object ContentFile { get; set; }

        protected abstract void CreateJsonFile();
        protected abstract void CompareJsonFile();

        private string _baseFolderPath = "~/json-files/";

        [SetUp]
        public void Setup()
        {
            CreateDirectoryIfDoesntExist(_baseFolderPath);
            _baseFolderPath = _baseFolderPath + FileName + ".json";
        }

        private static void CreateDirectoryIfDoesntExist(string folderName)
        {
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
        }

        [Test]
        [Ignore]
        public void RecordFile()
        {
            SaveOrUpdateJsonFileWith(SerializeObject(ContentFile));
        }

        [Test]
        public void CompareRecordedFileAndSerializedResult()
        {
            var fullPath = Path.GetFullPath(_baseFolderPath);
            var recordedFile = File.ReadAllLines(fullPath).First();

            if (string.IsNullOrEmpty(recordedFile))
                throw new FileNotFoundException("file not found , please record the file before check it's integrity");

            Assert.That(SerializeObject(ContentFile), Is.EqualTo(recordedFile));
        }

        private void SaveOrUpdateJsonFileWith(string contentFile)
        {
            if (!File.Exists(_baseFolderPath))
                CreateJsonFileWith(contentFile);
            else
                DeleteAndRecriateJsonFileWith(contentFile);
        }

        private static string SerializeObject(object content)
        {
            return JsonConvert.SerializeObject(content);
        }

        private void DeleteAndRecriateJsonFileWith(string content)
        {
            File.Delete(_baseFolderPath);
            CreateJsonFileWith(content);
        }

        private void CreateJsonFileWith(string content)
        {
            File.Create(_baseFolderPath).Dispose();
            WriteTextWith(content);
        }

        private void WriteTextWith(string content)
        {
            var textWriter = new StreamWriter(_baseFolderPath);
            textWriter.WriteLine(content);
            textWriter.Close();
        }
    }
}
