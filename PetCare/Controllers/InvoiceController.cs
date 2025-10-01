using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCare.Controllers
{
    [Route("invoices")]
    public class InvoiceController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5000/invoices");
            ViewBag.Invoices = await response.Content.ReadAsAsync<Invoice[]>();
            return View();
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Single(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5000/invoices/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            ViewBag.Invoice = await response.Content.ReadAsAsync<Invoice>();
            return View("Index");
        }
    }
}
