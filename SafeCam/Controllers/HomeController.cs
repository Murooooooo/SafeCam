using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SafeCam.DAL;
using SafeCam.Models;

namespace SafeCam.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    

    public IActionResult Index()
    {
        var Members = _context.Members.ToList();
        return View(Members);
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
