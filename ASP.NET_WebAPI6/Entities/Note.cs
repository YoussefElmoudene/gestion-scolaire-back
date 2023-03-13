using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_scolaire.Entities
{
    public class Note
{
        public int Id { get; set; }
        public double StudentNote { get; set; }
        public Student Student { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Module Module { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
}
}
