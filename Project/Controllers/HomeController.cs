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
        Guid g = Guid.NewGuid();
        string guid = Guid.NewGuid().ToString();

        public ActionResult Index()
        {
            return RedirectToAction("Customer");
        }
        
        public ActionResult Customer()
        {
            return View(dc.Customers.ToList());
        }
        
        public ActionResult Rider()
        {
            return View(dc.Riders.Where(st => st.Active != 0).ToList());
        }

        public ActionResult Retailer()
        {
            return View(dc.Retailers.Where(st => st.Active != 0).ToList());
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

            return RedirectToAction("Customer");
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
            return RedirectToAction("Customer");
        }

        public ActionResult deleteCustomer(String id)
        {
            Customer c = dc.Customers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////

            c.Active = 0;
            dc.SubmitChanges();
            return RedirectToAction("Customer");
        } 
        
        public ActionResult retreiveCustomer(String id)
        {
            Customer c = dc.Customers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////

            c.Active = 1;
            dc.SubmitChanges();
            return RedirectToAction("Customer");
        }

        public ActionResult addRider()
        {
            string name = Request["Rname"];
            string phn = Request["Rid"];
            string address = Request["Raddress"];
            string cnic = Request["Rcnic"];
            var date = Request["Rdate"];
            DateTime oDate = Convert.ToDateTime(date);
            

            //DateTime oDate = Convert.ToDateTime(date);
             //string x = oDate.Date.ToString();
             Rider r = new Rider();
             r.Name = name;
             r.Phone_Number = phn;
             r.Address = address;
             r.CNIC = cnic;
             r.JoiningDate = oDate.Date;
            r.Active = 1;
             dc.Riders.InsertOnSubmit(r);
             dc.SubmitChanges();

            return RedirectToAction("Rider");
        }

        public ActionResult ModifyRider()
        {
            string id = Request["Rid"];
            string name = Request["Rname"];
            string phone = Request["Rphone"];
            string address = Request["Raddress"];
            string cnic = Request["Rcnic"];
            string date = Request["Rdate"];

            Rider r = dc.Riders.First(std => std.Id == float.Parse(id));

            r.Name = name;
            r.Phone_Number = phone;
            r.Address = address;
            r.CNIC = cnic;
            DateTime oDate = Convert.ToDateTime(date);
            r.JoiningDate = oDate;
            dc.SubmitChanges();

            return RedirectToAction("Rider");
        }

        public ActionResult deleteRider(String id)
        {
            Rider c = dc.Riders.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            c.Active = 0;
            dc.SubmitChanges();
            return RedirectToAction("Rider");
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

            return RedirectToAction("Retailer");
        }

        public ActionResult ModifyRetailer()
        {
            string id = Request["Reid"];
            string name = Request["rename"];
            string phone = Request["rphn"];
            string balance = Request["Rebalance"];
           

            Retailer r = dc.Retailers.First(std => std.Id == float.Parse(id));

            r.Name = name;
            r.Phone_Number = phone;
            r.Balance = float.Parse(balance); 
            dc.SubmitChanges();

            return RedirectToAction("Retailer");
        }

        public ActionResult deleteRetailer(String id)
        {
            Retailer c = dc.Retailers.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            c.Active = 0;
            dc.SubmitChanges();
            return RedirectToAction("Retailer");
        }

        public ActionResult DailyDelivery()
        {
            return View();
        }

        public ActionResult Supply()
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

                Available a = dc.Availables.FirstOrDefault(st => st.Product=="Milk");
                a.Quantity = a.Quantity + float.Parse(quantity);

                dc.Supplies.InsertOnSubmit(s);
                dc.SubmitChanges();
            }
            Session["key"] = 1;
            return RedirectToAction("Supply"); 
        }

        public ActionResult deleteSupply(String id)
        {
            Supply c = dc.Supplies.First(std => std.Id == float.Parse(id));
            Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
            a.Quantity = a.Quantity - c.quantity;
            
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            dc.Supplies.DeleteOnSubmit(c);
            dc.SubmitChanges();
            return RedirectToAction("Supply");
        }

        public ActionResult modifySupply()
        {
            string id = Request["sid"];
            string retailerId = Request["rid"];
            string quantity = Request["quantity"];
            string rate = Request["rate"];
            string date = Request["date"];
            DateTime oDate = Convert.ToDateTime(date);

            Supply s = dc.Supplies.First(std => std.Id == float.Parse(id));
            Retailer r = dc.Retailers.First(std => std.Id == float.Parse(retailerId));
            if (r.Active != 0)
            {
                Available a = dc.Availables.FirstOrDefault(st => st.Product == "Milk");
                a.Quantity = a.Quantity - s.quantity;

                s.retailerId = r.Id;
                s.quantity = float.Parse(quantity);
                s.rate = float.Parse(rate);
                s.date = oDate;
                a.Quantity = a.Quantity + float.Parse(quantity);
                dc.SubmitChanges();
            }
            return RedirectToAction("Supply");
        }
        

        public ActionResult showall()
        {
            Session["Key"] = 1;
            return RedirectToAction("Supply");
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
            return RedirectToAction("Supply");

        }
        

        }

     
        
    }
