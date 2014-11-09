using FindYourMakeUp.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindYourMakeUp.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}