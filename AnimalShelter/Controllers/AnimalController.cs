using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet ("/animals")]
        public ActionResult Index()
        {
            List<Animal> allAnimals = Animal.GetAll();
            return View (allAnimals);
        }
    }
}