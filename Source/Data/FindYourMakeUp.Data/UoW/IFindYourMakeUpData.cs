namespace FindYourMakeUp.Data.UoW
{
    using System.Data.Entity;

    using FindYourMakeUp.Data.Contracts.Repositories;
    using FindYourMakeUp.Data.Models;

    public interface IFindYourMakeUpData
    {
        DbContext Context { get; }

        IRepository<ApplicationUser> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Manufacturer> Manufacturers { get; }

        IRepository<Product> Products { get; }

        IRepository<ProductType> ProductTypes { get; }

        IRepository<Review> Reviews { get; }

        int SaveChanges();
    }
}
