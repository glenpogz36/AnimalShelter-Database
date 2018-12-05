using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AnimalShelter;

namespace AnimalShelter.Models
{
    public class Animal
    {
        private string _name;
        private string _sex;
        private string _dueDate;
        private string _breed;
        private int _id;

        public Animal(string name, string sex, string dueDate, string breed, int id)
        {
            _name = name;
            _sex = sex;
            _dueDate = dueDate;
            _breed = breed;
            _id = id;
            
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }

        public string GetSex()
        {
            return _sex;
        }

        public void SetSex(string newSex)
        {
            _sex = newSex;
        }

        public string GetDueDate()
        {
            return _dueDate;
        }

        public void SetDueDate(string newDueDate)
        {
            _dueDate = newDueDate;
        }

        public string GetBreed()
        {
            return _breed;
        }

        public void SetBreed(string newBreed)
        {
            _breed = newBreed;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int newId)
        {
            _id = newId;
        }

        public static List<Animal> GetAll()
        {
            List<Animal> allAnimals = new List<Animal> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM animals;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                
                string name = rdr.GetString(1);
                string sex = rdr.GetString(2);
                DateTime dueDate = (DateTime) rdr.GetDateTime(3);
                string breed = rdr.GetString(4);
                int id= rdr.GetInt32(5);

                Animal newAnimal = new Animal(name, sex, dueDate.ToString("MM/d/yyyy"), breed, id);
                allAnimals.Add(newAnimal);
                
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAnimals;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM animals;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();

            }
        }

        public static Animal Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM 'animals' WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            @thisId.Value = id;
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int animalId = 0;
            string animalName = "";
            string animalSex = "";
            string animalDueDate = "";
            string animalBreed = "";
            while(rdr.Read())
            {
                animalId = rdr.GetInt32(5);
                animalName = rdr.GetString(1);
                animalSex = rdr.GetString(2);
                animalDueDate = rdr.GetString(3);
                animalBreed = rdr.GetString(4);
            }
            Animal foundAnimal = new Animal(animalName, animalSex, animalDueDate, animalBreed, animalId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundAnimal;
        }

        public override bool Equals(System.Object otherAnimal)
        {
            if (!(otherAnimal is Animal))
            {
                return false;
            }
            else
            {
                Animal newAnimal= (Animal) otherAnimal;
                bool idEquality = (this.GetId() == newAnimal.GetId());
                bool nameEquality = (this.GetName() == newAnimal.GetName());
                bool sexEquality = (this.GetSex() == newAnimal.GetSex());
                bool dueDateEquality = (this.GetDueDate() == newAnimal.GetDueDate());
                bool breedEquality = (this.GetBreed() == newAnimal.GetBreed());
                return (idEquality && nameEquality && sexEquality && dueDateEquality && breedEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO animals (name, sex, dueDate, breed) VALUES (@AnimalNme, @AnimalSex, @AnimalDueDate, @AnimalBreed);";

            cmd.Parameters.AddWithValue(@"AnimalName", this._name);
            cmd.Parameters.AddWithValue(@"AnimalSex", this._sex);
            cmd.Parameters.AddWithValue(@"AnimalDueDate", this._dueDate);
            cmd.Parameters.AddWithValue(@"AnimalBreed", this._breed);
        
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
