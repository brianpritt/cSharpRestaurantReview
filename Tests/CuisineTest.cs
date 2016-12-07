using Xunit;
using System;
using System.Collections.Generic;
using Restaurant.Objects;

namespace  Restaurant
{
  public class CuisineTest
  {
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
  }
}
