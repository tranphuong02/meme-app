using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Core.Model.Entities;

namespace Core.Model
{
    public class MemeContext : DbContext
    {
        public MemeContext() : base("MemeContext") { }

        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Photo> Photo { get; set; }
    }
}
