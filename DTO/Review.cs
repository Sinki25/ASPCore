using System;
using System.Runtime.Serialization;

namespace ApiCore.DTO
{
    [DataContract]
    public class Review
    {
        [DataMember(Name = "id", IsRequired = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "createOn", IsRequired = false)]
        public string CreatedOn { get; set; }

        [DataMember(Name = "authorId", IsRequired = true)]
        public Guid AuthorId { get; set; }

        [DataMember(Name = "mark", IsRequired = true)]
        public int Mark { get; set; }

        [DataMember(Name = "articleId", IsRequired = true)]
        public Guid ArticleId { get; set; }
    }
}