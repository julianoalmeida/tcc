using System;
using System.Collections.Generic;
using System.Linq;
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
    public class LoginControllerTest
    {
        private IUserBusiness _userBusiness;
        private LoginController _loginController;
        private readonly Mock<IUserData> _userData = new Mock<IUserData>();

        [SetUp]
        public void BeforeScenario()
        {
            Setup();
        }

        [TestCase("", "")]
        [TestCase("", "password")]
        [TestCase("not found login", "not found password")]
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

        private static void AssertListItensAreEquals(List<SelectListItem> firstList, IEnumerable<SelectListItem> secondList)
        {
            firstList.ForEach(item => secondList.Any(SelectListItemEqualsCondition(item)));
        }

        private static RedirectToRouteResult GetRedirectToRouteResultWith(ActionResult actionResult)
        {
            return (RedirectToRouteResult)actionResult;
        }

        private void Setup()
        {
            _userData.Setup(a => a.GetByCredentials("", "")).Returns(new User());

            _userData.Setup(a => a.GetByCredentials("not found login", "not found password")).Returns(new User());

            _userData.Setup(a => a.GetByCredentials("login", "password"))
                     .Returns(new User
                     {
                         AccessCode = (int)AccessProfileEnum.Adm,
                         Id = 1,
                         Login = "login",
                         Password = "password"
                     });

            _userBusiness = new UserBusiness(_userData.Object);
            _loginController = new LoginController(_userBusiness);
        }

        private void AssertTempdataLoggedUserHasValue(string login, string password)
        {
            var user = _loginController.TempData[Constants.LOGGED_USER] as User;

            Assert.That(user?.Login, Is.EqualTo(login));
            Assert.That(user?.Password, Is.EqualTo(password));
        }

        private IEnumerable<SelectListItem> ViewDataSexList => (_loginController.ViewData["Sex"] as List<SelectListItem>);
        private IEnumerable<SelectListItem> ViewDataAccessProfileList
            => (_loginController.ViewData["AccessProfiles"] as List<SelectListItem>);

        private static Func<SelectListItem, bool> SelectListItemEqualsCondition(SelectListItem item)
        {
            return
                a =>
                    a.Value == item.Value && a.Selected == item.Selected && a.Text == item.Text &&
                    a.Disabled == item.Disabled && a.Group == item.Group;
        }
    }
}
