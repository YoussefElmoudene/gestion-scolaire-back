using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gestion_scolaire.Entities
{
    public class Module
{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Coef { get; set; }
        public Specialite Specialite { get; set; }

        [ForeignKey("Specialite")]
        public int SpecialiteId { get; set; }
        [JsonIgnore]
        public ICollection<Note> Notes { get; set; }
        [JsonIgnore]
        public ICollection<Absence> Absences { get; set; }
        [JsonIgnore]
        public ICollection<Seance> Seances { get; set; }

    }
}
