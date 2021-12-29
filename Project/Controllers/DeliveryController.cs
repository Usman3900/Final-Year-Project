using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class DeliveryController : Controller
    {

        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        } 
        
        public ActionResult DailyDeliveryList()
        {
          
            if (Session["deliveryKey"] != null)
            {
                int index = (int)Session["deliveryKey"];
                if (index == 1)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    float rid = float.Parse(Session["rid"].ToString());
                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);
                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid && b.RiderId == rid && b.Date >= fdate && b.Date <=tdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 2)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid 
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);


                }
                else if (index == 3)
                {
                    float rid = float.Parse(Session["rid"].ToString());

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.RiderId == rid
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 4)
                {

                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);
                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.Date >= fdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 5)
                {

                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.Date <= tdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 6)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    float rid = float.Parse(Session["rid"].ToString());

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid && b.RiderId == rid
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 7)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);
                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid  && b.Date >= fdate 
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 8)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid && b.Date <= tdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 9)
                {
                    float rid = float.Parse(Session["rid"].ToString());
                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.RiderId == rid && b.Date >= fdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 10)
                {
                    float rid = float.Parse(Session["rid"].ToString());
                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where  b.RiderId == rid && b.Date <= tdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 11)
                {
                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);
                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.Date >= fdate && b.Date <= tdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 12)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    float rid = float.Parse(Session["rid"].ToString());
                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid && b.RiderId == rid && b.Date >= fdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);

                }
                else if (index == 13)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);
                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid && b.Date >= fdate && b.Date <= tdate
                                 select new Bill
                                 {
                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);


                }
                else if (index == 14)
                {
                    float cid = float.Parse(Session["cid"].ToString());
                    float rid = float.Parse(Session["rid"].ToString());
                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.CustomerId == cid && b.RiderId == rid && b.Date <= tdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);
                }
                else if (index == 15)
                {
                    float rid = float.Parse(Session["rid"].ToString());
                    DateTime fdate = Convert.ToDateTime(Session["fdate"]);
                    DateTime tdate = Convert.ToDateTime(Session["tdate"]);

                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 where b.RiderId == rid && b.Date >= fdate && b.Date <= tdate
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);
                }
                else
                {
                    var query = (from b in dc.Billings
                                 join c in dc.Customers on b.CustomerId equals c.Id
                                 join r in dc.Riders on b.RiderId equals r.Id
                                 select new Bill
                                 {

                                     customerId = b.CustomerId,
                                     riderId = b.RiderId,
                                     customerName = c.Name,
                                     riderName = r.Name,
                                     address = c.Address,
                                     date = b.Date,
                                     rate = b.Rate,
                                     quantity = b.Quantity,
                                     id = b.Id,
                                     bill = b.Rate * b.Quantity

                                 }).ToList();
                    return View(query);
                }

            }
            else
            {
               var query = (from b in dc.Billings
                             join c in dc.Customers on b.CustomerId equals c.Id
                             join r in dc.Riders on b.RiderId equals r.Id
                             select new Bill
                             {

                                 customerId = b.CustomerId,
                                 riderId = b.RiderId,
                                 customerName = c.Name,
                                 riderName = r.Name,
                                 address = c.Address,
                                 date = b.Date,
                                 rate = b.Rate,
                                 quantity = b.Quantity,
                                 id = b.Id,
                                 bill = b.Rate * b.Quantity

                             }).ToList();

                return View(query);
            }
            
        }
        
        public ActionResult addDelivery()
        {
            string Cid = Request["cid"];
            string Rid = Request["rid"];
            string date = Request["date"];
            string quantity = Request["quantity"];
            DateTime oDate = Convert.ToDateTime(date);

            Customer c = dc.Customers.First(std => std.Id == float.Parse(Cid));
            Rider r = dc.Riders.First(std => std.Id == float.Parse(Rid));

            Billing b = new Billing();

            b.CustomerId = (int)float.Parse(Cid);
            b.RiderId = (int)float.Parse(Rid);
            b.Date = oDate;
            try
            {
                b.Rate = (double)c.Rate;
            }
            catch (Exception ex)
            { 
            }
            b.Quantity = float.Parse(quantity);
            try
            {
                c.Balance += float.Parse(quantity) * (double)c.Rate;
            }
            catch(Exception e)
            { }

            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity -= float.Parse(quantity);

            dc.Billings.InsertOnSubmit(b);
            dc.SubmitChanges();

            return RedirectToAction("DailyDeliveryList");

        }

        public ActionResult modifyDelivery()
        {
            string id = Request["did"];
            string cid = Request["cid"];
            string rid = Request["rid"];
            string quantity = Request["quantity"];
            string date = Request["date"];

            Billing b = dc.Billings.First(std => std.Id == float.Parse(id));
            Customer c1 = dc.Customers.First(std => std.Id == float.Parse(cid));

            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity += b.Quantity;

            if (b.CustomerId != float.Parse(cid))
            {
                Customer c = dc.Customers.First(std => std.Id == b.CustomerId);
                c.Balance -= (b.Quantity * b.Rate);

                c1.Balance += (double)(c1.Rate * float.Parse(quantity));
            }
            else
            {
                c1.Balance -= (b.Rate * b.Quantity);
                c1.Balance += (double)(c1.Rate * float.Parse(quantity));
            }


            b.CustomerId = (int)float.Parse(cid);
            b.RiderId = (int)float.Parse(rid);
            b.Quantity = float.Parse(quantity);
            b.Rate = (double)c1.Rate;
            b.Date = Convert.ToDateTime(date);
            a.Quantity -= float.Parse(quantity);

            dc.SubmitChanges();
            return RedirectToAction("DailyDeliveryList");

        }

        public ActionResult RemoveDelivery(string id)
        {
            Billing b = dc.Billings.First(std => std.Id == float.Parse(id));
            Customer c1 = dc.Customers.First(std => std.Id == b.CustomerId);

            c1.Balance -= (b.Rate * b.Quantity);

            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity += b.Quantity;

            dc.Billings.DeleteOnSubmit(b);

            dc.SubmitChanges();
            return RedirectToAction("DailyDeliveryList");
        }

        public ActionResult filterSearch()
        {
            string cid = Request["cid"];
            string rid = Request["rid"];
            string fdate = Request["fdate"];
            string tdate = Request["tdate"];

            if (cid != "" && rid != "" && fdate != "" && tdate != "")
            {
                Session["deliveryKey"] = 1;
                Session["cid"] = cid;
                Session["rid"] = rid;
                Session["fdate"] = fdate;
                Session["tdate"] = tdate;
            }
            else if (cid != "" && rid == "" && fdate == "" && tdate == "")
            {
                Session["deliveryKey"] = 2;
                Session["cid"] = cid;
                
            }
            else if (cid == "" && rid != "" && fdate == "" && tdate == "")
            {
                Session["deliveryKey"] = 3;
                Session["rid"] = rid;

            }
            else if (cid == "" && rid == "" && fdate != "" && tdate == "")
            {
                Session["deliveryKey"] = 4;
                Session["fdate"] = fdate;

            }
            else if (cid == "" && rid == "" && fdate == "" && tdate != "")
            {
                Session["deliveryKey"] = 5;
                Session["tdate"] = tdate;

            }
            else if (cid != "" && rid != "" && fdate == "" && tdate == "")
            {
                Session["deliveryKey"] = 6;
                Session["cid"] = cid;
                Session["rid"] = rid;
               
            }
            else if (cid != "" && rid == "" && fdate != "" && tdate == "")
            {
                Session["deliveryKey"] = 7;
                Session["cid"] = cid;
                Session["fdate"] = fdate;

            }
            else if (cid != "" && rid == "" && fdate == "" && tdate != "")
            {
                Session["deliveryKey"] = 8;
                Session["cid"] = cid;
                Session["tdate"] = tdate;
            }
            else if (cid == "" && rid != "" && fdate != "" && tdate == "")
            {
                Session["deliveryKey"] = 9;
                Session["rid"] = rid;
                Session["fdate"] = fdate;
            }
            else if (cid == "" && rid != "" && fdate == "" && tdate != "")
            {
                Session["deliveryKey"] = 10;
                Session["rid"] = rid;
                Session["tdate"] = tdate;
            }
            else if (cid == "" && rid == "" && fdate != "" && tdate != "")
            {
                Session["deliveryKey"] = 11;
                Session["fdate"] = fdate;
                Session["tdate"] = tdate;
            }
            else if (cid != "" && rid != "" && fdate != "" && tdate == "")
            {
                Session["deliveryKey"] = 12;
                Session["cid"] = cid;
                Session["rid"] = rid;
                Session["fdate"] = fdate;
            }
            else if (cid != "" && rid == "" && fdate != "" && tdate != "")
            {
                Session["deliveryKey"] = 13;
                Session["cid"] = cid;
                Session["fdate"] = fdate;
                Session["tdate"] = tdate;
            }
            else if (cid != "" && rid != "" && fdate == "" && tdate != "")
            {
                Session["deliveryKey"] = 14;
                Session["cid"] = cid;
                Session["rid"] = rid;
                Session["tdate"] = tdate;
            }
            else if (cid == "" && rid != "" && fdate != "" && tdate != "")
            {
                Session["deliveryKey"] = 15;
                Session["rid"] = rid;
                Session["fdate"] = fdate;
                Session["tdate"] = tdate;
            }

            return RedirectToAction("DailyDeliveryList");
        }

        public ActionResult showAllDeliveries()
        {
            Session["deliveryKey"] = null;
            return RedirectToAction("DailyDeliveryList");
        }
        

    }
}