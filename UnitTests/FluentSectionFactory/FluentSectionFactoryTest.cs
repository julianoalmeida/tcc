using NUnit.Framework;
using Data.SetupSessionFactory;

namespace UnitTests.FluentSectionFactory
{
    [TestFixture]
    public class FluentSectionFactoryTest
    {
        [Test]
        public void CanCreateNHibernateSectionFactory()
        {
            const string DEFAULT_CONNECTION_STRING = "coonStringTcc";

            var factory = FluentSessionFactory.GetSessionFactory("thread_static", DEFAULT_CONNECTION_STRING);

            Assert.IsFalse(factory.IsClosed);
            Assert.That(factory, Is.Not.Null);
        }
    }
}
