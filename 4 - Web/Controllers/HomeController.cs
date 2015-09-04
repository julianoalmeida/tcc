using System.Web.Mvc;

namespace _4___Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index() => View();
    }
}
