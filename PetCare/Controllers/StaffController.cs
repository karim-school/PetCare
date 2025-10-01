using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCare.Controllers;

[Route("staff")]

public class StaffController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/staff");
        ViewBag.Staffs = await response.Content.ReadAsAsync<Staff[]>();
        return View();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Single(int id)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"http://localhost:5000/staff/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();
        ViewBag.Staff = await response.Content.ReadAsAsync<Staff>();
        return View("Index");
    }
}
