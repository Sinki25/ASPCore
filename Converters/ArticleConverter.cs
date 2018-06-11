using System;
using System.Globalization;
using ApiCore.DTO;

namespace ApiCore.Converters
{
    public static class ArticleConverter
    {
        public static Article ToDto(this BO.Article article)
        {
            return new Article
            {
                Id = article.Id,
                AuthorId = article.UserId,
                CreatedOn = article.CreatedOn.ToString("dd/MM/yyyy"),
                Title = article.Title,
                CategoryId = article.CategoryId,
                IsAccepted = article.IsAccepted,
                Text = article.Text
            };
        }

        public static BO.Article ToBo(this Article article)
        {
            return new BO.Article
            {
                Id = article.Id,
                UserId = article.AuthorId,
                CreatedOn = DateTime.ParseExact(article.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Title = article.Title,
                CategoryId = article.CategoryId,
                IsAccepted = article.IsAccepted,
                Text = article.Text
            };
        }
    }
}