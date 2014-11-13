namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class ManufacturerViewModel : IMapFrom<Manufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
