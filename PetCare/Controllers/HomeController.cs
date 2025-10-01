using Microsoft.AspNetCore.Mvc;

namespace PetCare.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
