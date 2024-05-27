using StudentCore.Models;
using StudentWebAPI.Models;

namespace StudentMvc.Models
{
    public class StudentEditViewModel
    {
        public StudentGetDto? Student { get; set; }

        public List<GroupDto>? Groups { get; set; }
    }
}
