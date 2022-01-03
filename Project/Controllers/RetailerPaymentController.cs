using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class RetailerPaymentController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: RetailerPayment

        public ActionResult Index()
        {
            Session["RetailerPaymentError"] = null;
            return RedirectToAction("paymentRetailer");
        }


        public ActionResult PaymentEntry()
        {
            string id = Request["ccustid"];
            string date = Request["Date"];
            string amount = Request["payment"];

            DateTime oDate = Convert.ToDateTime(date);
            try
            {
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
            }

            catch (Exception x)
            {
                Session["RetailerPaymentError"] = 1;
                Session["RetailerPaymentMessage0"] = "Payment cannot be added!";
                Session["RetailerPaymentMessage1"] = "Retailer not found!";
                return RedirectToAction("paymentRetailer");
            }
            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }


        public ActionResult deleteRetailerPayment(String id)
        {

            Retailer_Payment c = dc.Retailer_Payments.First(std => std.Id == float.Parse(id));

            Retailer r = dc.Retailers.First(std => std.Id == c.RetailerId);
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            r.Balance = r.Balance + c.Payment;
            dc.Retailer_Payments.DeleteOnSubmit(c);
            dc.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ModifyRetailerPayment()
        {
            string id = Request["custid"];
            string retailerId = Request["rid"];
            string date = Request["date"];
            string payment = Request["payment"];


            Retailer_Payment c = dc.Retailer_Payments.First(std => std.Id == float.Parse(id));
            try
            {
                Retailer r1 = dc.Retailers.First(std => std.Id == float.Parse(retailerId));



                if (c.RetailerId != float.Parse(id))
                {
                    Retailer r = dc.Retailers.First(std => std.Id == c.RetailerId);
                    r.Balance += c.Payment;

                }

                r1.Balance -= float.Parse(payment);
                ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
                c.RetailerId = Int16.Parse(retailerId);
                c.Date = Convert.ToDateTime(date);
                c.Payment = float.Parse(payment);
                c.RName = r1.Name;

                dc.SubmitChanges();
            }

            catch (Exception ex)
            {
                Session["RetailerPaymentError"] = 1;
                Session["RetailerPaymentMessage0"] = "Payment cannot be modified!";
                Session["RetailerPaymentMessage1"] = "New Entered Retailer not found!";
                return RedirectToAction("paymentRetailer");
            }
            return RedirectToAction("Index");
        }



        public ActionResult showAllRetailers()
        {
            Session["retailerKey"] = null;
            return RedirectToAction("Index");
        }


    }
}