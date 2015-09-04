using System.Web.Mvc;
using Comum;
using Negocio;

namespace _4___Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserBusiness _user;
        public LoginController(IUserBusiness user)
        {
            _user = user;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public JsonResult Logar(string login, string senha)
        {
            var result = 1;
            var usuario = _user.GetLoggedUser(login, senha);

            if (usuario != null)
                TempData[Constants.LOGGED_USER] = usuario;
            else
                result = 2;

            TempData.Keep(Constants.LOGGED_USER);
            return Json(new { retorno = result });
        }

        public ActionResult Sair()
        {
            TempData[Constants.LOGGED_USER] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}