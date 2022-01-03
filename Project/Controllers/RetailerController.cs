using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class RetailerController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: Retailer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listRetailer()
        {
            return View(dc.Retailers.ToList());
        }

        public ActionResult addRetailer()
        {
            string name = Request["Rename"];
            string phone = Request["Rphn"];
            string balance = Request["Rebalance"];

            Retailer r = new Retailer();
            r.Name = name;
            r.Phone_Number = phone;
            r.Balance = float.Parse(balance);
            r.Active = 1;
            dc.Retailers.InsertOnSubmit(r);
            dc.SubmitChanges();

            return RedirectToAction("listRetailer");
        }

        public ActionResult ModifyRetailer()
        {
            string id = Request["custid"];
            string name = Request["rename"];
            string phone = Request["rphn"];
            string balance = Request["Rebalance"];


            Retailer r = dc.Retailers.First(std => std.Id == float.Parse(id));

            r.Name = name;
            r.Phone_Number = phone;
            r.Balance = float.Parse(balance);
            dc.SubmitChanges();

            return RedirectToAction("listRetailer");
        }

        public ActionResult deleteRetailer(String id)
        {
            Retailer c = dc.Retailers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            c.Active = 0;
            dc.SubmitChanges();
            return RedirectToAction("listRetailer");
        } 
        
        public ActionResult retreiveRetailer(String id)
        {
            Retailer c = dc.Retailers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            c.Active = 1;
            dc.SubmitChanges();
            return RedirectToAction("listRetailer");
        }
        
                        

    }
}