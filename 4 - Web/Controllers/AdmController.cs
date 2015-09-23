using Comum;
using System;
using System.Web.Mvc;
using Entidades;
using Negocio;

namespace _4___Web.Controllers
{
    public class AdmController : BaseController
    {
        private readonly IAdmBusiness _admBusiness;
        public AdmController(IAdmBusiness admBusiness)
        {
            _admBusiness = admBusiness;
        }

        public new ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(Adm adm)
        {
            var actionName = nameof(Index);

            try
            {
                _admBusiness.SaveAndReturn(adm);
            }
            catch (Exception ex)
            {
                TempData[nameof(Adm)] = adm;
                TempData[Constants.ERROR] = ex.Message;
                actionName = nameof(Manage);
            }

            return RedirectToAction(actionName, "Adm");
        }
    }
}
