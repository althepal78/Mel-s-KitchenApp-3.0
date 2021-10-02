using MelsKitchen.Context;
using MelsKitchen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MelsKitchen.Controllers
{
    public class RecipeController : Controller
    {
        public ChefContext DBContext;

        public RecipeController(ChefContext context)
        {
            DBContext = context;
        }

        public Chef ChefInDB()
        {
            return DBContext.Chefs.FirstOrDefault(us => us.ChefID == HttpContext.Session.GetInt32("ID")); 
        }


        public IActionResult Index()
        {

            ViewBag.AllRecipes = DBContext.Recipes
                                .Include(rm => rm.RecipeMaker);

            ViewBag.ChefInDb = ChefInDB();

            return View();
        }

        [HttpGet]
        public IActionResult AddRecipe()
        {
            ViewBag.ChefInDb = ChefInDB();
            return View();
        }

        [HttpPost]
        public IActionResult CreateDish(Recipe dish)
        {
            if (ModelState.IsValid)
            {

                // Finding the person that is loggedin
                var userInDb = ChefInDB();

                // the chef that makes the dish is a whole person recipe maker is the person logged in
                dish.RecipeMaker = userInDb;

                // add and save to the database. 
                DBContext.Recipes.Add(dish);
                DBContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("AddRecipe");
        }


        [HttpGet]
        public IActionResult ShowDish(int id)
        {
            //getting the recipe we clicked on
            ViewBag.ShowDish = DBContext.Recipes
                                    .Include(rm => rm.RecipeMaker)
                                    .FirstOrDefault(rec => rec.RecipeID == id);

            ViewBag.ChefInDb = ChefInDB();
  
            return View();
        }

        [HttpGet]
        public IActionResult EditRecipe(int id)
        {

            var recipe = DBContext.Recipes.FirstOrDefault(rid => rid.RecipeID == id);
            ViewBag.ChefInDb = ChefInDB();

            return View("EditRecipe", recipe);
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe, int id)
        {
            var rec = DBContext.Recipes.FirstOrDefault(rid => rid.RecipeID == id);
            if(rec != null)
            {
                if (ModelState.IsValid)
                {
                    //updating the recipe
                    rec.RecipeName = recipe.RecipeName;
                    rec.Ingredients = recipe.Ingredients;
                    rec.Directions = recipe.Directions;
                    rec.UpdatedAt = DateTime.Now;

                    //now savechanges
                    DBContext.SaveChanges();

                    return RedirectToAction("ShowDish", "Recipe", new { @id = id });
                }
            }

            return View("EditRecipe", recipe);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var rec = DBContext.Recipes.FirstOrDefault(rid => rid.RecipeID == id);
            DBContext.Recipes.Remove(rec);
            DBContext.SaveChanges();



            return RedirectToAction("Index", "Recipe");
        }


    }
}
