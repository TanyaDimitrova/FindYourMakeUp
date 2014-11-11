namespace FindYourMakeUp.Data.UoW
{
    using FindYourMakeUp.Data.Contracts.Repositories;
    using FindYourMakeUp.Data.Models;

    public interface IFindYourMakeUpData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Category> Categories { get; }

        IRepository<Manufacturer> Manufacturers { get; }

        IRepository<Product> Products { get; }

        IRepository<ProductType> ProductTypes { get; }

        IRepository<Review> Reviews { get; }

        int SaveChanges();
    }
}
