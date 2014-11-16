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
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [UIHint("CascadeSubCategory")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

  
        [UIHint("Category")]
        public int? ParentCategoryId { get; set; }

        public string ParentCategoryName { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        [UIHint("Manufacturer")]
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [UIHint("ProductType")]
        public int ProductTypeId { get; set; }

        public string ProductTypeName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            //Mapper.CreateMap<ProductsViewModel, CategoriesViewModel>()
            //   .ForMember(c => c.Id, opt => opt.MapFrom(c => c.ParentCategoryId));

            Mapper.CreateMap<Product, ProductsViewModel>()
              .ForMember(c => c.ParentCategoryId, opt => opt.MapFrom(c => c.Category.ParentCategoryId));

            Mapper.CreateMap<Product, ProductsViewModel>()
                .ForMember(c => c.ParentCategoryName, opt => opt.MapFrom(c => c.Category.ParentCategory!=null? c.Category.ParentCategory.Name:"No category"));

            Mapper.CreateMap<Product, ProductsViewModel>()
                  .ForMember(c => c.CategoryName, opt => opt.MapFrom(c => c.Category.Name));
            
            Mapper.CreateMap<ProductsViewModel, ProductTypeViewModel>()
                 .ForMember(c => c.Name, opt => opt.MapFrom(c => c.ProductTypeName));

            Mapper.CreateMap<ProductsViewModel, ManufacturerViewModel>()
                .ForMember(c => c.Name, opt => opt.MapFrom(c => c.ManufacturerName));
        }
    }
}