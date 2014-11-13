namespace FindYourMakeUp.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using FindYourMakeUp.Data.UoW;

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

            var products = this.Data.Products.All().ToList().OrderBy(p => p.Rating).First().Category.Name;

            ViewData["Category"] = products;
            var categories = this.Data.Categories.All().Where(c => c.ParentCategoryId == null).ToList();
            ViewBag.Categories = categories;
            return View(categories);
        }

        [HttpPost]
        public ActionResult Index(int emp)
        {
            return View(emp);
        }

        public ActionResult GetSubCategoriesNames(int categoryId)
        {
            var cats = this.Data.Categories.All().Where(c => c.ParentCategoryId == categoryId).ToArray();

            return Json(cats);
        }
    }
}