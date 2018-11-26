using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using service_kamil.Models;

namespace service_kamil.Controllers
{
    public class HomeController : Controller
    {
        private static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Kamil(int id = 0)
        {
            int vpp;

            if (!Int32.TryParse(Request.Cookies["villages_per_page"], out vpp))
            {
                vpp = 10;
            }

            VillagesCollectionModel villages = VillagesCollectionModel.GetVillages(RequestType.Xml, "id", SortType.Asc, vpp * id, vpp);
            
            //VillagesListViewModel ViewModel = new VillagesListViewModel();
            //ViewModel.Number = 331;

            ViewData["czas"] = DateTime.Now;
            ViewData["page"] = id;
            ViewData["next_page"] = id + 1;
            ViewData["next2_page"] = id + 2;
            ViewData["last_page"] = id - 1;
            return View(villages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
