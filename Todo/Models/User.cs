using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class User
    {
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(4)]
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
