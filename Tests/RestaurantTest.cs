using Xunit;
using System;
using System.Collections.Generic;
using RestaurantDirectory.Objects;

namespace  RestaurantDirectory
{
  public class RestaurantTest : IDisposable
  {
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant_directory_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Restaurant.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_EqualOverride_True()
    {
      Restaurant firstRestaurant = new Restaurant("Jimbo's", 1);
      Restaurant secondRestaurant = new Restaurant("Jimbo's", 1);

      Assert.Equal(firstRestaurant,secondRestaurant);
    }
    [Fact]
    public void Save_SavesRestaurantToDatabase_true()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Sam's Somalian Food", 2);
      //Act
      newRestaurant.Save();
      List<Restaurant> allRestaurants = Restaurant.GetAll();
      //Assert
      Assert.Equal(newRestaurant, allRestaurants[0]);
    }
    [Fact]
    public void Test_FindsRestaurantInDatabase_true()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Indian Palace", 3);
      newRestaurant.Save();
      //Act
      Restaurant findRestaurant = Restaurant.Find(newRestaurant.GetId());
      //Assert
      Assert.Equal(newRestaurant, findRestaurant);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {
      //Arrange
      Restaurant newRestaurant = new Restaurant("Harry's Mexican", 4);
      newRestaurant.Save();
      //Act
      newRestaurant.Edit("Jose's Mexican");
      Restaurant foundRestaurant = Restaurant.Find(newRestaurant.GetId());

      //Assert
      Assert.Equal("Jose's Mexican", foundRestaurant.GetName());
    }
    [Fact]
    public void Test_Delete_deleteRestaurantFromDB()
    {
      //Arrange
      string name1 = "Hokey's Hashbrowns";
      Restaurant newRestaurant1 = new Restaurant(name1, 1);
      newRestaurant1.Save();

      string name2 = "Nate's Nepali";
      Restaurant newRestaurant2 = new Restaurant(name2, 3);
      newRestaurant2.Save();
      //Act
      newRestaurant1.Delete();
      List<Restaurant> resultRestaurant = Restaurant.GetAll();
      List<Restaurant> testRestaurantList = new List<Restaurant> {newRestaurant2};
      //Assert
      Console.WriteLine(testRestaurantList[0].GetName() + " " + testRestaurantList[0].GetCuisineId() + " " + testRestaurantList[0].GetId());
      Console.WriteLine(resultRestaurant[0].GetName() + " " + resultRestaurant[0].GetCuisineId() + " " + resultRestaurant[0].GetId());
      Assert.Equal(testRestaurantList, resultRestaurant);

    }
  }
}
