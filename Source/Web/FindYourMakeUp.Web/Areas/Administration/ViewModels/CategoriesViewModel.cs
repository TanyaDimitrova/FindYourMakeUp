namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;
    using FindYourMakeUp.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class CategoriesViewModel : AdministrationViewModel, IMapFrom<Category>, IHaveCustomMappings
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoriesViewModel>()
                .ForMember(c => c.Name, opt => opt.MapFrom(c => c.Name)).ReverseMap();
        }
    }
}
