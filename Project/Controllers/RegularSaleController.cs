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
            if (Session["regularSaleKey"] != null)
            {
                int x = (int)Session["regularSaleKey"];
                if (x == 1)
                {
                    string id = Session["id"].ToString();
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.RegularSales.Where(st => st.Name == id && st.Date >= fDate && st.Date <= tDate).ToList());
                }

                else if (x == 2)
                {
                    string id = Session["id"].ToString();
                    return View(dc.RegularSales.Where(st => st.Name == id).ToList());
                }

                else if (x == 3)
                {
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.RegularSales.Where(st => st.Date >= fDate).ToList());
                }

                else if (x == 4)
                {
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.RegularSales.Where(st => st.Date <= tDate).ToList());
                }

                else if (x == 5)
                {
                    string id = Session["id"].ToString();
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.RegularSales.Where(st => st.Name == id && st.Date >= fDate).ToList());
                }

                else if (x == 6)
                {
                    string id = Session["id"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.RegularSales.Where(st => st.Name == id && st.Date <= tDate).ToList());
                }

                else if (x == 7)
                {
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.RegularSales.Where(st => st.Date >= fDate && st.Date <= tDate).ToList());
                }

            }

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

        public ActionResult filterSaleSearch()
        {

            string id = Request["name"];
            string from = Request["fdate"];
            string to = Request["tdate"];

            if (id != "" && from != "" && to != "")
            {
                Session["regularSaleKey"] = 1;
                Session["id"] = id;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }

            else if (id != "" && from == "" && to == "")
            {
                Session["regularSaleKey"] = 2;
                Session["id"] = id;
            }


            else if (id == "" && from != "" && to == "")
            {
                Session["regularSaleKey"] = 3;
                Session["fromDate"] = from;
            }

            else if (id == "" && from == "" && to != "")
            {
                Session["regularSaleKey"] = 4;
                Session["toDate"] = to;
            }

            else if (id != "" && from != "" && to == "")
            {
                Session["id"] = id;
                Session["regularSaleKey"] = 5;
                Session["fromDate"] = from;
            }

            else if (id != "" && from == "" && to != "")
            {
                Session["regularSaleKey"] = 6;
                Session["id"] = id;
                Session["toDate"] = to;
            }

            else if (id == "" && from != "" && to != "")
            {
                Session["regularSaleKey"] = 7;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }

            return RedirectToAction("RegularSaleList");
        }

        public ActionResult showAllRegularSale()
        {
            Session["regularSaleKey"] = null;

            return RedirectToAction("RegularSaleList");
        }

    }
}