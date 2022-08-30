using AirlineWebApp.customMiddleware;
using AirlineWebApp.Data;
using AirlineWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Diagnostics;

namespace AirlineWebApp.Controllers
{
    
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            ViewData["uName"] = HttpContext.Session.GetString("Name");
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}