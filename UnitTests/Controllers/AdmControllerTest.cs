using Comum;
using Data;
using Entidades;
using Entidades.Enums;
using Moq;
using Negocio;
using NUnit.Framework;
using _4___Web.Controllers;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class AdmControllerTest : BaseControllerHelper
    {
        private AdmBusiness _admBusiness;
        private AdmController _admController { get; set; }
        private readonly Mock<IAdmData> _admData = new Mock<IAdmData>();
        private readonly Mock<IPersonData> _personData = new Mock<IPersonData>();

        [SetUp]
        public void BeforeScenario()
        {
            Setup();
        }

        [Test]
        public void RedirectToAdmIndexWhenCanSaveNewAdm()
        {
            const string EXPECTED_ACTION_NAME = nameof(_admController.Index);
            const string EXPECTED_CONTROLLER_NAME = "Adm";

            var result = GetRedirectToRouteResultWith(_admController.Manage(new Adm { Person = ValidPerson }));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
        }

        [TestCase("", "mobile number", "email@email.com", (int)SexEnum.Feminino)]
        [TestCase("name", "", "email@email.com", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "", (int)SexEnum.Feminino)]
        [TestCase("name", "mobile number", "invalid email", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "email@email.com", 0)]
        public void StaysOnSamePageWhenCantSaveAdm(string name, string mobileNumber, string email, int sex)
        {
            const string EXPECTED_CONTROLLER_NAME = "Adm";
            const string EXPECTED_ACTION_NAME = nameof(_admController.Manage);

            var person = BuildPersonWith(name, mobileNumber, email, sex);
            var result = GetRedirectToRouteResultWith(_admController.Manage(new Adm { Person = person }));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
        }

        [TestCase("", "mobile number", "email@email.com", (int)SexEnum.Feminino)]
        [TestCase("name", "", "email@email.com", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "", (int)SexEnum.Feminino)]
        [TestCase("name", "mobile number", "invalid email", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "email@email.com", 0)]
        public void FillAdmTempDataWhenCantSaveAdm(string name, string mobileNumber, string email, int sex)
        {
            var adm = new Adm {Person = BuildPersonWith(name, mobileNumber, email, sex)};
            
            _admController.Manage(adm);
            Assert.That(_admController.TempData[nameof(Adm)] as Adm, Is.EqualTo(adm));
        }

        private static Person BuildPersonWith(string name, string mobileNumber, string email, int sex)
        {
            return new Person { Name = name, MobileNumber = mobileNumber, Email = email, Sex = sex };
        }

        private void Setup()
        {
            _admData.Setup(a => a.SaveAndReturn(It.IsAny<Adm>())).Returns(new Adm());
            _personData.Setup(a => a.SaveAndReturn(It.IsAny<Person>())).Returns(new Person());

            _admBusiness = new AdmBusiness(_admData.Object, new PersonBusiness(_personData.Object));

            _admController = new AdmController(_admBusiness);
        }

        private static Person ValidPerson
            =>
                new Person
                {
                    Name = "Name",
                    Id = 1,
                    MobileNumber = "999999999",
                    Email = "test@test.com",
                    Sex = (int)SexEnum.Feminino
                };
    }
}