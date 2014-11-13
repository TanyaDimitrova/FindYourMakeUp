﻿using System.Collections;
using System.Web.Mvc;

using FindYourMakeUp.Data;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity;
using AutoMapper;
using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;
using FindYourMakeUp.Data.Contracts;
using FindYourMakeUp.Data.UoW;
using FindYourMakeUp.Data.Contracts.Models;

namespace FindYourMakeUp.Web.Areas.Administration.Controllers.Base
{
    public abstract class KendoGridAdministrationController : AdministrationController
    {
        public KendoGridAdministrationController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]
                                 DataSourceRequest request)
        {
            var ads =
                this.GetData()
                    .ToDataSourceResult(request);

            return this.Json(ads);
        }

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
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
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                model.ModifiedOn = dbModel.ModifiedOn;

                return dbModel;
            }

            return null;
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]
                                              DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}