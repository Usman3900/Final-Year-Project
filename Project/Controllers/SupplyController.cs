using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class SupplyController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        DataClasses1DataContext dc1 = new DataClasses1DataContext();

        // GET: Supply
        public ActionResult Index()
        {
            return RedirectToAction("listSupply");
        }

        public ActionResult listSupply()
        {
            if (Session["key"] != null)
            {
                int index = (int)Session["key"];

                if (index == 1)
                {
                    return View(dc.Supplies.ToList());
                }

                else if (index == 2)
                {
                    DateTime from = Convert.ToDateTime(Session["fromDate"].ToString());
                    DateTime to = Convert.ToDateTime(Session["toDate"].ToString());
                    float sid = float.Parse(Session["id"].ToString());
                    return View(dc.Supplies.Where(t => t.retailerId == sid && t.date >= from && t.date <= to).ToList());
                }

                else if (index == 3)
                {
                    float sid = float.Parse(Session["id"].ToString());
                    return View(dc.Supplies.Where(t => t.retailerId == sid).ToList());
                }

                else if (index == 4)
                {
                    DateTime from = Convert.ToDateTime(Session["fromDate"].ToString());
                    DateTime to = Convert.ToDateTime(Session["toDate"].ToString());
                    return View(dc.Supplies.Where(t => t.date >= from && t.date <= to).ToList());
                }

                else if (index == 5)
                {
                    DateTime from = Convert.ToDateTime(Session["fromDate"].ToString());
                    float sid = float.Parse(Session["id"].ToString());
                    return View(dc.Supplies.Where(t => t.retailerId == sid && t.date >= from).ToList());
                }

                else if (index == 6)
                {
                    DateTime to = Convert.ToDateTime(Session["toDate"].ToString());
                    float sid = float.Parse(Session["id"].ToString());
                    return View(dc.Supplies.Where(t => t.retailerId == sid && t.date <= to).ToList());
                }

                else if (index == 7)
                {
                    DateTime from = Convert.ToDateTime(Session["fromDate"].ToString());
                    return View(dc.Supplies.Where(t => t.date >= from).ToList());
                }

                else if (index == 8)
                {
                    DateTime to = Convert.ToDateTime(Session["toDate"].ToString());
                    return View(dc.Supplies.Where(t => t.date <= to).ToList());
                }
            }
            return View(dc.Supplies.ToList());
        }

        public ActionResult addSupply()
        {
            string id = Request["sid"];
            string quantity = Request["quantity"];
            string rate = Request["rate"];
            string date = Request["date"];
            DateTime oDate = Convert.ToDateTime(date);

            Retailer r = dc.Retailers.First(std => std.Id == float.Parse(id));
            if (r.Active != 0)
            {
                Supply s = new Supply();
                s.retailerId = (int)float.Parse(id);
                s.quantity = float.Parse(quantity);
                s.date = oDate;
                s.rate = float.Parse(rate);
                s.name = r.Name;

                r.Balance = r.Balance + (float.Parse(rate) * float.Parse(quantity));

                Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
                a.Quantity = a.Quantity + float.Parse(quantity);

                dc.Supplies.InsertOnSubmit(s);
                dc.SubmitChanges();
            }
            Session["key"] = 1;
            return RedirectToAction("listSupply");
        }

        public ActionResult deleteSupply(String id)
        {
            Supply c = dc.Supplies.First(std => std.Id == float.Parse(id));
            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity = a.Quantity - c.quantity;

            Retailer r = dc.Retailers.First(std => std.Id == c.retailerId);
            r.Balance = r.Balance - (c.rate * c.quantity);
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            dc.Supplies.DeleteOnSubmit(c);
            dc.SubmitChanges();
            return RedirectToAction("listSupply");
        }

        public ActionResult modifySupply()
        {
            string id = Request["custid"];
            string retailerId = Request["rid"];
            string quantity = Request["quantity"];
            string rate = Request["rate"];
            string date = Request["date"];
            DateTime oDate = Convert.ToDateTime(date);

            Supply s = dc.Supplies.First(std => std.Id == float.Parse(id));

            Retailer r = dc.Retailers.First(std => std.Id == float.Parse(retailerId));

            if (s.retailerId != float.Parse(retailerId))
            {
                Retailer r1 = dc1.Retailers.First(std => std.Id == s.retailerId);
                r1.Balance = r1.Balance - (s.rate * s.quantity);
                dc1.SubmitChanges();
            }

            else {
                r.Balance = r.Balance - (s.rate * s.quantity);
            }

            if (r.Active != 0)
            {
                Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
                a.Quantity = a.Quantity - s.quantity;
                s.retailerId = r.Id;
                s.quantity = float.Parse(quantity);
                s.rate = float.Parse(rate);
                s.date = oDate;
                a.Quantity = a.Quantity + float.Parse(quantity);
                r.Balance = r.Balance + (float.Parse(quantity) * float.Parse(rate)); 
                dc.SubmitChanges();
            }

            return RedirectToAction("listSupply");
        }


        public ActionResult showall()
        {
            Session["Key"] = 1;
            return RedirectToAction("listSupply");
        }

        public ActionResult searchSupplyRecord()
        {
            string id = Request["sid"];
            string fromDate = Request["fromDate"];
            string toDate = Request["toDate"];

            if (id != "" && fromDate != "" && toDate != "")
            {
                Session["id"] = id;
                Session["fromDate"] = fromDate;
                Session["toDate"] = toDate;
                Session["key"] = 2;

            }

            else if (id != "" && fromDate == "" && toDate == "")
            {
                Session["id"] = id;
                Session["key"] = 3;
            }

            else if (id == "" && fromDate != "" && toDate != "")
            {
                Session["fromDate"] = fromDate;
                Session["toDate"] = toDate;
                Session["key"] = 4;
            }

            else if (id != "" && fromDate != "" && toDate == "")
            {
                Session["id"] = id;
                Session["fromDate"] = fromDate;
                Session["key"] = 5;
            }

            else if (id != "" && fromDate == "" && toDate != "")
            {
                Session["id"] = id;
                Session["toDate"] = toDate;
                Session["key"] = 6;
            }

            else if (id == "" && fromDate != "" && toDate == "")
            {
                Session["fromDate"] = fromDate;
                Session["key"] = 7;
            }

            else if (id == "" && fromDate == "" && toDate != "")
            {
                Session["toDate"] = toDate;
                Session["key"] = 8;
            }
            return RedirectToAction("listSupply");

        }

    }
}