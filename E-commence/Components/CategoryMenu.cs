using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using E_commence.Models;
using Microsoft.AspNetCore.Mvc;


namespace E_commence.Components
{
    public class CategoryMenu : ViewComponent
    {
        ecommenceEntities1 entities = new ecommenceEntities1();
       public IViewComponentResult Invoke()
        {
            return View(entities.Kategorilers.ToList());
            //return View();
        }
    }
}