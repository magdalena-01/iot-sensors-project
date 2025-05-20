using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROEKT_SENZORI.Models;

namespace PROEKT_SENZORI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
       

        public IActionResult Index()
        {
            var allData = _context.DATA.ToList();
            var latest = allData.OrderByDescending(d => d.Time).FirstOrDefault();

            var temps = allData
                .SelectMany(d => new[] { d.Sensor1, d.Sensor2, d.Sensor3, d.Sensor4 })
                .Where(v => !string.IsNullOrEmpty(v))
                .Select(v => double.Parse(v.Split(',')[0].Replace("°C", "").Trim()))
                .ToList();

            var latestTemp = latest != null ? double.Parse(latest.Sensor1.Split(',')[0].Replace("°C", "").Trim()) : 0;

            var model = new DashboardViewModel
            {
                TotalSensors = allData.Count,
                CurrentTemp = latestTemp,
                MinTemp = temps.Min(),
                MaxTemp = temps.Max()
            };

            return View(model);
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
}
