using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_scolaire.Entities
{
    public class Seance
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Groupe Groupe { get; set; }
        [ForeignKey("Groupe")]
        public  int GroupId { get; set; }
        public Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Module Module { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DaysOfWeek { get; set; }
        public bool AllDay { get; set; }

    }
}



