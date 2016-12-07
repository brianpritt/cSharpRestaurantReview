using Nancy;
using System.Collections.Generic;
using System;
using Restaurant.Objects;

namespace Restaurant
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
