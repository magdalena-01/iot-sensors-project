using Microsoft.AspNetCore.Mvc;
using PROEKT_SENZORI.Models;

namespace PROEKT_SENZORI.Controllers
{
   
    public class SensorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SensorService _sensorService;

        public SensorsController(ApplicationDbContext context, SensorService sensorService)
        {
            _context = context;
            _sensorService = sensorService;
        }

        public IActionResult LatestValues()
        {
            var latestData = _context.DATA
                .OrderByDescending(d => d.Time)
                .FirstOrDefault();
            return View(latestData);
        }

        public IActionResult History()
        {
            var history = _context.DATA
                .OrderByDescending(d => d.Time)
                .Take(50)
                .ToList();
            return View(history);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult SaveSensorData()
        {
            string rawData = _sensorService.ReadSensorData();
            if (!string.IsNullOrEmpty(rawData))
            {
                string[] values = rawData.Split(',');

                if (values.Length == 8)
                {
                    var newRecord = new DATA
                    {
                        Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Sensor1 = $"{values[0]}°C, {values[1]}%",
                        Sensor2 = $"{values[2]}°C, {values[3]}%",
                        Sensor3 = $"{values[4]}°C, {values[5]}%",
                        Sensor4 = $"{values[6]}°C, {values[7]}%"
                    };

                    _context.DATA.Add(newRecord);
                    _context.SaveChanges();
                }
            }

            return Ok();
        }
        public IActionResult Types()
        {
            var latestData = _context.DATA
                .OrderByDescending(d => d.Id)
                .FirstOrDefault(); // Get the most recent row

            return View(latestData);
        }
    }
}
