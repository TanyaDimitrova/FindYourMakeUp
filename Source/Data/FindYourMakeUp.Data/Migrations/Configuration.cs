namespace FindYourMakeUp.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using FindYourMakeUp.Data.Models;

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

            var hairCategories = context.Categories.Where(p => p.ParentCategory.Name == "Hair").ToList();

            foreach (var purpose in hairCategories)
            {
                purpose.ProductTypes.Add(shampooType);
                purpose.ProductTypes.Add(condType);
                purpose.ProductTypes.Add(maskType);
            }

            context.Categories
                   .Where(p => p.Name == "Hygiene" && p.ParentCategory.Name == "Body")
                   .First()
                   .ProductTypes
                   .Add(new ProductType { Name = "Shower" });

            context.Categories
                   .Where(p => p.Name == "Skin Care" && p.ParentCategory.Name == "Body")
                   .First()
                   .ProductTypes
                   .Add(new ProductType { Name = "Body Lotion" });

            var makeup = context.Categories
                                .Where(p => p.Name == "Make Up" && p.ParentCategory.Name == "Face")
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