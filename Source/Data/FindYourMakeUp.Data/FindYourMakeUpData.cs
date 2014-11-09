namespace FindYourMakeUp.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using FindYourMakeUp.Data.Contracts.Repositories;
    using FindYourMakeUp.Data.Models;

    class FindYourMakeUpData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public FindYourMakeUpData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
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
