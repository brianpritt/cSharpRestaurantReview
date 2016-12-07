using Nancy;
using System.Collections.Generic;
using System;
using RestaurantDirectory.Objects;

namespace RestaurantDirectory
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/add/cuisine"] = _ =>{
        return View["cuisine_form.cshtml"];
      };
      Post["/add/cuisine"] = _ =>{
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine_name"]);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/add/restaurant"] = _ =>{
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["restaurant_form.cshtml", allCuisines];
      };
      Post["/add/restaurant"] = _ =>{
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant_name"], Request.Form["cuisine_id"]);
        newRestaurant.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/cuisine/{id}"] = parameters =>{
        var currentCuisine = Cuisine.Find(parameters.id);
        return View["cuisine.cshtml", currentCuisine];
      };
      Delete["/delete-all"] = _ =>{
        Cuisine.DeleteAll();
        Restaurant.DeleteAll();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
    }
  }
}
