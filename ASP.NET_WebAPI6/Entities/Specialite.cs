using System.Text.Json.Serialization;

namespace gestion_scolaire.Entities
{
    public class Specialite
{
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Module> Modules { get; set; }
        [JsonIgnore]
        public ICollection<Groupe> Groupes { get; set; }
        [JsonIgnore]
        public ICollection<Teacher> Teachers { get; set; }
    }
}
