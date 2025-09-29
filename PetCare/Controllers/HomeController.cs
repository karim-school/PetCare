using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PetCare.Models;
using PetCareAPI.Models;

namespace PetCare.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/zipcodes");
        var zipcodes = await response.Content.ReadAsAsync<ZipCode[]>();
        Console.WriteLine($"Zip codes ({zipcodes.Length}):");
        foreach (var zipCode in zipcodes)
        {
            Console.WriteLine($"{zipCode.Code} = {zipCode.City}");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}