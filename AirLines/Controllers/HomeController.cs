using AirLines.customMiddleware;
using AirLines.Data;
using AirLines.Models;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Diagnostics;

namespace AirLines.Controllers
{
    
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("Name");
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}