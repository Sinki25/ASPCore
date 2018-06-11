using System;
using LinqToDB.Mapping;

namespace ApiCore.BO
{
    [Table("Review")]
    public class Review
    {
        public Review()
        {
        }

        public Review(Guid userId, int mark, Guid articleId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Mark = mark;
            CreatedOn = DateTime.Now;
            ArticleId = articleId;
        }
        [Column, NotNull, PrimaryKey] public Guid Id { get; set; }
        [Column, NotNull] public DateTime CreatedOn { get; set; }
        [Column, NotNull] public int Mark { get; set; }
        [Column, NotNull] public Guid UserId { get; set; }
        [Column, NotNull] public Guid ArticleId { get; set; }
    }
}