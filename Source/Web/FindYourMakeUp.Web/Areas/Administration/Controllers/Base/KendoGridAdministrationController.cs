namespace FindYourMakeUp.Web.Areas.Administration.Controllers.Base
{
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;

    using AutoMapper;

    using FindYourMakeUp.Data.Contracts.Models;
    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public abstract class KendoGridAdministrationController : AdministrationController
    {
        public KendoGridAdministrationController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data =
                this.GetData()
                    .ToDataSourceResult(request);

            return this.Json(data);
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                int result = this.ChangeEntityStateAndSave(dbModel, EntityState.Added);

                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual TModel Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : AuditInfo
            where TViewModel : AdministrationViewModel
        {
            if (model != null && ModelState.IsValid)
            {
                TModel dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                int result = this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);

                model.ModifiedOn = dbModel.ModifiedOn;

                return dbModel;
            }

            return null;
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        private int ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            return this.Data.SaveChanges();
        }
    }
}