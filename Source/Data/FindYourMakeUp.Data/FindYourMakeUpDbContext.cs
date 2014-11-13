namespace FindYourMakeUp.Data
{
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;

    using FindYourMakeUp.Data.Contracts.Models;
    using FindYourMakeUp.Data.Migrations;
    using FindYourMakeUp.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FindYourMakeUpDbContext : IdentityDbContext<ApplicationUser>
    {
        public FindYourMakeUpDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FindYourMakeUpDbContext, Configuration>());
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

        public IDbSet<ProductType> ProductTypes { get; set; }

        public IDbSet<Review> Reviews { get; set; }

        public static FindYourMakeUpDbContext Create()
        {
            return new FindYourMakeUpDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                var provider = CultureInfo.InvariantCulture;
                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
