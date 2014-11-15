namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class ProductsViewModel : AdministrationViewModel, IMapFrom<Product>, IHaveCustomMappings
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [UIHint("Category")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Required]
        [UIHint("Manufacturer")]
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        [Required]
        [UIHint("ProductType")]
        public int ProductTypeId { get; set; }

        public string ProductTypeName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<ProductsViewModel, CategoriesViewModel>()
                  .ForMember(c => c.Name, opt => opt.MapFrom(c => c.CategoryName));

            Mapper.CreateMap<ProductsViewModel, ProductTypeViewModel>()
                 .ForMember(c => c.Name, opt => opt.MapFrom(c => c.ProductTypeName));

            Mapper.CreateMap<ProductsViewModel, ManufacturerViewModel>()
                .ForMember(c => c.Name, opt => opt.MapFrom(c => c.ManufacturerName));
        }
    }
}