using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCare.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5000/customers");
            ViewBag.Customers = await response.Content.ReadAsAsync<Customer[]>();
            return View();
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Single(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5000/customers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            ViewBag.Customer = await response.Content.ReadAsAsync<Customer>();
            return View("Index");
        }
    }
}
