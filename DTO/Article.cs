using System;
using System.Runtime.Serialization;

namespace ApiCore.DTO
{
    [DataContract]
    public class Article
    {
        [DataMember(Name = "id", IsRequired = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "createOn", IsRequired = false)]
        public string CreatedOn { get; set; }

        [DataMember(Name = "title", IsRequired = true)]
        public string Title { get; set; }

        [DataMember(Name = "text", IsRequired = true)]
        public string Text { get; set; }

        [DataMember(Name = "authorId", IsRequired = false)]
        public Guid AuthorId { get; set; }

        [DataMember(Name = "categoryId", IsRequired = true)]
        public Guid CategoryId { get; set; }

        [DataMember(Name = "isAccepted", IsRequired = false)]
        public bool? IsAccepted { get; set; }
    }
}