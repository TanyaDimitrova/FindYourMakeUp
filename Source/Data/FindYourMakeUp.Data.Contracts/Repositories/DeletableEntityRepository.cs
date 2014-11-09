namespace FindYourMakeUp.Data.Contracts.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    using FindYourMakeUp.Data.Contracts.Models;

     public class DeletableEntityRepository<T> : GenericEFRepository<T>, IDeletableEntityRepository<T>
         where T : class, IDeletableEntity
     {
         public DeletableEntityRepository(DbContext context)
             : base(context)
         {
         }

         public override IQueryable<T> All()
         {
             return base.All().Where(x => !x.IsDeleted);
         }

         public IQueryable<T> AllWithDeleted()
         {
             return base.All();
         }
     }
 }