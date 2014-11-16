namespace FindYourMakeUp.Web.Controllers
{
    using System.Threading;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Data.UoW;
    using System;
    using System.Globalization;

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
        
        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}