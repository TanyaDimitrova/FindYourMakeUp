﻿namespace FindYourMakeUp.Web.Areas.Administration.ViewModels
{
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Web.Areas.Administration.ViewModels.Base;
    using FindYourMakeUp.Web.Infrastructure.Mapping;

    public class ManufacturerViewModel : AdministrationViewModel, IMapFrom<Manufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
