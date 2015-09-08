using Entidades.Enums;
using Entidades.Extensions;

namespace IntegrationTests.EnumTests
{
    public class AccessProfileEnumTest : BaseTests
    {
        public sealed override string FileName { get; set; }
        public sealed override object ContentFile { get; set; }

        public AccessProfileEnumTest()
        {
            FileName = "AccessProfileEnum";
            ContentFile = AccessProfileEnum.Discente.GetEnumDescriptions();
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