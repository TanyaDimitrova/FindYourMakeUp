using FindYourMakeUp.Data.UoW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper.QueryableExtensions;
using FindYourMakeUp.Web.ViewModels.Home;
using FindYourMakeUp.Data.Models;

namespace FindYourMakeUp.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var myDir = new DirectoryInfo(Server.MapPath("~") + @"\Content\Images");
            int count = myDir.GetFiles().Length;
            ViewBag.Count = count;

            var products = this.Data.Products.All().Project().To<IndexProductsViewModel>();

            return View(products);
        }
    }
}