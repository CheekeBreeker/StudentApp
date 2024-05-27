using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCore.Models;
using StudentWebAPI.Models;

namespace StudentWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private StudentAppContext _context;

        public GroupController(StudentAppContext context) 
        {
            _context = context;
        }

        [HttpPut]
        public void Put([FromBody] GroupAddDto dto)
        {
            var group = new Group { Name = dto.Name, OwnerId = dto.OwnerId };

            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        [HttpPost]
        public void Post(Group group)
        {
            var existGroup = _context.Groups.AsNoTracking().FirstOrDefault(x => x.Id == group.Id);

            if(existGroup != null)
            {
                _context.Groups.Update(group);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        [Route("GetOne")]
        public Group? Get(int id)
        {
            return _context.Groups.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        [Route("GetAll")]
        public List<Group> GetAll([FromBody] GroupFilterDto group)
        {
            var query = _context.Groups.AsQueryable();

            if(group.Id != null)
            {
                query = query.Where(x => x.Id == group.Id);
            }

            if(group.Name != null)
            {
                query = query.Where(x => x.Name.Contains(group.Name));
            }

            if (group.OwnerId != null)
            {
                query = query.Where(x => x.OwnerId == group.OwnerId);
            }

            return query.ToList();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);

            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
            }
        }
    }
}