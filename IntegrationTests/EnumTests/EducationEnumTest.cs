using Entidades.Enums;
using Entidades.Extensions;

namespace IntegrationTests.EnumTests
{
    public class EducationEnumTest : BaseTests
    {
        public sealed override string FileName { get; set; }
        public sealed override object ContentFile { get; set; }

        public EducationEnumTest()
        {
            FileName = "EducationEnum";
            ContentFile = EducationEnum.EnsinoMedio.GetEnumDescriptions();
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