using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gestion_scolaire.Entities
{
    public class Student: User
{
        public Groupe Groupe { get; set; }
        [ForeignKey("Groupe")]
        public int GroupeId { get; set; }
        [JsonIgnore]
        public ICollection<Note> Notes { get; set; }
        [JsonIgnore]
        public ICollection<Absence> Absences { get; set; }
}
}
