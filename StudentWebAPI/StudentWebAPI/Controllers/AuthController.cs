using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCore.Models;
using StudentWebAPI.Models;

namespace StudentWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private StudentAppContext _context;

        private IMapper _mapper;

        public AuthController(IMapper mapper, StudentAppContext context) 
        { 
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public StudentGetDto? Index([FromBody] StudentAuthDto model)
        {
            var student = _context.Students.FirstOrDefault(x => x.Email == model.Login && x.Password == model.Password);

            if (student == null) return null;

            var studentDto = _mapper.Map<StudentGetDto>(student);

            return studentDto;
        }
    }
}
