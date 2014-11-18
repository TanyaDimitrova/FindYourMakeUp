namespace FindYourMakeUp.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class ProductListViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int Rating { get; set; }

        public string ManufacturerName { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductListViewModel>()
                         .ForMember(p => p.ManufacturerName, opt => opt.MapFrom(x => x.Manufacturer.Name));
        }
    }
}