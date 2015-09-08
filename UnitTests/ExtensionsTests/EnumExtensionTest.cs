using NUnit.Framework;
using Entidades.Enums;
using Entidades.Extensions;

namespace UnitTests.ExtensionsTests
{
    [TestFixture]
    public class EnumExtensionTest
    {
        [TestCase(EducationEnum.EnsinoFundamental, "Ensino Fundamental")]
        [TestCase(EducationEnum.EnsinoMedio, "Ensino Médio")]
        [TestCase(EducationEnum.EnsinoSuperior, "Ensino Superior")]
        [TestCase(EducationEnum.PosGraduacao, "Pós Graduação")]
        public void CanGetEnumDescription(EducationEnum educationEnum, string expectedEnumDecriptionText)
        {
            Assert.That(educationEnum.GetEnumDescription(), Is.EqualTo(expectedEnumDecriptionText));
        }

        [TestCase(ClassesTimeEnum.Manha, "Matutino")]
        [TestCase(ClassesTimeEnum.Tarde, "Vespertino")]
        [TestCase(ClassesTimeEnum.Noite, "Noturno")]
        public void CanGetEnumDescription(ClassesTimeEnum educationEnum, string expectedEnumDecriptionText)
        {
            Assert.That(educationEnum.GetEnumDescription(), Is.EqualTo(expectedEnumDecriptionText));
        }
    }
}
