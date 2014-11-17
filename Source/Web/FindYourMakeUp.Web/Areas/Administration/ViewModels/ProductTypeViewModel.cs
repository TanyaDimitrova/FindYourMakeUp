namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class ProductTypeViewModel : IMapFrom<ProductType>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }
    }
}