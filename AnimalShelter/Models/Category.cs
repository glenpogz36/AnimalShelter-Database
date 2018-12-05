using System.Collections.Generic;

namespace AnimalShelter.Models
{
  public class Category
  {
      private static List<Category> _instances= new List<Category> {};
      private string _type;
      private int _id;
      private List<Animal> _animals;

      public Category(string type)
      {
          _type = type;
          _instances.Add(this);
          _id = _instances.Count;
          _animals = new List<Animal>{};
      }

      public string GetType()
      {
          return _type;
      }
      public int GetId()
      {
          return _id;
      }
      public void AddAnimal(Animal animal)
      {
          _animals.Add(animal);
      }
      public static void ClearAll()
      {
          _instances.Clear();
      }
      public static List<Category> GetAll()
      {
          return _instances;
      }
      public static Category Find(int searchId)
      {
          return _instances[searchId-1];
      }
      public List<Animal> GetAnimals()
      {
          return _animals;
      }
    }
  }
