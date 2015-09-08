using Entidades.Enums;
using Entidades.Extensions;

namespace IntegrationTests.EnumTests
{
    public class SexEnumTest : BaseTests
    {
        public override string FileName { get; set; }
        public override object ContentFile { get; set; }

        public SexEnumTest()
        {
            FileName = "SexEnum";
            ContentFile = SexEnum.Feminino.GetEnumDescriptions();
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