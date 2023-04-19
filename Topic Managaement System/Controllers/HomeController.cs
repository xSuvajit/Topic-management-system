using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TMS.DTO;
using TMS.Manager.Contract;
using Topic_Managaement_System.Models;

namespace Topic_Managaement_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        private readonly IUtilities utilities;
        private readonly IExcelUtilities excel;
        private readonly string? strCon;

        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration, 
             IUtilities _utilities, IExcelUtilities _excel)
        {
            _logger = logger;
            configuration = _configuration;  
            utilities = _utilities;
            excel = _excel;
            strCon = configuration.GetConnectionString("connStr");
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Registration() { 
            Topics topics = new Topics();
            topics.GetTopics = utilities.GetAllTopics(strCon);
            return View(topics);
        }

        public string GetValue()
        {
            return utilities.GetValue(strCon);
        }

        public string GetSomeValue()
        {
            return utilities.GetSomeValue(strCon);
        }

        public string ExportToExcel()
        {
            Topics topics = new Topics();
            topics.GetTopics = utilities.GetAllTopics(strCon);
            var result = excel.CreateExcel(topics);
            return result;
        }
    }
}