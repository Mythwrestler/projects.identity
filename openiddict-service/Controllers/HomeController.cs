using Microsoft.AspNetCore.Mvc;

namespace Identity.OpenIddict.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}