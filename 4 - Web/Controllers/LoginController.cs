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
        public new ActionResult Index() => View();

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
                TempData["Error"] = ex.Message;
                controllerName = "Login";
            }

            return RedirectToAction("Index", controllerName);
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            BuildDropDownLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(Person model)
        {
            var actionName = "Login";
            try
            {
                _personBusiness.SaveAndReturn(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                actionName = "CreateAccount";
            }

            return RedirectToAction(actionName, "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Exit()
        {
            TempData[Constants.LOGGED_USER] = null;
            return RedirectToAction("Index", "Login");
        }

        private void BuildDropDownLists()
        {
            ViewData["Sex"] = BuildListItemfromEnum<SexEnum>(string.Empty);
            ViewData["AccessProfiles"] = BuildListItemfromEnum<AccessProfileEnum>(string.Empty);
        }
    }
}