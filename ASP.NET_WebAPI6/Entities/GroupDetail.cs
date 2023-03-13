using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gestion_scolaire.Entities
{
    public class GroupDetail
{
        public int Id { get; set; }
        [JsonIgnore]
        public Groupe Groupe { get; set; }
        [ForeignKey("Groupe")]
        public int GroupeId { get; set; }
        public Teacher Teacher { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }


    }
}
