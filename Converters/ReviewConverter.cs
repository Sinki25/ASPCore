using System;
using System.Globalization;
using ApiCore.DTO;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace ApiCore.Converters
{
    public static class ReviewConverter
    {
        public static Review ToDto(this BO.Review review)
        {
            return new Review
            {
                Id = review.Id,
                ArticleId = review.ArticleId,
                AuthorId = review.UserId,
                CreatedOn = review.CreatedOn.ToString("dd/MM/yyyy"),
                Mark = review.Mark
            };
        }

        public static BO.Review ToBo(this Review review)
        {
            return new BO.Review
            {
                Id = review.Id,
                UserId = review.AuthorId,
                CreatedOn = DateTime.ParseExact(review.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Mark = review.Mark,
                ArticleId = review.ArticleId
            };
        }
    }
}
