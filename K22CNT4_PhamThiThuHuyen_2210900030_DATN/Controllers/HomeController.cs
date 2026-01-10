using System.Diagnostics;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
