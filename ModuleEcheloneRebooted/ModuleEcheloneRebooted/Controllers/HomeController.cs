using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ModuleEcheloneRebooted.Models;
using static ModuleEcheloneRebooted.Helpers;

namespace ModuleEcheloneRebooted.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //[Route("")]
        //[Route("Home")]
        [Route( "{controller=Home}/{action=Index}")]

        public IActionResult Index()
        {
            return View();
        }
        [Route("Home/AboutUs")]
        public IActionResult AboutUs()
        { 
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}