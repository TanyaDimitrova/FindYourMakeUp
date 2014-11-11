namespace FindYourMakeUp.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using FindYourMakeUp.Data.Contracts.Repositories;
    using FindYourMakeUp.Data.Models;
    using FindYourMakeUp.Data.Repositories;
    using FindYourMakeUp.Data.UoW;

    public class FindYourMakeUpData : IFindYourMakeUpData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public FindYourMakeUpData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public DbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }


        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Manufacturer> Manufacturers
        {
            get { return this.GetRepository<Manufacturer>(); }
        }

        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }

        public IRepository<ProductType> ProductTypes
        {
            get { return this.GetRepository<ProductType>(); }
        }
        
        public IRepository<Review> Reviews
        {
            get { return this.GetRepository<Review>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(GenericEFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
