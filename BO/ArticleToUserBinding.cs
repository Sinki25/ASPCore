using System;
using LinqToDB.Mapping;

namespace ApiCore.BO
{
    [Table("ArticleToUserBinding")]
    public class ArticleToUserBinding
    {
        public ArticleToUserBinding()
        {
        }

        public ArticleToUserBinding(Guid userId, Guid articleId, DateTime deadline)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ArticleId = articleId;
            Accepted = null;
            Deadline = deadline;
        }

        [Column, NotNull, PrimaryKey] public Guid Id { get; set; }

        [Column, NotNull] public Guid UserId { get; set; }

        [Column, NotNull] public Guid ArticleId { get; set; }

        [Column, Nullable] public bool? Accepted { get; set; }

        [Column, NotNull] public DateTime Deadline { get; set; }
    }
}