using StudentCore.Models;
using StudentWebAPI.Models;

namespace StudentMvc.Models
{
    public class GroupViewModel
    {
        public List<GroupDto> Groups { get; set; }

        public GroupFilterDto Filter { get; set; }
    }
}
