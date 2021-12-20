using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CustomerController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: Customer
        public ActionResult Index()
        {
            return RedirectToAction("listCustomer");
        }

        public ActionResult listCustomer()
        {
            return View(dc.Customers.ToList());
        }

        public ActionResult addNewCustomer()
        {
            String PhnNo = Request["custid"];
            String name = Request["custname"];
            String address = Request["custaddress"];
            float balance = float.Parse(Request["custbalance"]);

            Customer c = new Customer();
            c.PhnNumber = PhnNo;
            c.Name = name;
            c.Address = address;
            c.Balance = balance;
            c.Active = 1;
            //////////////////////////////////// Adding new Customers but need to add checks for dual addition //////////////////////////////////
            dc.Customers.InsertOnSubmit(c);
            dc.SubmitChanges();

            return RedirectToAction("listCustomer");
        }

        public ActionResult Modify()
        {
            float id = float.Parse(Request["custid"]);
            String phnNumber = Request["custno"];
            String address = Request["custaddress"];
            String name = Request["custname"];
            String balance = Request["add"];

            Customer c = dc.Customers.First(std => std.Id == id);


            //////////////////////////////////// Adding new Customers but need to add checks for dual addition //////////////////////////////////

            c.Name = name;
            c.Address = address;
            c.PhnNumber = phnNumber;
            c.Balance = float.Parse(balance);
            dc.SubmitChanges();
            return RedirectToAction("listCustomer");
        }

        public ActionResult deleteCustomer(String id)
        {
            Customer c = dc.Customers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////

            c.Active = 0;
            dc.SubmitChanges();
            return RedirectToAction("listCustomer");
        }

        public ActionResult retreiveCustomer(String id)
        {
            Customer c = dc.Customers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////

            c.Active = 1;
            dc.SubmitChanges();
            return RedirectToAction("listCustomer");
        }
    }
}