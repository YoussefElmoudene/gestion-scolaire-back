using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gestion_scolaire.Entities
{
    public class Teacher: User
{
        public Specialite Specialite { get; set; }
        [ForeignKey("Specialite")]
        public int SpecialiteId { get; set; }
        [JsonIgnore]
        public ICollection<GroupDetail> GroupDetails { get; set; }
        [JsonIgnore]
        public ICollection<Seance> Seances { get; set; }

    }
}
