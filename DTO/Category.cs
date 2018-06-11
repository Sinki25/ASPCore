using System;
using System.Runtime.Serialization;

namespace ApiCore.DTO
{
    [DataContract]
    public class Category
    {
        [DataMember(Name = "id", IsRequired = false)]
        public Guid Id { get; set; }

        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }
    }
}
