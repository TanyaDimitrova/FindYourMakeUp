﻿using FindYourMakeUp.Data.Models;
using FindYourMakeUp.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindYourMakeUp.Web.ViewModels
{
    public class ProductListViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}