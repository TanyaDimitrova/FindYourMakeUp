using FindYourMakeUp.Web.Areas.Administration.Controllers.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FindYourMakeUp.Web.Areas.Administration.ViewModels;
using FindYourMakeUp.Data.UoW;

using Model = FindYourMakeUp.Data.Models.Category;
using ViewModel = FindYourMakeUp.Web.Areas.Administration.ViewModels.CategoriesParentsViewModel;
using Kendo.Mvc.UI;

namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
    public class CategoriesController : KendoGridAdministrationController
    {
        public CategoriesController(IFindYourMakeUpData data)
            : base(data)
        {

        }
        // GET: Administration/Categories
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            this.Data.Context.Configuration.ProxyCreationEnabled = false;
            return this.Data.Categories.All().Project().To<CategoriesParentsViewModel>();
        }

        public ActionResult GetParents()
        {
            this.Data.Context.Configuration.ProxyCreationEnabled = false;
            var data = this.Data.Categories.All().Where(c => c.ParentCategoryId == null)
                .Project().To<CategoriesParentsViewModel>();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Categories.GetById(id) as T;
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
                this.Data.Categories.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}