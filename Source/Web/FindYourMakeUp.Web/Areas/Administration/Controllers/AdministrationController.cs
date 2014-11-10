namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.Controllers;

    [Authorize(Roles = "Admin")]
    public class AdministrationController : BaseController
    {
        public AdministrationController(IData data)
            : base(data)
        {
        }
    }
}