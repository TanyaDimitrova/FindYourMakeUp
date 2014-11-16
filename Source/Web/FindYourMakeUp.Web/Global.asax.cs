namespace FindYourMakeUp.Web
{
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using FindYourMakeUp.Web.Infrastructure.Mapping;
    using System.Threading;
    using System.Globalization;
    using System;
    using FindYourMakeUp.Web.Infrastructure.ModelBinders;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            ModelBinders.Binders.Add(typeof(DateTime), new JsonDateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(Nullable<DateTime>), new JsonDateTimeModelBinder());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var autoMapperConfig = new AutoMapperConfig(Assembly.GetExecutingAssembly());
            autoMapperConfig.Execute();
        }
    }
}
