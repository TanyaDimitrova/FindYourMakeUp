namespace FindYourMakeUp.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.Areas.Administration.Controllers.Base;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.UI;

    using Model = FindYourMakeUp.Data.Models.Manufacturer;
    using ViewModel = FindYourMakeUp.Web.Areas.Administration.ViewModels.ManufacturerViewModel;

    public class ManufacturersController : KendoGridAdministrationController
    {
        public ManufacturersController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
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
            model.Name = dbModel.Name;

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Manufacturers.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Manufacturers.GetById(id) as T;
        }

        protected override IEnumerable GetData()
        {
            this.Data.Context.Configuration.ProxyCreationEnabled = false;
            return this.Data.Manufacturers.All().Project().To<ManufacturerViewModel>();
        }
    }
}