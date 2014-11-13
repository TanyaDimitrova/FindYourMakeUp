namespace FindYourMakeUp.Web.ViewModels.Home
{
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class IndexProductsViewModel : IMapFrom<Product>
    {
        public string Name { get; set; }
    }
}