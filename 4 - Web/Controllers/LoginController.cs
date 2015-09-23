using System;
using Negocio;
using System.Web.Mvc;
using Comum;
using Entidades;
using Entidades.Enums;

namespace _4___Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserBusiness _user;
        private readonly IPersonBusiness _personBusiness;
        public LoginController(IUserBusiness user, IPersonBusiness personBusiness)
        {
            _user = user;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [AllowAnonymous]
        public new ActionResult Index() => View(nameof(Index));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password)
        {
            var controllerName = "Home";
            try
            {
                TempData[Constants.LOGGED_USER] = _user.GetByCredentials(login, password);
                TempData.Keep(Constants.LOGGED_USER);
            }
            catch (Exception ex)
            {
                controllerName = "Login";
                TempData[Constants.ERROR] = ex.Message;
            }

            return RedirectToAction(nameof(Index), controllerName);
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            var person = TempData[nameof(Person)] as Person ?? new Person();
            BuildDropDownLists(person);
            return View(nameof(CreateAccount), person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(Person model)
        {
            var actionName = nameof(Index);
            try
            {
                _personBusiness.SaveAndReturn(model);
            }
            catch (Exception ex)
            {
                actionName = nameof(CreateAccount);
                TempData[Constants.ERROR] = ex.Message;
                TempData[nameof(Person)] = model;
            }

            return RedirectToAction(actionName, "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Exit()
        {
            TempData[Constants.LOGGED_USER] = null;
            return RedirectToAction(nameof(Index), "Login");
        }

        private void BuildDropDownLists(Person person)
        {
            ViewData["Sex"] = BuildListItemfromEnum<SexEnum>(person.Sex.ToString());
            ViewData["AccessCode"] = BuildListItemfromEnum<AccessProfileEnum>(person.User?.AccessCode.ToString());
        }
    }
}