namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using FindYourMakeUp.Data.UoW;

    public class HomeController : AdministrationController
    {
        public HomeController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {

            return View();
        }
    }
}