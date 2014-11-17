namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.Areas.Administration.Controllers.Base;

    public class HomeController : AdministrationController
    {
        public HomeController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            // todo: opravi viewto
            return this.View();
        }
    }
}