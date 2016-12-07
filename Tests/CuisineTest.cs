using Xunit;
using System;
using System.Collections.Generic;
using Restaurant.Objects;

namespace  Restaurant
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
  }
}
