namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.Areas.Administration.Controllers.Base;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.UI;

    using Model = FindYourMakeUp.Data.Models.Product;
    using ViewModel = FindYourMakeUp.Web.Areas.Administration.ViewModels.ProductsViewModel;

    public class ProductsController : KendoGridAdministrationController
    {
        public ProductsController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            this.PopulateManufacturers();
            this.PopulateProductTypes();
            this.PopulateCategories();
            return this.View();
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
            {
                model.Id = dbModel.Id;
            }

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Update<Model, ViewModel>(model, model.Id);

            model.ManufacturerName = dbModel.Manufacturer.Name;
            model.CategoryName = dbModel.Category.Name;
            model.ProductTypeName = dbModel.ProductType.Name;

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Products.Delete(model.Id.Value);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        public JsonResult GetCascadeCategories()
        {
            var data = this.Data
                .Categories
                .All()
                .Where(c => c.ParentCategoryId == null)
                .Select(c => new { Id = c.Id, Name = c.Name });

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeSubCategories(int? categoryId)
        {
            var subCategories = this.Data.Categories.All();

            if (categoryId != null)
            {
                subCategories = subCategories.Where(p => p.ParentCategoryId == categoryId);
            }

            return this.Json(subCategories.Select(p => new { Id = p.Id, Name = p.Name }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeProducts(int? categoryId)
        {
            var productTypes = this.Data.ProductTypes.All();

            if (categoryId != null)
            {
                productTypes = productTypes.Where(p => p.Categories.Any(c => c.Id == categoryId));
            }

            return this.Json(productTypes.Select(p => new { Id = p.Id, Name = p.Name }), JsonRequestBehavior.AllowGet);
        }

        protected override IEnumerable GetData()
        {
            this.Data.Context.Configuration.ProxyCreationEnabled = false;
            return this.Data.Products.All().Project().To<ProductsViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Products.GetById(id) as T;
        }

        private void PopulateCategories()
        {
            var categories = this.Data.Categories.All().OrderBy(c => c.Name).Project().To<CategoriesViewModel>();

            this.ViewData["Categories"] = categories;
            this.ViewData["DefaultCategories"] = categories.FirstOrDefault();
        }

        private void PopulateManufacturers()
        {
            var manufacturers = this.Data.Manufacturers.All().OrderBy(c => c.Name).Project().To<ManufacturerViewModel>();

            this.ViewData["Manufacturers"] = manufacturers;
            this.ViewData["DefaultManufacturers"] = manufacturers.FirstOrDefault();
        }

        private void PopulateProductTypes()
        {
            var productTypes = this.Data.ProductTypes.All().OrderBy(c => c.Name).Project().To<ProductTypeViewModel>();

            this.ViewData["ProductTypes"] = productTypes;
            this.ViewData["DefaultProductTypes"] = productTypes.FirstOrDefault();
        }
    }
}