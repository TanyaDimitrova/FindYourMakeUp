namespace FindYourMakeUp.Web.ViewModels
{
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class ProductTypeNavigationViewModel : IMapFrom<ProductType>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}