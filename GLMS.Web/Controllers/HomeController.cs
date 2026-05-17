using GLMS.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using GLMS.Web.Models;

namespace GLMS.Web.Controllers
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
            ViewBag.TotalClients = _context.Clients.Count();
            ViewBag.TotalContracts = _context.Contracts.Count();
            ViewBag.ActiveContracts = _context.Contracts.Count(c => c.Status == "Active");
            ViewBag.DraftContracts = _context.Contracts.Count(c => c.Status == "Draft");
            ViewBag.ExpiredContracts = _context.Contracts.Count(c => c.Status == "Expired");
            ViewBag.OnHoldContracts = _context.Contracts.Count(c => c.Status == "On Hold");
            ViewBag.ServiceRequests = _context.ServiceRequests.Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}