using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Restaurant.Objects
{
  public class Cuisine
  {
    string _name;
    int _id;

    public Cuisine(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public override bool Equals(Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
      Cuisine newCuisine = (Cuisine) otherCuisine;
      bool idEquality = (this.GetId() == newCuisine.GetId());
      bool nameEquality = (this.GetName() == newCuisine.GetName());
      return (idEquality && nameEquality);
      }
    }


    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Cuisine newCuisine =  new Cuisine(name, id);
        allCuisines.Add(newCuisine);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allCuisines;
    }
  }
}
