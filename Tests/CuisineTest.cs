using Xunit;
using System;
using System.Collections.Generic;
using RestaurantDirectory.Objects;

namespace  RestaurantDirectory
{
  public class CuisineTest : IDisposable
  {
    public void Dispose()
    {
      Cuisine.DeleteAll();
    }
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant_directory_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void GetAll_DatabaseEmpty_true()
    {
      //Arrange
      //Act
      int result = Cuisine.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_EqualOverride_True()
    {
      Cuisine firstCuisine = new Cuisine("Texican");
      Cuisine secondCuisine = new Cuisine("Texican");

      Assert.Equal(firstCuisine,secondCuisine);
    }
    [Fact]
    public void Save_SavesCuisineToDatabase_true()
    {
      //Arrange
      Cuisine newCuisine = new Cuisine("Somali");
      //Act
      newCuisine.Save();
      List<Cuisine> allCuisines = Cuisine.GetAll();
      //Assert
      Assert.Equal(newCuisine, allCuisines[0]);
    }
    [Fact]
    public void Test_FindsCuisineInDatabase_true()
    {
      //Arrange
      Cuisine newCuisine = new Cuisine("Indian");
      newCuisine.Save();
      //Act
      Cuisine findCuisine = Cuisine.Find(newCuisine.GetId());
      //Assert
      Assert.Equal(newCuisine, findCuisine);
    }
    [Fact]
    public void Edit_ChangesName_true()
    {
      //Arrange
      Cuisine newCuisine = new Cuisine("Mexican");
      newCuisine.Save();
      //Act
      newCuisine.Edit("Texican");
      Cuisine foundCuisine = Cuisine.Find(newCuisine.GetId());

      //Assert
      Assert.Equal("Texican", foundCuisine.GetName());
    }
    [Fact]
    public void Test_Delete_deleteCategoryFromDB()
    {
      //Arrange
      string name1 = "Thai";
      Cuisine newCuisine1 = new Cuisine(name1);
      newCuisine1.Save();

      string name2 = "Nepali";
      Cuisine newCuisine2 = new Cuisine(name2);
      newCuisine2.Save();
      //Act
      newCuisine1.Delete();
      List<Cuisine> resultCuisine = Cuisine.GetAll();
      List<Cuisine> testCuisineList = new List<Cuisine> {newCuisine2};
      //Assert
      Assert.Equal(testCuisineList, resultCuisine);

    }
  }
}
