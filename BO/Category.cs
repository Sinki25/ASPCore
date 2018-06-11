using System;
using LinqToDB.Mapping;

namespace ApiCore.BO
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
        }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        [Column, NotNull, PrimaryKey] public Guid Id { get; set; }
        [Column, NotNull] public string Name { get; set; }
    }
}