using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        
        public ActionResult Index()
        {
            return RedirectToAction("listCustomer", "Customer");
        }
       

    }

     
        
    }
