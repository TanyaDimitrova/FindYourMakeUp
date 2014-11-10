namespace FindYourMakeUp.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using FindYourMakeUp.Data.Contracts.Models;
    using FindYourMakeUp.Data.Contracts.Repositories;

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

         public override void Delete(T entity)
         {
            entity.DeletedOn = DateTime.Now;
            entity.IsDeleted = true;

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
         }
     }
 }