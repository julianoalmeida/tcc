using Comum;
using Entidades;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioBusiness _usuario;

        public LoginController(IUsuarioBusiness usuario)
        {
            _usuario = usuario;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public JsonResult Logar(string login, string senha)
        {
            var retorno = 1;
            Usuario usuario = _usuario.RecuperarUsuarioLogado(login, senha);
            if (usuario != null)
            {
                TempData[Constantes.USUARIO_LOGADO] = usuario;
            }
            else
            {
                retorno = 2;
            }

            TempData.Keep(Constantes.USUARIO_LOGADO);
            return Json(new { retorno = retorno });
        }

        public ActionResult Sair()
        {
            TempData[Constantes.USUARIO_LOGADO] = null;
            return RedirectToAction("Index", "Login");
        }
      
    }
}
