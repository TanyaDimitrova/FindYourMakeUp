using FindYourMakeUp.Data.UoW;
using FindYourMakeUp.Web.Areas.Administration.Controllers.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FindYourMakeUp.Web.Areas.Administration.ViewModels;
using Kendo.Mvc.UI;

using Model = FindYourMakeUp.Data.Models.Manufacturer;
using ViewModel = FindYourMakeUp.Web.Areas.Administration.ViewModels.ManufacturerViewModel;

namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
    public class ManufacturersController : KendoGridAdministrationController
    {
        public ManufacturersController(IFindYourMakeUpData data)
            : base(data)
        {

        }

        // GET: Administration/Manufacturers
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            this.Data.Context.Configuration.ProxyCreationEnabled = false;
            return this.Data.Manufacturers.All().Project().To<ManufacturerViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Manufacturers.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Update<Model, ViewModel>(model, model.Id);

            //model.ManufacturerName = dbModel.Manufacturer.Name;
            //model.CategoryName = dbModel.Category.Name;
            //model.ProductTypeName = dbModel.ProductType.Name;

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Products.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}