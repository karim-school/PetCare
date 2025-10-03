using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;
using PetCareAPI.Views;

namespace PetCare.Controllers;

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
        response = await client.GetAsync($"http://localhost:5000/customers/{id}/pets");
        ViewBag.Pets = await response.Content.ReadAsAsync<PetView[]>();
        return View("Index");
    }
}
