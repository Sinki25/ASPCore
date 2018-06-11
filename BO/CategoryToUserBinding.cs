using System;
using LinqToDB.Mapping;

namespace ApiCore.BO
{
    [Table("CategoryToUserBinding")]
    public class CategoryToUserBinding
    {
        public CategoryToUserBinding()
        {
        }

        public CategoryToUserBinding(Guid userId, Guid categoryId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CategoryId = categoryId;
        }
        [Column, NotNull, PrimaryKey] public Guid Id { get; set; }
        [Column, NotNull] public Guid UserId { get; set; }
        [Column, NotNull] public Guid CategoryId { get; set; }
    }
}