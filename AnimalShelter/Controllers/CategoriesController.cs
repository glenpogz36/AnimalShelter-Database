using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
  public class CategoriesController : Controller
  {
      [HttpGet("/categories")]
      public ActionResult Index()
      {
          List<Category> allCategory = Category.GetAll();
          return View(allCategory);
      }

      [HttpGet("/categories/new")]
      public ActionResult New()
      {
          return View();
      }

      [HttpPost("/categories")]
      public ActionResult Create(string categoryType)
      {
          Category newCategory = new Category(categoryType);
          List<Category> allCategories = Category.GetAll();
          return View("Index", allCategories);
      }

      [HttpGet ("/catergories/{id}")]
      public ActionResult Show(int id)
      {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Category selectedCategory = Category.Find(id);
          List<Animal> categoryAnimals = selectedCategory.GetAnimals();
          model.Add("category", selectedCategory);
          model.Add("animals", categoryAnimals);
          return View(model);
      }

      [HttpPost("/categories/{categoryId}/animals")]
      public ActionResult Create(int categoryId, string animalName, string animalSex, string animalDueDate, string animalBreed, int animalId)
      {
          Dictionary<string, object>model = new Dictionary<string, object>();
          Category foundCategory = Category.Find(categoryId);
          Animal newAnimal = new Animal(animalName, animalSex, animalDueDate, animalBreed, animalId);
          foundCategory.AddAnimal(newAnimal);
          List<Animal> categoryAnimals = foundCategory.GetAnimals();
          model.Add("animals", categoryAnimals);
          model.Add("category", foundCategory);
          return View("Show", model);
        }

  }
}
