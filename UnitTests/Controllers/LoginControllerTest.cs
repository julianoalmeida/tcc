using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Comum;
using Data;
using Data.BaseRepositories;
using Entidades;
using Entidades.Enums;
using Moq;
using Negocio;
using NUnit.Framework;
using _4___Web.Controllers;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class LoginControllerTest : BaseControllerTest
    {
        private IUserBusiness _userBusiness;
        private IPersonBusiness _personBusiness;
        private LoginController _loginController;
        private readonly Mock<IUserData> _userData = new Mock<IUserData>();
        private readonly Mock<IBaseRepositoryRepository<Person>> _personData = new Mock<IBaseRepositoryRepository<Person>>();
        private const string INVALID_CREDENTIALS = "invalid credentials";
        private const string DUPLICATED_NAME = "DUPLICATED NAME";
        private DateTime _duplicatedDate = new DateTime(2000, 5, 5);

        [SetUp]
        public void BeforeScenario()
        {
            Setup();
        }

        [TestCase("", "")]
        [TestCase("", "password")]
        [TestCase(INVALID_CREDENTIALS, INVALID_CREDENTIALS)]
        public void RedirectToLoginIfProvidedCredentialIsNullOrCantFoundUser(string login, string password)
        {
            const string EXPECTED_ACTION_NAME = "Index";
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var result = GetRedirectToRouteResultWith(_loginController.Login(login, password));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
            Assert.That(_loginController.TempData[Constants.LOGGED_USER], Is.Null);
        }

        [Test]
        public void RedirectToHomePageIfFoundTheUser()
        {
            const string EXPECTED_ACTION_NAME = "Index";
            const string EXPECTED_CONTROLLER_NAME = "Home";

            var result = GetRedirectToRouteResultWith(_loginController.Login("login", "password"));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
            AssertTempdataLoggedUserHasValue("login", "password");
        }

        [Test]
        public void RedirectToLoginWhenLeaveSystem()
        {
            const string EXPECTED_ACTION_NAME = "Index";
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var result = GetRedirectToRouteResultWith(_loginController.Exit());

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
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
            const string EXPECTED_ACTION_NAME = "Index";
            const string EXPECTED_CONTROLLER_NAME = "Login";

            var result = GetRedirectToRouteResultWith(_loginController.CreateAccount(ValidPerson));

            Assert.That(result.RouteValues["action"], Is.EqualTo(EXPECTED_ACTION_NAME));
            Assert.That(result.RouteValues["controller"], Is.EqualTo(EXPECTED_CONTROLLER_NAME));
            Assert.That(_loginController.TempData[Constants.LOGGED_USER], Is.Null);
        }

        private void AssertTempdataLoggedUserHasValue(string login, string password)
        {
            var user = _loginController.TempData[Constants.LOGGED_USER] as User;

            Assert.That(user?.Login, Is.EqualTo(login));
            Assert.That(user?.Password, Is.EqualTo(password));
        }

        private IEnumerable<SelectListItem> ViewDataAccessProfileList
            => ViewTadaToListSelectListItem(_loginController.ViewData["AccessProfiles"]);

        private IEnumerable<SelectListItem> ViewDataSexList => ViewTadaToListSelectListItem(_loginController.ViewData["Sex"]);

        private static Person ValidPerson
            =>
                new Person
                {
                    BirthDate = DateTime.Now,
                    Name = "Name",
                    Id = 1,
                    MobileNumber = "999999999",
                    Email = "test@test.com",
                    Sex = (int)SexEnum.Feminino
                };

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

            _userBusiness = new UserBusiness(_userData.Object);
            _personBusiness = new PersonBusiness(_personData.Object);
            _loginController = new LoginController(_userBusiness, _personBusiness);
        }
    }
}
