using Microsoft.AspNetCore.Mvc;
using StudentCore.Models;
using StudentMvc.Models;
using StudentMvc.Services;
using StudentWebAPI.Models;
using System.Net.Http.Json;

namespace StudentMvc.Controllers
{
    public class GroupController : Controller
    {
        private readonly HttpClientService _httpClientService;

        public GroupController(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> Index(GroupFilterDto filter)
        {
            if(User.Identity.IsAuthenticated)
            {
                int.TryParse(User.FindFirst("Id")?.Value, out int ownerId);
                filter.OwnerId = ownerId;
            }

            var groups = await _httpClientService.Post<GroupFilterDto, List<GroupDto>>("/Group/GetAll", filter);

            var viewModel = new GroupViewModel
            {
                Filter = filter,
                Groups = groups
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClientService.Delete($"/Group?id={id}");

            return RedirectToAction(nameof(Index));

            //return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            int.TryParse(User.FindFirst("Id")?.Value, out int ownerId);

            var group = new GroupAddDto { Name = name, OwnerId = ownerId };

            await _httpClientService.Put("/Group", group);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name)
        {
            var group = new GroupDto { Id = id, Name = name };

            await _httpClientService.Post("/Group", group);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _httpClientService.Get<GroupDto>($"/Group/GetOne?id={id}");

            return View(model);
        }
    }
}
