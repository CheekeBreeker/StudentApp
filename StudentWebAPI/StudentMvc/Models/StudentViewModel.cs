using StudentCore.Models;
using StudentWebAPI.Models;

namespace StudentMvc.Models
{
    public class StudentViewModel
    {
        public List<StudentGetDto> Students { get; set; }

        public List<GroupDto> Groups { get; set; }

        public StudentFilterDto Filter { get; set; }
    }
}
