using Entidades.Extensions;
using System.Collections.Generic;

namespace IntegrationTests.ExtensionTests
{
    public class EnumerableExtensionTest : BaseTests
    {
        public sealed override string FileName { get; set; }
        public sealed override object ContentFile { get; set; }

        private readonly IEnumerable<string> _list = new List<string> { "item1", "item2", "item3",
                                                                        "item4", "item5", "item6" };
        public EnumerableExtensionTest()
        {
            FileName = "EnumerableExtension";
            ContentFile = _list.BuildStringFormatedTextFromListWithCollonSplitingElements();
        }

        protected override void CreateJsonFile()
        {
            RecordFile();
        }

        protected override void CompareJsonFile()
        {
            CompareRecordedFileAndSerializedResult();
        }
    }
}