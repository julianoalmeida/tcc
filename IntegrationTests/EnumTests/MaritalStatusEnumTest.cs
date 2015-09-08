using System;
using Entidades.Enums;
using Entidades.Extensions;

namespace IntegrationTests.EnumTests
{
    public class MaritalStatusEnumTest : BaseTests
    {
        public sealed override string FileName { get; set; }
        public sealed override object ContentFile { get; set; }

        public MaritalStatusEnumTest()
        {
            FileName = "MaritalStatusEnum";
            ContentFile = MaritalStatusEnum.Casado.GetEnumDescriptions();
        }
        protected override void CreateJsonFile()
        {
            RecordFile();
        }

        protected override void CompareJsonFile()
        {
            RecordFile();
        }
    }
}
