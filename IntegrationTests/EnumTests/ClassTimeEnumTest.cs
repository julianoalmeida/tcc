using Entidades.Enums;
using Entidades.Extensions;

namespace IntegrationTests.EnumTests
{
    public class ClassTimeEnumTest : BaseTests
    {
        public override sealed string FileName { get; set; }
        public override sealed object ContentFile { get; set; }

        public ClassTimeEnumTest()
        {
            FileName = "ClassTimeEnum";
            ContentFile = ClassesTimeEnum.Manha.GetEnumDescriptions();
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