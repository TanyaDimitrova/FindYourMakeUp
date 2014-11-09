using FindYourMakeUp.Data.UoW;
using System;
using System.Collections.Generic;
using System.IO;
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
            var myDir = new DirectoryInfo(Server.MapPath("~") + @"\Content\Images");
            int count = myDir.GetFiles().Length;
            ViewBag.Count = count;
            return View();
        }
    }
}