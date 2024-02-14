
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Department : BaseEntity
    {
        // One to many relationship with Branch
        [JsonIgnore]
        public List<Branch>? Branches { get; set; }


    }
}
