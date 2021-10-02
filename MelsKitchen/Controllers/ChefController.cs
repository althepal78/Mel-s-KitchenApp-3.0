using MelsKitchen.Context;
using MelsKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MelsKitchen.Controllers
{
    public class ChefController : Controller
    {
        public ChefContext DBContext;

        public ChefController(ChefContext context)
        {
            DBContext = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            ViewBag.ChefInDb = ChefInDB();
            return View();
        }

        public Chef ChefInDB()
        {
            return DBContext.Chefs.FirstOrDefault(us => us.ChefID == HttpContext.Session.GetInt32("ID")); 
        }

        //This is partial view
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult LoggedIn(Login logger)
        {
            if (ModelState.IsValid)
            {
                // verify if email is in database
                var userInDb = DBContext.Chefs.FirstOrDefault(us => us.Email == logger.LoginEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("LoggerEmail", "Invalid Email or Password");
                    return View("Index");
                }

                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(logger, userInDb.Password, logger.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoggerPassword", "Invalid Email or Password");
                    return View("Index");
                }

                //create a  session 
                HttpContext.Session.SetInt32("ID", userInDb.ChefID);

                return RedirectToAction("Dashboard");
            }

            return View("Index");
        }

        //This is partial view for Registering the new chefs
        [HttpGet]
        public IActionResult Register()
        {
            return PartialView();
        }


        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            if (ModelState.IsValid)
            {
                if(DBContext.Chefs.Any(em => em.Email == chef.Email))
                {
                    ModelState.AddModelError("Email", "Email already in system.");
                    return View("Index");
                }

                // hash passwords 
                PasswordHasher<Chef> Hasher = new PasswordHasher<Chef>();
                chef.Password = Hasher.HashPassword(chef, chef.Password);

                // time to create the chef 
                DBContext.Chefs.Add(chef);
                DBContext.SaveChanges();

                // create session so we wont be logged out
                HttpContext.Session.SetInt32("ID", chef.ChefID);

                //get to where we need to get to son 
                return RedirectToAction("Dashboard");
            }

            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
