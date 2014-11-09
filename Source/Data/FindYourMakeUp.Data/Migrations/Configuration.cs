namespace FindYourMakeUp.Data.Migrations
{
    using FindYourMakeUp.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FindYourMakeUpDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            ContextKey = "FindYourMakeUp.Data.ApplicationDbContext";
        }

        protected override void Seed(FindYourMakeUpDbContext context)
        {
            if (!context.Categories.Any())
            {
                SeedCategories(context);
            }

            if (!context.Purposes.Any())
            {
                SeedPurposes(context);
            }

            if (!context.ProductTypes.Any())
            {
                SeedProductTypes(context);
            }

            if (!context.Manufacturers.Any())
            {
                SeedManufacturers(context);
            }
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

            var purposesHair = context.Purposes.Where(p => p.Category.Name == "Hair").ToList();

            foreach (var purpose in purposesHair)
            {
                purpose.ProductTypes.Add(shampooType);
                purpose.ProductTypes.Add(condType);
                purpose.ProductTypes.Add(maskType);
            }

            context.Purposes
                   .Where(p => p.Name == "Hygiene" && p.Category.Name == "Body")
                   .First()
                   .ProductTypes
                   .Add(new ProductType { Name = "Shower" });

            context.Purposes
                   .Where(p => p.Name == "Skin Care" && p.Category.Name == "Body")
                   .First()
                   .ProductTypes
                   .Add(new ProductType { Name = "Body Lotion" });

            var makeup = context.Purposes
                                .Where(p => p.Name == "Make Up" && p.Category.Name == "Face")
                                .First();

            makeup.ProductTypes.Add(new ProductType { Name = "Lip stick & Gloss" });
            makeup.ProductTypes.Add(new ProductType { Name = "Mascara" });
            makeup.ProductTypes.Add(new ProductType { Name = "Foundation" });
            makeup.ProductTypes.Add(new ProductType { Name = "Eye shadow" });

            context.SaveChanges();
        }

        private void SeedCategories(FindYourMakeUpDbContext context)
        {
            context.Categories.Add(new Category { Name = "Hair" });
            context.Categories.Add(new Category { Name = "Body" });
            context.Categories.Add(new Category { Name = "Face" });
            context.Categories.Add(new Category { Name = "Hands" });
            context.SaveChanges();
        }

        private static void SeedPurposes(FindYourMakeUpDbContext context)
        {
            var hairCategoryId = context.Categories.Where(c => c.Name == "Hair").First().Id;
            context.Purposes.Add(new Purpose { Name = "Oily", CategoryId = hairCategoryId });
            context.Purposes.Add(new Purpose { Name = "Dry", CategoryId = hairCategoryId });
            context.Purposes.Add(new Purpose { Name = "Weak", CategoryId = hairCategoryId });
            context.Purposes.Add(new Purpose { Name = "Colored", CategoryId = hairCategoryId });

            var bodyCategoryId = context.Categories.Where(c => c.Name == "Body").First().Id;
            context.Purposes.Add(new Purpose { Name = "Hygiene", CategoryId = bodyCategoryId });
            context.Purposes.Add(new Purpose { Name = "Skin Care", CategoryId = bodyCategoryId });

            var faceCategoryId = context.Categories.Where(c => c.Name == "Face").First().Id;
            context.Purposes.Add(new Purpose { Name = "Make Up", CategoryId = faceCategoryId });
            context.Purposes.Add(new Purpose { Name = "Skin Care", CategoryId = faceCategoryId });

            var handsCategoryId = context.Categories.Where(c => c.Name == "Hands").First().Id;
            context.Purposes.Add(new Purpose { Name = "Nail Lacquer", CategoryId = handsCategoryId });
            context.Purposes.Add(new Purpose { Name = "Skin Care", CategoryId = handsCategoryId });

            context.SaveChanges();
        }
    }
}