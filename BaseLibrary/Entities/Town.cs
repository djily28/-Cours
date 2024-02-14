
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        //Relationship : One to Many with Consultant
        [JsonIgnore]
        public List<Consultant>? Consultants { get; set; }

        //Many to one relationship with city
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}

