using StudentCore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentWebAPI.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Group))]    
        public int GroupId { get; set; }

        public Group? Group { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
