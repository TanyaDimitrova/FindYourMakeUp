namespace FindYourMakeUp.Web.ViewModels.Home
{
    using AutoMapper;
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class IndexProductsViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ManufacturerName { get; set; }

        [UIHint("Image")]
        public string ImageUrl { get; set; }

        public int ReviewsCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Product, IndexProductsViewModel>()
                         .ForMember(p => p.ManufacturerName, opt => opt.MapFrom(x => x.Manufacturer.Name));

            configuration.CreateMap<Product, IndexProductsViewModel>()
                        .ForMember(p => p.ReviewsCount, opt => opt.MapFrom(x => x.Reviews.Count));
        }
    }
}