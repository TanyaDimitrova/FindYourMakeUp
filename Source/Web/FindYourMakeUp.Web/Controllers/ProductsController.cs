namespace FindYourMakeUp.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.ViewModels;
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Utils;
    using System;
    using System.Collections.Generic;

    public class ProductsController : BaseController
    {
        private const int PageSize = 10;

        public ProductsController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var categories = this.Data.Categories.All().Where(c => c.ParentCategory == null)
                                 .Select(c => new CategoryNavigationViewModel()
                                        {
                                            Id = c.Id,
                                            Name = c.Name,
                                            SubCategories = c.Children.Select(t => new SubCategoryNavigationViewModel()
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
            return this.View(categories);
        }

        // GET Products/ListByType/(typeId)
        public ActionResult ListByType(int id, int categoryId)
        {
            var productsList = this.Data.Products
                                   .All()
                                   .Where(p => p.CategoryId == categoryId && p.ProductTypeId == id);

            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "ProductTypeId";
            info.SortDirection = "ascending";
            info.PageSize = PageSize;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(productsList.Count()
                           / info.PageSize)));
            info.CurrentPageIndex = 0;
            var query = productsList.OrderBy(c => c.ProductTypeId).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;

            return this.PartialView("_ProductsListView", query.ToList());
        }

        public ActionResult Details(int id)
        {
            var product = this.Data.Products.All().Where(p => p.Id == id).FirstOrDefault();
            return this.View(product);
        }


        public ActionResult Error()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult GetPage(int id, int categoryId, SortingPagingInfo info)
        {
            var productsList = this.Data.Products
                                   .All()
                                   .Where(p => p.CategoryId == categoryId && p.ProductTypeId == id);

            IQueryable<Product> query = productsList;
            
            query = (info.SortDirection == "ascending" ?
                          productsList.OrderBy(c => c.ProductTypeId) :
                           productsList.OrderByDescending(c => c.ProductTypeId));
            switch (info.SortField)
            {
                case "Name":
                    query = (info.SortDirection == "ascending" ?
                            productsList.OrderBy(c => c.Name) :
                             productsList.OrderByDescending(c => c.Name));
                    break;
                case "Manufacturer":
                    query = (info.SortDirection == "ascending" ?
                            productsList.OrderBy(c => c.Manufacturer) :
                             productsList.OrderByDescending(c => c.Manufacturer));
                    break;
                case "Rating":
                    query = (info.SortDirection == "ascending" ?
                            productsList.OrderBy(c => c.Reviews.Count()) :
                            productsList.OrderByDescending(c => c.Reviews.Count()));
                    break;
            }

            query = query.Skip(info.CurrentPageIndex
                  * info.PageSize).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;
            List<Product> model = query.ToList();
            return PartialView("_ProductsListView", model);
        }
    }
}