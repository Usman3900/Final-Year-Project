using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class CustomerPaymentsController : Controller
    {

        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: CustomerBillings
        public ActionResult Index()
        {
            Session["CustomerExpenditureError"] = null;
            return RedirectToAction("CustomerBillings");
        }

        public ActionResult CustomerBillings()
        {
            if (Session["customerKey"] != null)
            {
                int x = (int)Session["customerKey"];
                if (x == 1)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Customer_Payments.Where(st => st.CustomerId == id && st.Date >= fDate && st.Date <= tDate).ToList());
                }

                else if (x == 2)
                {
                    float id = float.Parse(Session["id"].ToString());
                    return View(dc.Customer_Payments.Where(st => st.CustomerId == id).ToList());
                }

                else if (x == 3)
                {
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.Customer_Payments.Where(st => st.Date >= fDate).ToList());
                }

                else if (x == 4)
                {
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Customer_Payments.Where(st => st.Date <= tDate).ToList());
                }

                else if (x == 5)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.Customer_Payments.Where(st => st.CustomerId == id && st.Date >= fDate).ToList());
                }

                else if (x == 6)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Customer_Payments.Where(st => st.CustomerId == id && st.Date <= tDate).ToList());
                }

                else if (x == 7)
                {
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Customer_Payments.Where(st => st.Date >= fDate && st.Date <= tDate).ToList());
                }

            }

            return View(dc.Customer_Payments.ToList());
        }

        public ActionResult addCustomerPayment()
        {
            string id = Request["custid"];
            string date = Request["date"];
            string payment = Request["payment"];

            DateTime fDate = Convert.ToDateTime(date);

            try
            {
                Customer c = dc.Customers.First(std => std.Id == float.Parse(id));

                Customer_Payment p = new Customer_Payment();
                p.CustomerId = (int)float.Parse(id);
                p.Name = c.Name;
                p.Date = fDate;
                p.Payment = float.Parse(payment);

                c.Balance -= float.Parse(payment);

                dc.Customer_Payments.InsertOnSubmit(p);

                dc.SubmitChanges();
            }

            catch (Exception ex)
            {
                Session["CustomerExpenditureError"] = 1;
                Session["PaymentMessage0"] = "Payment Cannot be Addded";
                Session["PaymentMessage1"] = "Customer not Found!";
                return RedirectToAction("CustomerBillings");
            }

            return RedirectToAction("Index");

        }

        public ActionResult ModifyCustomerPayment()
        {
            string id = Request["custid"];
            string cid = Request["Id"];
            string date = Request["date"];
            string payment = Request["payment"];

            try
            {
                Customer_Payment p = dc.Customer_Payments.First(std => std.Id == float.Parse(id));

                if (p.CustomerId != float.Parse(cid))
                {
                    Customer c = dc.Customers.First(std => std.Id == p.CustomerId);
                    c.Balance += float.Parse(payment);

                    Customer c1 = dc.Customers.First(std => std.Id == float.Parse(cid));
                    c1.Balance -= float.Parse(payment);
                    p.CustomerId = c1.Id;
                }

                else
                {
                    Customer c1 = dc.Customers.First(std => std.Id == float.Parse(cid));
                    c1.Balance -= float.Parse(payment);
                }

                p.Date = Convert.ToDateTime(date);
                p.Payment = float.Parse(payment);
                dc.SubmitChanges();
            }

            catch (Exception ex)
            {
                Session["CustomerExpenditureError"] = 1;
                Session["PaymentMessage0"] = "Payment Cannot be Modified!";
                Session["PaymentMessage1"] = "Customer not Found!";
                return RedirectToAction("CustomerBillings");
            }

            return RedirectToAction("Index");
        }


        public ActionResult SearchCustomer()
        {

            string id = Request["rid"];
            string from = Request["fromdate"];
            string to = Request["todate"];

            if (id != "" && from != "" && to != "")
            {
                Session["customerKey"] = 1;
                Session["id"] = id;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }

            else if (id != "" && from == "" && to == "")
            {
                Session["customerKey"] = 2;
                Session["id"] = id;
            }


            else if (id == "" && from != "" && to == "")
            {
                Session["customerKey"] = 3;
                Session["fromDate"] = from;
            }

            else if (id == "" && from == "" && to != "")
            {
                Session["customerKey"] = 4;
                Session["toDate"] = to;
            }

            else if (id != "" && from != "" && to == "")
            {
                Session["id"] = id;
                Session["customerKey"] = 5;
                Session["fromDate"] = from;
            }

            else if (id != "" && from == "" && to != "")
            {
                Session["customerKey"] = 6;
                Session["id"] = id;
                Session["toDate"] = to;
            }

            else if (id == "" && from != "" && to != "")
            {
                Session["customerKey"] = 7;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }

            return RedirectToAction("Index");
        }

        public ActionResult showAllCustomers()
        {
            Session["customerKey"] = null;
            return RedirectToAction("Index");
        }


    }
}