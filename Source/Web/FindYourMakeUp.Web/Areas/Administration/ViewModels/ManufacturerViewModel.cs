namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class ManufacturerViewModel : AdministrationViewModel, IMapFrom<Manufacturer>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
