using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AnimalShelter;

namespace AnimalShelter.Models
{
    public class Animal
    {
        private string _type;
        private string _name;
        private string _sex;
        private int _date;
        private string _breed;
        private int _id;

    public Animal(string type, string name, string sex, int date, string breed, int id)
    {
        _type = type;
        _name = name;
        _sex = sex;
        _date = date;
        _breed = breed;
        _id = id;
        
    }

     public string GetType()
    {
        return _type;
    }

    public void SetType(string newType)
    {
        _type = newType;
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

    public int GetDate()
    {
        return _date;
    }

    public void SetDate(int newDate)
    {
        _date = newDate;
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
        List<Animal> allAnimals = new List<Animal> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM animals;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            string type = rdr.GetString(0);
            string name = rdr.GetString(1);
            string sex = rdr.GetString(2);
            int date = rdr.GetInt32(3);
            string breed = rdr.GetString(4);
             int id= rdr.GetInt32(5);
            Animal newAnimal = new Animal(type, name, sex, date, breed, id);
            allAnimals.Add(newAnimal);
            
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allAnimals;
    }

    }

}
