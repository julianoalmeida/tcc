using Entidades.Extensions;
using NUnit.Framework;

namespace UnitTests.ExtensionsTests
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase(null, "")]

        [TestCase("(text)", "text")]
        [TestCase("555,555", "555555")]
        [TestCase("11/11/1111", "11111111")]
        [TestCase("000.000.000-00", "00000000000")]
        public void CanRemoveMaskCharacters(string valueWithMaskCharacters, string expectedText)
        {
            Assert.That(valueWithMaskCharacters.RemoveMaskCharacters(), Is.EqualTo(expectedText));
        }

        [TestCase(null, "")]
        [TestCase("téxt wíth accênts", "text with accents")]
        public void CanRemoveAccents(string valueWithAccents, string expectedText)
        {
            Assert.That(valueWithAccents.RemoveAccents(), Is.EqualTo(expectedText));
        }

        [TestCase(null, "")]
        [TestCase("       ", "")]
        [TestCase("       text", "text")]
        [TestCase("       text     ", "text")]
        [TestCase("    this is a text with no empty space     ", "this is a text with no empty space")]
        public void CanRemoveEmptySpaces(string valueWithEmptySpace, string expectedText)
        {
            Assert.That(valueWithEmptySpace.RemoveEmptySpaces(), Is.EqualTo(expectedText));
        }

        [TestCase(null, "")]
        [TestCase("  téxt wíth accênts  ", "text with accents")]
        public void CanRemoveAccentsAndEmptySpaces(string valueWithEmptySpacesAndAccents, string expectedText)
        {
            Assert.That(valueWithEmptySpacesAndAccents.RemoveAccentsAndEmptySpaces(), Is.EqualTo(expectedText));
        }

        [TestCase(null, "")]
        [TestCase("text", "xt")]
        [TestCase("formated value", "ue")]
        public void CanReturnTwoLastCharactersFromText(string text, string expectedText)
        {
            Assert.That(text.GetTwoLastCharacters(), Is.EqualTo(expectedText));
        }
    }
}