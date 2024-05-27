using Microsoft.AspNetCore.Mvc;
using StudentCore.Models;
using StudentMvc.Models;
using StudentMvc.Services;
using StudentWebAPI.Models;
using System.Net.Http.Json;

namespace StudentMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClientService _httpClientService;

        public StudentController(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> Index(StudentFilterDto filter)
        {
            var model = await _httpClientService.Post<StudentFilterDto, StudentGetAllDto>("/Student/GetAll", filter);

            var viewModel = new StudentViewModel
            {
                Students = model.Students,
                Groups = model.Groups,
                Filter = filter
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClientService.Delete($"/Student?id={id}");

            return RedirectToAction(nameof(Index));

            //return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentAddDto student)
        {
            await _httpClientService.Put("/Student", student);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await GetGroups();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentEditDto student)
        {
            await _httpClientService.Post("/Student", student);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using HttpClient client = new HttpClient();

            var model = new StudentEditViewModel
            {
                Student = await _httpClientService.Get<StudentGetDto>($"/Student/GetOne?id={id}"),

                Groups = await GetGroups()
            };

            return View(model);
        }

        private async Task<List<GroupDto>> GetGroups()
        {
            var model = await _httpClientService.Post<GroupFilterDto, List<GroupDto>>("/Group/GetAll", new GroupFilterDto());

            return model;
        }
    }
}
