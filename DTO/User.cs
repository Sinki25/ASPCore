using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApiCore.DTO
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "id", IsRequired = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Name = "reviews", IsRequired = false)]
        public List<Review> Reviews { get; set; }

        [DataMember(Name = "articles", IsRequired = false)]
        public List<Article> Articles { get; set; }

        [DataMember(Name = "registeredOn", IsRequired = false)]
        public string RegisteredOn { get; set; }

        [DataMember(Name = "referralId", IsRequired = false)]
        public Guid? ReferralId { get; set; }

        [DataMember(Name = "rating", IsRequired = false)]
        public int Rating { get; set; }

        [DataMember(Name = "password", IsRequired = true)]
        public string Password { get; set; }

        [DataMember(Name= "articlesForReview", IsRequired = false)]
        public Guid [] ArticlesForReview { get; set; }

        [DataMember(Name = "reviewedArticles", IsRequired = false)]
        public Guid[] ReviewedArticles { get; set; }
    }
}