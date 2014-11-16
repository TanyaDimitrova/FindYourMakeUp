using AutoMapper;
using FindYourMakeUp.Data.UoW;
using FindYourMakeUp.Web.IntpuViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FindYourMakeUp.Web.ViewModels;
using FindYourMakeUp.Data.Models;

namespace FindYourMakeUp.Web.Controllers
{
    public class ReviewsController : BaseController
    {
        public ReviewsController(IFindYourMakeUpData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            int id = int.Parse(Request.FilePath.Split(new char[] { '/' }).Last());
            var product = this.Data
            .Products
                              .All()
                              .Where(p => p.Id == id)
                              .Project()
                              .To<ProductListViewModel>()
                              .First();
            var inputModel = new ReviewInputModel { ProductId = id, Product = product };
            return PartialView("_Create", inputModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewInputModel review)
        {
            if (review != null && ModelState.IsValid)
            {
                var dbReview = Mapper.Map<Review>(review);
                dbReview.UserId = this.CurrentUser.Id;
                this.Data.Reviews.Add(dbReview);
                // TODO: One user should be able to create only one review per product
                this.Data.SaveChanges();
                var r = this.Data.Products.GetById(dbReview.ProductId).Rating;
                // TODO: Test this message
                TempData["SuccessMessage"] = "Your review was successfully created!";
                return Json(new { url = Url.Action("Products", "Details", new { id = dbReview.ProductId }) });
            }
            return PartialView("_Create", review);
        }
    }
}