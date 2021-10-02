using MelsKitchen.Context;
using MelsKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MelsKitchen.Controllers
{
    public class HomeController : Controller
    {
        private ChefContext DBContext;

        public HomeController(ChefContext context)
        {
            DBContext = context;
        }

        public Chef ChefInDB()
        {
            return DBContext.Chefs.FirstOrDefault(cid => cid.ChefID == HttpContext.Session.GetInt32("ID"));
        }

        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult HeaderPartial()
        {
            ViewBag.ChefInDb = ChefInDB();
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
