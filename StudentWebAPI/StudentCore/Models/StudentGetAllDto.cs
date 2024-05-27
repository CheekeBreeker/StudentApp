using StudentWebAPI.Models;

namespace StudentCore.Models
{
    public class StudentGetAllDto
    {
        public List<StudentGetDto> Students { get; set; }

        public List<GroupDto> Groups { get; set; }
    }
}
