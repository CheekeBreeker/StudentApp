using AutoMapper;
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
    public class StudentController : Controller
    {
        private StudentAppContext _context;

        private readonly IMapper _mapper;

        public StudentController(IMapper mapper, StudentAppContext context) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPut]
        public void Put([FromBody] StudentAddDto model)
        {
            var student = _mapper.Map<StudentAddDto, Student>(model);

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        [HttpPost]
        public void Post(StudentEditDto student)
        {
            var existStudent = _context.Students.FirstOrDefault(x => x.Id == student.Id);

            if(existStudent != null)
            {
                _mapper.Map(student, existStudent);

                _context.Students.Update(existStudent);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        [Route("GetOne")]
        public StudentGetDto? Get(int id)
        {
            var student = _context.Students.Include(p => p.Group).FirstOrDefault(x => x.Id == id);

            if (student == null) return null;

            return StudentGetDto(student);
        }

        [HttpPost]
        [Route("GetAll")]
        public StudentGetAllDto GetAll([FromBody] StudentFilterDto filter)
        {
            Thread.Sleep(1000);
            var query = _context.Students.AsQueryable();

            if(filter.FirstName != null)
            {
                query = query.Where(x => x.FirstName.Contains(filter.FirstName));
            }

            if (filter.LastName != null)
            {
                query = query.Where(x => x.LastName.Contains(filter.LastName));
            }

            if (filter.Email != null)
            {
                query = query.Where(x => x.Email.Contains(filter.Email));
            }

            if (filter.GroupId != null)
            {
                query = query.Where(x => x.GroupId == filter.GroupId);
            }

            var students = query.ToList()
                .Select(student => StudentGetDto(student))
                .ToList();

            var model = new StudentGetAllDto
            {
                Students = students,
                Groups = _context.Groups.Select(x => new GroupDto { Id = x.Id, Name = x.Name }).ToList()
            };

            return model;
        }

        private StudentGetDto StudentGetDto(Student student)
        {
            var result = _mapper.Map<StudentGetDto>(student);

            return result;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}