namespace FindYourMakeUp.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.ViewModels;

    public class ProductsController : BaseController
    {
        public ProductsController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        // GET: Products
        public ActionResult Index()
        {
            var categories = this.Data.Categories.All().Where(c => c.ParentCategory == null) // Parents
                                 .Select(c => new CategoryNavigationViewModel()
                                        {
                                            Id = c.Id,
                                            Name = c.Name,
                                            SubCategories = c.Children.Select(t => new SubCategoryNavigationViewModel()// subcategorires
                                            {
                                                Id = t.Id,
                                                Name = t.Name,
                                                Types = t.ProductTypes.Select(x => new ProductTypeNavigationViewModel()
                                                {
                                                    Id = x.Id,
                                                    Name = x.Name,
                                                    CategoryId = t.Id
                                                })
                                            })
                                        }).ToList();
            return View(categories);
        }


        // GET Products/ListByType/(typeId)
        public ActionResult ListByType(int id, int categoryId)
        {
            var productsList = this.Data.Products
                .All()
                .Where(p => p.CategoryId == categoryId && p.ProductTypeId == id)
                .ToList();
            return PartialView("_ProductsListView", productsList);
        }
    }
}