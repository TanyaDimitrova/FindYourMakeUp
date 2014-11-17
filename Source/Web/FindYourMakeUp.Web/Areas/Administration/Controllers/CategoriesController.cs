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

    using Model = FindYourMakeUp.Data.Models.Category;
    using ViewModel = FindYourMakeUp.Web.Areas.Administration.ViewModels.CategoriesParentsViewModel;

    public class CategoriesController : KendoGridAdministrationController
    {
        public CategoriesController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
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

            dbModel.ParentCategoryId = model.Category.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Update<Model, ViewModel>(model, model.Id);
            dbModel.ParentCategoryId = model.Category.Id;
            this.Data.SaveChanges();

            return this.GridOperation(model, request);
        }

        public ActionResult GetParents()
        {
            this.Data.Context.Configuration.ProxyCreationEnabled = false;
            var data = this.Data
                .Categories
                .All()
                .Where(c => c.ParentCategoryId == null)
                .Project()
                .To<CategoriesParentsViewModel>();

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Categories.Delete(model.Id.Value);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            this.Data.Context.Configuration.ProxyCreationEnabled = false;
            var data = this.Data
                .Categories
                .All()
                .Project()
                .To<CategoriesParentsViewModel>();

            return data;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Categories.GetById(id) as T;
        }

        private void PopulateCategories()
        {
            var categories = this.Data
                .Categories
                .All()
                .Where(c => c.ParentCategoryId == null)
                .OrderBy(c => c.Name)
                .Project()
                .To<CategoriesViewModel>();

            this.ViewData["Categories"] = categories;
            this.ViewData["DefaultCategories"] = categories.FirstOrDefault();
        }
    }
}