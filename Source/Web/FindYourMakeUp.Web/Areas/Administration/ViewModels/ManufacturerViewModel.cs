namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;
    using FindYourMakeUp.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class ManufacturerViewModel : AdministrationViewModel, IMapFrom<Manufacturer>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
