using AutoMapper;
using FindYourMakeUp.Data.Models;
using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;
using FindYourMakeUp.Web.Infrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    public class ProductsViewModel : AdministrationViewModel, IMapFrom<Product>, IHaveCustomMappings
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [UIHint("Category")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [UIHint("Manufacturer")]
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        [UIHint("ProductType")]
        public int ProductTypeId { get; set; }

        public string ProductTypeName { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<ProductsViewModel, CategoryViewModel>()
                  .ForMember(c => c.Name, opt => opt.MapFrom(c => c.CategoryName));

            Mapper.CreateMap<ProductsViewModel, ProductTypeViewModel>()
                 .ForMember(c => c.Name, opt => opt.MapFrom(c => c.ProductTypeName));

            Mapper.CreateMap<ProductsViewModel, ManufacturerViewModel>()
                .ForMember(c => c.Name, opt => opt.MapFrom(c => c.ManufacturerName));
        }
    }
}