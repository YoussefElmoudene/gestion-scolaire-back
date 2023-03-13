
namespace gestion_scolaire.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cin { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public bool IsEnabled { get; set; }
        public string Role { get; set; }
    }
}
