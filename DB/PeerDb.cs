using System;
using System.Collections.Generic;
using System.Linq;
using ApiCore.BO;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace ApiCore.DB
{
    public class PeerDb : DataConnection
    {
        private readonly List<Type> _initializedTables = new List<Type>();

        public PeerDb() : base("PostgreSQL")
        {
            Initialize();
        }

        public ITable<User> Users => GetTable<User>();

        public ITable<Review> Reviews => GetTable<Review>();

        public ITable<Category> Categories => GetTable<Category>();

        public ITable<Article> Articles => GetTable<Article>();

        public ITable<CategoryToUserBinding> CategoryToUserBindings => GetTable<CategoryToUserBinding>();

        public ITable<ArticleToUserBinding> ArticleToUserBindings => GetTable<ArticleToUserBinding>();

        private void CheckTableExistsOrCreateNew<T>()
        {
            var tType = typeof(T);
            var name = (tType.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute)
                ?.Name;
            if (_initializedTables.Contains(tType))
                return;

            var query = string.Format(
                "select exists (select 1 from information_schema.tables where table_name = '{0}');", name);
            var tableExist = this.Query<bool>(query).Single();
            if (!tableExist)
                this.CreateTable<T>();

            _initializedTables.Add(tType);
        }

        private void Initialize()
        {
            CheckTableExistsOrCreateNew<Category>();
            if (!Categories.Any())
            {
                this.Insert(new Category("Category 1"));
                this.Insert(new Category("Category 2"));
                this.Insert(new Category("Category 3"));
            }

            CheckTableExistsOrCreateNew<User>();
            CheckTableExistsOrCreateNew<Article>();
            CheckTableExistsOrCreateNew<Review>();
            CheckTableExistsOrCreateNew<CategoryToUserBinding>();
            CheckTableExistsOrCreateNew<ArticleToUserBinding>();
        }
    }
}