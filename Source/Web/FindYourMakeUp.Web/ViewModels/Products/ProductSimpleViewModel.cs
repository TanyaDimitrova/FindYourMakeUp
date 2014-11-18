using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindYourMakeUp.Web.ViewModels.Product
{
    public class ProductSimpleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}