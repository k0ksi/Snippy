using System;
using System.Collections.Generic;
using Snippy.Data.Repositories;
using Snippy.Models;

namespace Snippy.Data.UnitOfWork
{
    public class SnippyData : ISnippyData
    {
        private ISnippyDbContext context;
        private IDictionary<Type, object> repositories;

        public SnippyData(ISnippyDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Snippet> Snippets
        {
            get { return this.GetRepository<Snippet>(); }
        }

        public IRepository<Language> Languages
        {
            get { return this.GetRepository<Language>(); }
        }

        public IRepository<Label> Labels
        {
            get { return this.GetRepository<Label>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                var repository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}