using FindYourMakeUp.Data.UoW;
using FindYourMakeUp.Web.Areas.Administration.Controllers.Base;
using FindYourMakeUp.Web.Areas.Administration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections;
using FindYourMakeUp.Data.Models;
using Model = FindYourMakeUp.Data.Models.Product;
using ViewModel = FindYourMakeUp.Web.Areas.Administration.ViewModels.ProductsViewModel;
using AutoMapper;
using System.Globalization;
namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
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
            return View();
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

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            //var dbModel=  base.Update<Model, ViewModel>(model, model.Id);

            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.Data.Products.GetById(model.Id.Value);
                Mapper.Map(model, dbModel);
                this.Data.SaveChanges();
                model.ModifiedOn = dbModel.ModifiedOn;
                model.Id = dbModel.Id;


                model.ManufacturerName = dbModel.Manufacturer.Name;
                model.CategoryName = dbModel.Category.Name;
                model.ProductTypeName = dbModel.ProductType.Name;
            }



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

        private void PopulateCategories()
        {
            var categories = this.Data.Categories.All().OrderBy(c => c.Name).Project().To<CategoryViewModel>();

            ViewData["Categories"] = categories;
            ViewData["DefaultCategories"] = categories.FirstOrDefault();

        }

        private void PopulateManufacturers()
        {
            var manufacturers = this.Data.Manufacturers.All().OrderBy(c => c.Name).Project().To<ManufacturerViewModel>();

            ViewData["Manufacturers"] = manufacturers;
            ViewData["DefaultManufacturers"] = manufacturers.FirstOrDefault();

        }

        private void PopulateProductTypes()
        {
            var productTypes = this.Data.ProductTypes.All().OrderBy(c => c.Name).Project().To<ProductTypeViewModel>();

            ViewData["ProductTypes"] = productTypes;
            ViewData["DefaultProductTypes"] = productTypes.FirstOrDefault();

        }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            return base.BeginExecute(requestContext, callback, state);
        }
    
    }
}