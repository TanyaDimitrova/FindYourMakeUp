namespace FindYourMakeUp.Web.ViewModels
{
    using System.Collections.Generic;

    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Infrastructure.Mapping;
    using System.Collections;

    public class SubCategoryNavigationViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductTypeNavigationViewModel> Types { get; set; }
    }
}