namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class CategoriesParentsViewModel : CategoriesViewModel, IMapFrom<Category>, IHaveCustomMappings
    {
        [UIHint("ParentCategories")]
        public CategoriesViewModel Category { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoriesParentsViewModel>()
                      .ForMember(c => c.Category, o => o.MapFrom(x => x.ParentCategory))
                      .ReverseMap();
        }
    }
}