using FindYourMakeUp.Data.Models;
using FindYourMakeUp.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindYourMakeUp.Web.ViewModels.Home
{
    public class IndexProductsViewModel:IMapFrom<Product>
    {
        public string Name { get; set; }
    }
}