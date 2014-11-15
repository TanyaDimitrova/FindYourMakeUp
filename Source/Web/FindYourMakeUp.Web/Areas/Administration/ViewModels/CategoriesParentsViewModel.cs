using AutoMapper;
using FindYourMakeUp.Data.Models;
using FindYourMakeUp.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    public class CategoriesParentsViewModel : CategoriesViewModel, IHaveCustomMappings
    {
        public int? ParentCategoryId { get; set; }

        [UIHint("Category2")]
        public string ParentCategoryName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<CategoriesParentsViewModel, Category>()
                .ForMember(dest => dest.ParentCategory, input =>
    input.MapFrom(s => Mapper.Map<CategoriesParentsViewModel, Category>(s)));
        }
    }
}