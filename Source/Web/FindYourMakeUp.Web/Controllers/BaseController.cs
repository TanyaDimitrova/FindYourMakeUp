namespace FindYourMakeUp.Web.Controllers
{
    using System.Threading;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Data.UoW;

    public abstract class BaseController : Controller
    {
        private IFindYourMakeUpData data;

        public BaseController(IFindYourMakeUpData data)
        {
            this.Data = data;
            this.CurrentUser = data.Users.GetById(Thread.CurrentPrincipal.Identity.GetUserId());
        }

        protected IFindYourMakeUpData Data
        {
            get
            {
                return this.data;
            }

            private set
            {
                this.data = value;
            }
        }

        protected ApplicationUser CurrentUser { get; private set; }
    }
}