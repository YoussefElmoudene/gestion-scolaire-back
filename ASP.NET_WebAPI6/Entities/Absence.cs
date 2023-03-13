using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_scolaire.Entities
{
    public class Absence
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public Groupe Groupe { get; set; }
        [ForeignKey("Groupe")]
        public int GroupeId { get; set; }

        public Student Student { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Module Module { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
    }
}
