using System.Collections.Generic;
using Entidades.Extensions;
using NUnit.Framework;

namespace UnitTests.ExtensionsTests
{
    [TestFixture]
    public class ListExtensionTest
    {
        [Test]
        public void CanBuildStringFormatedTextFromListWithCollonSplitingElementsWithMultipleItens()
        {
            const string EXPECTED_FORMATED_TEXT = "item1, item2, item3 e item4";

            var list = new List<string> { "item1", "item2", "item3", "item4" };
            var formatedText = list.BuildStringFormatedTextFromListWithCollonSplitingElements();

            Assert.That(formatedText, Is.EqualTo(EXPECTED_FORMATED_TEXT));
        }

        [Test]
        public void CanBuildStringFormatedTextFromListWithCollonSplitingElementsWithTwoItens()
        {
            const string EXPECTED_FORMATED_TEXT = "item1 e item2";

            var list = new List<string> { "item1", "item2" };
            var formatedText = list.BuildStringFormatedTextFromListWithCollonSplitingElements();

            Assert.That(formatedText, Is.EqualTo(EXPECTED_FORMATED_TEXT));
        }

        [Test]
        public void CanBuildStringFormatedTextFromListWithCollonSplitingElementsWithOneItens()
        {
            const string EXPECTED_FORMATED_TEXT = "item1";

            var list = new List<string> { "item1" };
            var formatedText = list.BuildStringFormatedTextFromListWithCollonSplitingElements();

            Assert.That(formatedText, Is.EqualTo(EXPECTED_FORMATED_TEXT));
        }

        [Test]
        public void BuildEmptySpaceWithEmptyList()
        {
            const string EXPECTED_FORMATED_TEXT = "";

            var formatedText = new List<string>().BuildStringFormatedTextFromListWithCollonSplitingElements();

            Assert.That(formatedText, Is.EqualTo(EXPECTED_FORMATED_TEXT));
        }

        [Test]
        public void BuildEmptySpaceWithListWithNullValues()
        {
            const string EXPECTED_FORMATED_TEXT = "";
            var list = new List<string> { null, null, null, null };

            var formatedText = list.BuildStringFormatedTextFromListWithCollonSplitingElements();

            Assert.That(formatedText, Is.EqualTo(EXPECTED_FORMATED_TEXT));
        }

        [Test]
        public void CanIgnoreNullAndEmptyValues()
        {
            const string EXPECTED_FORMATED_TEXT = "item1";
            var list = new List<string> { null, null, null, null, "item1" };

            var formatedText = list.BuildStringFormatedTextFromListWithCollonSplitingElements();

            Assert.That(formatedText, Is.EqualTo(EXPECTED_FORMATED_TEXT));
        }

        [Test]
        public void CanBuildAnformatedStringWithAListContaingNullAndEmptyValues()
        {
            const string EXPECTED_FORMATED_TEXT = "item1, item2, item3, item4 e item5";
            var list = new List<string> { null, "item1", null, null, "item2", "", "", null, null, "item3", "item4", "item5" };

            var formatedText = list.BuildStringFormatedTextFromListWithCollonSplitingElements();

            Assert.That(formatedText, Is.EqualTo(EXPECTED_FORMATED_TEXT));
        }
    }
}