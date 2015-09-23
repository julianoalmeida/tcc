using System.Collections.Generic;
using System.Web.Mvc;
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
    public class LoginControllerTest : BaseControllerHelper
    {
        private IUserBusiness _userBusiness;
        private IPersonBusiness _personBusiness;
        private LoginController _loginController;
        private readonly Mock<IUserData> _userData = new Mock<IUserData>();
        private readonly Mock<IPersonData> _personData = new Mock<IPersonData>();
        private const string INVALID_CREDENTIALS = "invalid credentials";
        private readonly Person _duplicatedPerson = BuildDuplicatedPerson();

        [SetUp]
        public void BeforeScenario()
        {
            Setup();
        }
        
        [TestCase("", "")]
        [TestCase("", "password")]
        [TestCase("login", "")]
        [TestCase(INVALID_CREDENTIALS, INVALID_CREDENTIALS)]
        public void RedirectToLoginIfProvidedCredentialIsNullOrCantFoundUser(string login, string password)
        {
            const string EXPECTED_ACTION_NAME = nameof(_loginController.Index);
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var result = GetRedirectToRouteResultWith(_loginController.Login(login, password));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
            Assert.That(_loginController.TempData[Constants.LOGGED_USER], Is.Null);
        }

        [Test]
        public void RedirectToHomePageIfFoundTheUser()
        {
            const string EXPECTED_ACTION_NAME = nameof(_loginController.Index);
            const string EXPECTED_CONTROLLER_NAME = "Home";

            var result = GetRedirectToRouteResultWith(_loginController.Login("login", "password"));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
        }

        [Test]
        public void FillLoggedUserTempDataIfCanFindUser()
        {
            _loginController.Login("login", "password");
            AssertTempdataLoggedUserHasValue("login", "password");
        }

        [Test]
        public void RedirectToLoginWhenLeaveSystem()
        {
            const string EXPECTED_ACTION_NAME = nameof(_loginController.Index);
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var result = GetRedirectToRouteResultWith(_loginController.Exit());

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
        }

        [Test]
        public void FillLoggedUserWithNullValueWhenLeaveSystem()
        {
            _loginController.Exit();
            Assert.That(_loginController.TempData[Constants.LOGGED_USER], Is.Null);
        }

        [Test]
        public void CanCreateCreateAccountDropDownList()
        {
            var expectedSexList = _loginController.BuildListItemfromEnum<SexEnum>(string.Empty);
            var expectedAccessProfilesList = _loginController.BuildListItemfromEnum<AccessProfileEnum>(string.Empty);

            _loginController.CreateAccount();

            AssertListItensAreEquals(expectedSexList, ViewDataSexList);
            AssertListItensAreEquals(expectedAccessProfilesList, ViewDataAccessProfileList);
        }

        [Test]
        public void RedirectToLoginIfCanCreateUser()
        {
            const string EXPECTED_ACTION_NAME = nameof(_loginController.Index);
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var result = GetRedirectToRouteResultWith(_loginController.CreateAccount(ValidPerson));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
        }

        [TestCase("", "mobile number", "email@email.com", (int)SexEnum.Feminino)]
        [TestCase("name", "", "email@email.com", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "", (int)SexEnum.Feminino)]
        [TestCase("name", "mobile number", "invalid email", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "email@email.com", 0)]
        public void RedirectToCreateUserIfCantSavePerson(string name, string mobileNumber, string email, int sex)
        {
            const string EXPECTED_ACTION_NAME = nameof(_loginController.CreateAccount);
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var person = BuildPersonWith(name, mobileNumber, email, sex);
            var result = GetRedirectToRouteResultWith(_loginController.CreateAccount(person));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
        }

        [TestCase("", "mobile number", "email@email.com", (int)SexEnum.Feminino)]
        [TestCase("name", "", "email@email.com", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "", (int)SexEnum.Feminino)]
        [TestCase("name", "mobile number", "invalid email", (int)SexEnum.Masculino)]
        [TestCase("name", "mobile number", "email@email.com", 0)]
        public void FillPersonTempDataWithCurrentPersonIfCantSavePerson(string name, string mobileNumber, string email, int sex)
        {
            var person = BuildPersonWith(name, mobileNumber, email, sex);
            _loginController.CreateAccount(person);
            AssertPersonTempDataIsEqualsToPerson(person);
        }

        [Test]
        public void ShowRequiredErrorMessageWhenPersonsRequiredFildsAreEmpty()
        {
            _loginController.CreateAccount(new Person());
            Assert.That(_loginController.TempData[Constants.ERROR], Is.EqualTo(Messages.REQUIRED_FIELDS));
        }

        [Test]
        public void RedirectToCreateUserIfProvidedPersonInformationAlreadyExistsInDatabase()
        {
            const string EXPECTED_ACTION_NAME = nameof(_loginController.CreateAccount);
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var result = GetRedirectToRouteResultWith(_loginController.CreateAccount(_duplicatedPerson));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
        }

        [Test]
        public void ShowDuplicatedErrorMessageWhenProvidedPersonInformationAlreadyExistsInDatabase()
        {
            _loginController.CreateAccount(_duplicatedPerson);
            Assert.That(_loginController.TempData[Constants.ERROR], Is.EqualTo(Messages.DUPLICATED_PERSON));
        }

        private void AssertPersonTempDataIsEqualsToPerson(Person person)
        {
            Assert.That(_loginController.TempData[nameof(Person)] as Person, Is.EqualTo(person));
        }

        private void AssertTempdataLoggedUserHasValue(string login, string password)
        {
            var user = _loginController.TempData[Constants.LOGGED_USER] as User;

            Assert.That(user?.Login, Is.EqualTo(login));
            Assert.That(user?.Password, Is.EqualTo(password));
        }

        private static Person BuildPersonWith(string name, string mobileNumber, string email, int sex)
        {
            return new Person { Name = name, MobileNumber = mobileNumber, Email = email, Sex = sex };
        }

        private IEnumerable<SelectListItem> ViewDataAccessProfileList
            => ViewTadaToListSelectListItem(_loginController.ViewData["AccessCode"]);

        private IEnumerable<SelectListItem> ViewDataSexList => ViewTadaToListSelectListItem(_loginController.ViewData["Sex"]);

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

        private static Person BuildDuplicatedPerson()
        {
            return new Person
            {
                Name = "Duplicated Person",
                Id = 1,
                MobileNumber = "999999999",
                Email = "duplicatedEmail@test.com",
                Sex = (int)SexEnum.Feminino
            };
        }

        private void Setup()
        {
            _userData.Setup(a => a.GetByCredentials(string.Empty, string.Empty)).Returns(new User());
            _userData.Setup(a => a.GetByCredentials(INVALID_CREDENTIALS, INVALID_CREDENTIALS)).Returns(new User());
            _userData.Setup(a => a.GetByCredentials("login", "password"))
                     .Returns(new User
                     {
                         AccessCode = (int)AccessProfileEnum.Adm,
                         Id = 1,
                         Login = "login",
                         Password = "password"
                     });

            _personData.Setup(a => a.SaveAndReturn(new Person())).Returns(new Person());
            _personData.Setup(a => a.IsDuplicated(_duplicatedPerson)).Returns(true);

            _userBusiness = new UserBusiness(_userData.Object);
            _personBusiness = new PersonBusiness(_personData.Object);
            _loginController = new LoginController(_userBusiness, _personBusiness);
        }
    }
}
