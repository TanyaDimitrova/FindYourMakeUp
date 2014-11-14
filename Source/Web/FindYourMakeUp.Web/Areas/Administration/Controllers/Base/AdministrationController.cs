namespace FindYourMakeUp.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.Controllers;

    // TODO: Remove comment
    // [Authorize(Roles = "Admin")]
    public abstract class AdministrationController : BaseController
    {
        public AdministrationController(IFindYourMakeUpData data)
            : base(data)
        {
        }
    }
}