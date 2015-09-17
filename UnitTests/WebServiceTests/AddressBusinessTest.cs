using System.Collections.Generic;
using System.Linq;
using Negocio;
using NUnit.Framework;

namespace UnitTests.WebServiceTests
{
    [TestFixture]
    public class AddressBusinessTest
    {
        private readonly IAddressBusiness _adress = new AddressBusiness();

        private readonly List<string> _expectedStates = new List<string>
        {
            "AC", "AL", "AM", "AP", "BA", "CE","DF", "ES", "GO", "MA", "MG","MS", "MT",
            "PA","PB", "PE", "PI", "PR","RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO"
        };

        [Test]
        public void CanGetAllStatesFromWebService()
        {
            var states = _adress.GetStates();
            Assert.That(states.SequenceEqual(_expectedStates));
            Assert.That(states.Count, Is.EqualTo(_expectedStates.Count));
        }

        [Test]
        public void CanGetCitiesNameForEachState()
        {
            _expectedStates.ForEach(state => Assert.IsTrue(_adress.GetCitiesByState(state).Any()));
        }

        [TestCase("BROTAS")]
        [TestCase("BARRETOS")]
        [TestCase("SAO PAULO")]
        [TestCase("SAO CARLOS")]
        [TestCase("ARARAQUARA")]
        [TestCase("AMERICO BRASILIENSE")]
        public void EnsureCanGetSomeSpCities(string expectedCity)
        {
            var cities = _adress.GetCitiesByState("SP");
            Assert.That(cities.Contains(expectedCity));
        }
    }
}
