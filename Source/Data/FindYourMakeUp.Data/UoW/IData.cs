namespace FindYourMakeUp.Data.UoW
{
    using FindYourMakeUp.Data.Contracts.Repositories;
    using FindYourMakeUp.Data.Models;

    public interface IData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Manufacturer> Manufacturers { get; }

        IRepository<Product> Products { get; }

        IRepository<ProductType> ProductTypes { get; }

        IRepository<Purpose> Purposes { get; }

        IRepository<Review> Reviews { get; }

        int SaveChanges();
    }
}
