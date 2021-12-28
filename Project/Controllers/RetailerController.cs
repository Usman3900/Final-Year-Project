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
        public ActionResult PaymentEntry()
        {
            string id = Request["ccustid"];
            string date = Request["Date"];
            string amount = Request["payment"];

            DateTime oDate = Convert.ToDateTime(date);

            Retailer r = dc.Retailers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            Retailer_Payment rp = new Retailer_Payment();
            rp.RetailerId = (int)float.Parse(id);
            rp.Date = oDate;
            rp.Payment = float.Parse(amount);
            rp.RName = r.Name;
            dc.Retailer_Payments.InsertOnSubmit(rp);

            r.Balance = r.Balance - float.Parse(amount);

            dc.SubmitChanges();
            return RedirectToAction("paymentRetailer");
        }

        public ActionResult paymentRetailer()
        {
            if (Session["retailerKey"] != null)
            {
                int x = (int)Session["retailerKey"];
                if (x == 1)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Retailer_Payments.Where(st => st.RetailerId == id && st.Date >= fDate && st.Date <= tDate).ToList());
                }

                else if (x == 2)
                {
                    float id = float.Parse(Session["id"].ToString());
                    return View(dc.Retailer_Payments.Where(st => st.RetailerId == id).ToList());
                }

                else if (x == 3)
                {
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.Retailer_Payments.Where(st => st.Date >= fDate).ToList());
                }

                else if (x == 4)
                {
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Retailer_Payments.Where(st => st.Date <= tDate).ToList());
                }

                else if (x == 5)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.Retailer_Payments.Where(st => st.RetailerId == id && st.Date >= fDate).ToList());
                }

                else if (x == 6)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Retailer_Payments.Where(st => st.RetailerId == id && st.Date <= tDate).ToList());
                }

                else if (x == 7)
                {
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Retailer_Payments.Where(st => st.Date >= fDate && st.Date <= tDate).ToList());
                }

            }

            return View(dc.Retailer_Payments.ToList());
        }

        public ActionResult SearchRetailer()
        {
            string id = Request["rid"];
            string from = Request["fromdate"];
            string to = Request["todate"];

            if (id != "" && from != "" && to != "")
            {
                Session["retailerKey"] = 1;
                Session["id"] = id;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }

            else if (id != "" && from == "" && to == "")
            {
                Session["retailerKey"] = 2;
                Session["id"] = id;
            }

            else if (id == "" && from != "" && to == "")
            {
                Session["retailerKey"] = 3;
                Session["fromDate"] = from;
            }

            else if (id == "" && from == "" && to != "")
            {
                Session["retailerKey"] = 4;
                Session["toDate"] = to;
            }

            else if (id != "" && from != "" && to == "")
            {
                Session["id"] = id;
                Session["retailerKey"] = 5;
                Session["fromDate"] = from;
            }

            else if (id != "" && from == "" && to != "")
            {
                Session["retailerKey"] = 6;
                Session["id"] = id;
                Session["toDate"] = to;
            }

            else if (id == "" && from != "" && to != "")
            {
                Session["retailerKey"] = 7;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }

            return RedirectToAction("paymentRetailer");
        }


        public ActionResult deleteRetailerPayment(String id)
        {
            
            Retailer_Payment c = dc.Retailer_Payments.First(std => std.Id == float.Parse(id));

            Retailer r = dc.Retailers.First(std => std.Id == c.RetailerId);
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            r.Balance = r.Balance + c.Payment;
            dc.Retailer_Payments.DeleteOnSubmit(c);
            dc.SubmitChanges();
            return RedirectToAction("paymentRetailer");
        }

        public ActionResult showAllRetailers()
        {
            Session["retailerKey"] = null;
            return RedirectToAction("paymentRetailer");
        }
        
                        

    }
}