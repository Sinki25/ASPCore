using System;
using System.Runtime.Serialization;

namespace ApiCore.DTO
{
    [DataContract]
    public class ArticleToUserBinding
    {
        [DataMember(Name = "id", IsRequired = true)]
        public Guid Id { get; set; }

        [DataMember(Name = "articleId", IsRequired = true)]
        public Guid ArticleId { get; set; }

        [DataMember(Name = "deadline", IsRequired = false)]
        public string DeadLine { get; set; }
    }
}