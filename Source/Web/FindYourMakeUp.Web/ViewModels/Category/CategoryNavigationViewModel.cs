﻿namespace FindYourMakeUp.Web.ViewModels
{
    using System.Collections.Generic;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class CategoryNavigationViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SubCategoryNavigationViewModel> SubCategories { get; set; }
    }
}