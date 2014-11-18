namespace FindYourMakeUp.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.ViewModels.Home;

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

            var topProducts = this.Data
                                  .Products
                                  .All()
                                  .OrderByDescending(p => p.Reviews.Count())
                                  .Take(3)
                                  .Project()
                                  .To<IndexProductsViewModel>()
                                  .ToList();


            return this.View(topProducts);
        }

        [HttpPost]
        public ActionResult Index(int emp)
        {
            return this.View(emp);
        }

        public ActionResult GetSubCategoriesNames(int categoryId)
        {
            var cats = this.Data.Categories.All().Where(c => c.ParentCategoryId == categoryId).ToArray();

            return this.Json(cats);
        }
    }
}