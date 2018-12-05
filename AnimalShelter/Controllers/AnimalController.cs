using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet ("/categories/{categoryId}/animals/new")]
        public ActionResult New(int categoryId)
        {
           Category category = Category.Find(categoryId);
           return View (category);
        }

      [HttpGet("/categories/{categoryId}/animals/{animalId}")]
    public ActionResult Show(int categoryId, int animalId)
    {
      Animal animal = Animal.Find(animalId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category category = Category.Find(categoryId);
      model.Add("animal", animal);
      model.Add("category", category);
      return View(model);
    }

    [HttpPost("/animals/delete")]
    public ActionResult DeleteAll()
    {
      Animal.ClearAll();
      return View();
    }
    }
}