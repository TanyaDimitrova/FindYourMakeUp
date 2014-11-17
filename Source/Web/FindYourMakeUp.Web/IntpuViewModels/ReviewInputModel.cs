namespace FindYourMakeUp.Web.IntpuViewModels
{
    using System.ComponentModel.DataAnnotations;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;
    using FindYourMakeUp.Web.ViewModels;

    public class ReviewInputModel : IMapFrom<Review>
    {
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        [Required]
        [MinLength(100)]
        public string Content { get; set; }

        [Required]
        [Range(-10, 10)]
        [UIHint("NumericTextBox")]
        public int Rate { get; set; }

        [Required]
        public int ProductId { get; set; }

        public ProductListViewModel Product { get; set; }
    }
}