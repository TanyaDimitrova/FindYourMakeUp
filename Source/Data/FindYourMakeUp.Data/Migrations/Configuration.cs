namespace FindYourMakeUp.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using FindYourMakeUp.Common;
    using FindYourMakeUp.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<FindYourMakeUpDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "FindYourMakeUp.Data.ApplicationDbContext";
        }

        protected override void Seed(FindYourMakeUpDbContext context)
        {
            this.SeedRoles(context);

            if (!context.Categories.Any())
            {
                this.SeedCategories(context);
            }

            if (!context.ProductTypes.Any())
            {
                this.SeedProductTypes(context);
            }

            if (!context.Manufacturers.Any())
            {
                this.SeedManufacturers(context);
            }

            if (!context.Products.Any())
            {
                this.SeedProducts(context);
            }
        }

        private void SeedRoles(FindYourMakeUpDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
            context.SaveChanges();
        }

        private void SeedProducts(FindYourMakeUpDbContext context)
        {
            context.Products.Add(new Product
            {
                Name = "Sensibio",
                Description = "Face cream for sensitive skin",
                ManufacturerId = context.Manufacturers.Where(m => m.Name == "Bioderma").First().Id,
                ProductTypeId = context.ProductTypes.Where(p => p.Name == "Face creame").First().Id,
                CategoryId = context.Categories.Where(c => c.Name == "Face").First().Id
            });

            context.SaveChanges();
        }

        private void SeedManufacturers(FindYourMakeUpDbContext context)
        {
            context.Manufacturers.Add(new Manufacturer { Name = "Nivea" });
            context.Manufacturers.Add(new Manufacturer { Name = "Bioderma" });
            context.Manufacturers.Add(new Manufacturer { Name = "La Roche-Posay" });
            context.Manufacturers.Add(new Manufacturer { Name = "Aroma" });
            context.Manufacturers.Add(new Manufacturer { Name = "Avon" });
            context.SaveChanges();
        }

        private void SeedProductTypes(FindYourMakeUpDbContext context)
        {
            var shampooType = new ProductType { Name = "Shampoo" };
            var condType = new ProductType { Name = "Conditioner" };
            var maskType = new ProductType { Name = "Mask" };

            var hairCategories = context.Categories.Where(c => c.ParentCategory.Name == "Hair").ToList();

            foreach (var subCategory in hairCategories)
            {
                subCategory.ProductTypes.Add(shampooType);
                subCategory.ProductTypes.Add(condType);
                subCategory.ProductTypes.Add(maskType);
            }

            context.Categories
                   .Where(c => c.Name == "Hygiene" && c.ParentCategory.Name == "Body")
                   .First()
                   .ProductTypes
                   .Add(new ProductType { Name = "Shower" });

            context.Categories
                   .Where(c => c.Name == "Skin Care" && c.ParentCategory.Name == "Body")
                   .First()
                   .ProductTypes
                   .Add(new ProductType { Name = "Body Lotion" });

            var makeup = context.Categories
                                .Where(c => c.Name == "Make Up" && c.ParentCategory.Name == "Face")
                                .First();

            makeup.ProductTypes.Add(new ProductType { Name = "Lip stick & Gloss" });
            makeup.ProductTypes.Add(new ProductType { Name = "Mascara" });
            makeup.ProductTypes.Add(new ProductType { Name = "Foundation" });
            makeup.ProductTypes.Add(new ProductType { Name = "Eye shadow" });

            var faceCare = context.Categories
                              .Where(c => c.Name == "Skin Care" && c.ParentCategory.Name == "Face")
                              .First();

            makeup.ProductTypes.Add(new ProductType { Name = "Face creame" });
            context.SaveChanges();
        }

        private void SeedCategories(FindYourMakeUpDbContext context)
        {
            context.Categories.Add(new Category { Name = "Hair" });
            context.Categories.Add(new Category { Name = "Body" });
            context.Categories.Add(new Category { Name = "Face" });
            context.Categories.Add(new Category { Name = "Hands" });
            context.SaveChanges();

            var hairCategoryId = context.Categories.Where(c => c.Name == "Hair").First().Id;
            context.Categories.Add(new Category { Name = "Oily", ParentCategoryId = hairCategoryId });
            context.Categories.Add(new Category { Name = "Dry", ParentCategoryId = hairCategoryId });
            context.Categories.Add(new Category { Name = "Weak", ParentCategoryId = hairCategoryId });
            context.Categories.Add(new Category { Name = "Colored", ParentCategoryId = hairCategoryId });

            var bodyCategoryId = context.Categories.Where(c => c.Name == "Body").First().Id;
            context.Categories.Add(new Category { Name = "Hygiene", ParentCategoryId = bodyCategoryId });
            context.Categories.Add(new Category { Name = "Skin Care", ParentCategoryId = bodyCategoryId });

            var faceCategoryId = context.Categories.Where(c => c.Name == "Face").First().Id;
            context.Categories.Add(new Category { Name = "Make Up", ParentCategoryId = faceCategoryId });
            context.Categories.Add(new Category { Name = "Skin Care", ParentCategoryId = faceCategoryId });

            var handsCategoryId = context.Categories.Where(c => c.Name == "Hands").First().Id;
            context.Categories.Add(new Category { Name = "Nail Lacquer", ParentCategoryId = handsCategoryId });
            context.Categories.Add(new Category { Name = "Skin Care", ParentCategoryId = handsCategoryId });

            context.SaveChanges();
        }
    }
}