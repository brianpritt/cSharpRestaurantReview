using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RestaurantDirectory.Objects
{
  public class Restaurant
  {
    string _name;
    int _cuisine_id;
    int _id;

    public Restaurant(string name, int cuisine_id, int id = 0)
    {
      _name = name;
      _cuisine_id = cuisine_id;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public int GetCuisineId()
    {
      return _cuisine_id;
    }
    public string GetName()
    {
      return _name;
    }

    public override bool Equals(Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
      Restaurant newRestaurant = (Restaurant) otherRestaurant;
      bool idEquality = (this.GetId() == newRestaurant.GetId());
      bool nameEquality = (this.GetName() == newRestaurant.GetName());
      bool cuisineIdEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
      return (idEquality && nameEquality && cuisineIdEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int cuisine_id = rdr.GetInt32(2);
        Restaurant newRestaurant =  new Restaurant(name, cuisine_id, id);
        allRestaurants.Add(newRestaurant);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allRestaurants;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, cuisine_id) OUTPUT INSERTED.id VALUES (@name, @cuisine_id);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@name";
      nameParameter.Value = this.GetName();

      SqlParameter cuisineIdParameter = new SqlParameter("@cuisine_id", this.GetCuisineId());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(cuisineIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      int foundRestaurantCuisineId = 0;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantCuisineId = rdr.GetInt32(2);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantCuisineId, foundRestaurantId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }

    public void Edit(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET name = @NewName OUTPUT INSERTED.name WHERE id = @RestaurantId;", conn);

      SqlParameter nameParameter = new SqlParameter("@NewName", newName);
      SqlParameter restaurantIdParameter = new SqlParameter("@RestaurantId", this.GetId());
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(restaurantIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter("@RestaurantId", this.GetId());

      cmd.Parameters.Add(restaurantIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
