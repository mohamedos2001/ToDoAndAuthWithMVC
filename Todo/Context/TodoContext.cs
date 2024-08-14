using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Context
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo.Models.Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }


        public TodoContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;database=Todo;trusted_Connection=true;Encrypt=false");
        }
    }
}
