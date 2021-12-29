using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class RegularSaleController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: RegularSale
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult RegularSaleList()
        {
            return View(dc.RegularSales.ToList());
        }

        public ActionResult AddRegularSale()
        {
            string name = Request["custname"];
            string date = Request["date"];
            string rate = Request["rate"];
            string quantity = Request["quantity"];

            DateTime oDate = Convert.ToDateTime(date);
            RegularSale r = new RegularSale();

            r.Name = name;
            r.Date = oDate;
            r.Rate = float.Parse(rate);
            r.Quantity = float.Parse(quantity);

            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity -= float.Parse(quantity);

            dc.RegularSales.InsertOnSubmit(r);
            dc.SubmitChanges();

            return RedirectToAction("RegularSaleList");
        }

        public ActionResult ModifyRegularSale()
        {
            string id = Request["custid"];
            string name = Request["name"];
            string date = Request["date"];
            string rate = Request["rate"];
            string quantity = Request["Quantity"];

            RegularSale s = dc.RegularSales.FirstOrDefault(st => st.Id == float.Parse(id));
            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity += s.Quantity;

            s.Name = name;
            s.Date = Convert.ToDateTime(date);
            s.Rate = float.Parse(rate);
            s.Quantity = float.Parse(quantity);

            a.Quantity -= float.Parse(quantity);

            dc.SubmitChanges();
            return RedirectToAction("RegularSaleList");

        }

        public ActionResult DeleteRegularSale(string id)
        {

            RegularSale s = dc.RegularSales.FirstOrDefault(st => st.Id == float.Parse(id));
            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity += s.Quantity;

            dc.RegularSales.DeleteOnSubmit(s);
            dc.SubmitChanges();
            return RedirectToAction("RegularSaleList");


        }


    }
}