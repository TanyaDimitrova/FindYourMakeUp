namespace FindYourMakeUp.Data.UoW
{
    using FindYourMakeUp.Data.Contracts.Repositories;
    using FindYourMakeUp.Data.Models;

    public interface IData
    {
        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
