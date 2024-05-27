using Microsoft.EntityFrameworkCore;
using StudentCore.Models;

namespace StudentWebAPI.Models
{
    public class StudentAppContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public StudentAppContext(DbContextOptions<StudentAppContext> options) : base(options)
        {

        }
    }
}
