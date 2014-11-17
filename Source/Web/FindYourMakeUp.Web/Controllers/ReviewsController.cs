namespace FindYourMakeUp.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Data.UoW;
    using FindYourMakeUp.Web.IntpuViewModels;
    using FindYourMakeUp.Web.ViewModels;

    public class ReviewsController : BaseController
    {
        public ReviewsController(IFindYourMakeUpData data) : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            int id = int.Parse(Request.FilePath.Split(new char[] { '/' }).Last());

            if (this.Data.Reviews.All().Any(r => r.ProductId == id && r.UserId == this.CurrentUser.Id))
            {
                ModelState.AddModelError("AlreadyExisting", "You have already created review for this product ");
            }

            var product = this.Data
                              .Products
                              .All()
                              .Where(p => p.Id == id)
                              .Project()
                              .To<ProductListViewModel>()
                              .First();

            var inputModel = new ReviewInputModel { ProductId = id, Product = product };
            return this.PartialView("_Create", inputModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewInputModel review)
        {
            if (review != null && ModelState.IsValid)
            {
                if (this.Data.Reviews.All().Any(r => r.ProductId == review.ProductId && r.UserId == this.CurrentUser.Id))
                {
                    ModelState.AddModelError("AlreadyExisting", "You have created review for this product already");
                }
                else
                {
                    var dbReview = Mapper.Map<Review>(review);
                    dbReview.UserId = this.CurrentUser.Id;

                    this.Data.Reviews.Add(dbReview);
                    this.Data.SaveChanges();

                    return this.PartialView("_ListReviews", dbReview.Product.Reviews);
                }
            }

            return this.PartialView("_Create", review);
        }
    }
}