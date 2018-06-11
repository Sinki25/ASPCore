using System;
using LinqToDB.Mapping;

namespace ApiCore.BO
{
    [Table("Article")]
    public class Article
    {
        public Article()
        {
        }

        public Article(string title, string text, Guid userId, Guid categoryId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Text = text;
            UserId = userId;
            CategoryId = categoryId;
            CreatedOn = DateTime.Now;
        }

        public Article(DTO.Article article)
        {
            Id = Guid.NewGuid();
            Title = article.Title;
            CreatedOn = DateTime.Now;
            Text = article.Text;
            IsAccepted = null;
            CategoryId = article.CategoryId;
            UserId = article.AuthorId;
        }
        [Column, NotNull, PrimaryKey] public Guid Id { get; set; }
        [Column, NotNull] public DateTime CreatedOn { get; set; }
        [Column, NotNull] public string Title { get; set; }
        [Column, NotNull] public string Text { get; set; }
        [Column, Nullable] public bool? IsAccepted { get; set; }
        [Column, NotNull] public Guid UserId { get; set; }
        [Column, NotNull] public Guid CategoryId { get; set; }
    }
}