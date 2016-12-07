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
        return View["index.cshtml"];
      };

    }
  }
}
