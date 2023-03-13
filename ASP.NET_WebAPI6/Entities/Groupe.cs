using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gestion_scolaire.Entities
{
    public class Groupe
{
        public int Id { get; set; }
        public string Name { get; set; }
        public Specialite Specialite { get; set; }
        [ForeignKey("Specialite")]
        public int SpecialiteId { get; set; }
        public int NrStudent { get; set; }
        public ICollection<GroupDetail> GroupDetails { get; set; }
        [JsonIgnore]
        public ICollection<Student> Students { get; set; }
        [JsonIgnore]
        public ICollection<Absence> Absences { get; set; }
        [JsonIgnore]
        public ICollection<Seance> Seances { get; set; }

    }
}
