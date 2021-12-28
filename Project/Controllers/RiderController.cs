using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class RiderController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: Rider
        public ActionResult Index()
        {
            return RedirectToAction("listRider");
        }

        public ActionResult listRider()
        {
            return View(dc.Riders.ToList());
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

            return RedirectToAction("listRider");
        }

        public ActionResult ModifyRider()
        {
            string id = Request["custid"];
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

            return RedirectToAction("listRider");
        }

        public ActionResult deleteRider(String id)
        {
            Rider c = dc.Riders.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            c.Active = 0;
            dc.SubmitChanges();
            return RedirectToAction("listRider");
        }

        public ActionResult retreiveRider(String id)
        {
            Rider c = dc.Riders.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            c.Active = 1;
            dc.SubmitChanges();
            return RedirectToAction("listRider");
        }
        public ActionResult addExpenditure()
        {
            string id = Request["rid"];
            string date = Request["date"];
            string description = Request["description"];
            string amount = Request["amount"];
            DateTime oDate = Convert.ToDateTime(date);

            Rider c = dc.Riders.First(std => std.Id == float.Parse(id));
            
            ////////////////////////////////// Confirmation is needed ///////////////////////////////////////
            Rider_Expenditure r = new Rider_Expenditure();
            
            r.Date = oDate;
            r.Description = description;
            r.RiderID = (int)float.Parse(id);
            r.RiderName = c.Name;
            r.Amount = float.Parse(amount);
            dc.Rider_Expenditures.InsertOnSubmit(r);


            dc.SubmitChanges();
            return RedirectToAction("expenditureRider");
        }
        
        public ActionResult modifyExpenditure()
        {
            string id = Request["custid"];
            string riderID = Request["Rid"];
            string amount = Request["amount"];
            string desc = Request["desc"];
            string date = Request["date"];

            DateTime oDate = Convert.ToDateTime(date);
            Rider_Expenditure re = dc.Rider_Expenditures.First(std => std.Id == float.Parse(id));
            re.RiderID = (int)float.Parse(riderID);
            re.Amount = float.Parse(amount);
            re.Description = desc;
            re.Date = oDate;

            dc.SubmitChanges();
            return RedirectToAction("expenditureRider");
        }

        public ActionResult deleteExpenditure(String id)
        {
            Rider_Expenditure re = dc.Rider_Expenditures.First(std => std.Id == float.Parse(id));
            ////////////////////////////////// Deleteing Data But Confirmation message is needed ///////////////////////////////////////
            dc.Rider_Expenditures.DeleteOnSubmit(re);
            dc.SubmitChanges();
            return RedirectToAction("expenditureRider");
        }

        public ActionResult expenditureRider()
        {
            if (Session["expKey"] != null)
            {
                int x = (int)Session["expKey"];
                if (x == 1)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Rider_Expenditures.Where(st => st.RiderID == id && st.Date >= fDate && st.Date <= tDate).ToList());
                }

                else if (x == 2)
                {
                    float id = float.Parse(Session["id"].ToString());
                    return View(dc.Rider_Expenditures.Where(st => st.RiderID == id).ToList());
                }

                else if (x == 3)
                {
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.Rider_Expenditures.Where(st =>st.Date >= fDate).ToList());
                }

                else if (x == 4)
                {
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Rider_Expenditures.Where(st =>st.Date <= tDate).ToList());
                }

                else if (x == 5)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string from = Session["fromDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    return View(dc.Rider_Expenditures.Where(st => st.RiderID == id && st.Date >= fDate).ToList());
                }

                else if (x == 6)
                {
                    float id = float.Parse(Session["id"].ToString());
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Rider_Expenditures.Where(st => st.RiderID == id && st.Date <= tDate).ToList());
                }

                else if (x == 7)
                {
                    string from = Session["fromDate"].ToString();
                    string to = Session["toDate"].ToString();
                    DateTime fDate = Convert.ToDateTime(from);
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Rider_Expenditures.Where(st =>st.Date >= fDate && st.Date <= tDate).ToList());
                }

            }

            return View(dc.Rider_Expenditures.ToList());
        }

        public ActionResult SearchRider()
        {
            string id = Request["rid"];
            string from = Request["fromdate"];
            string to = Request["todate"];

            if (id != "" && from != "" && to != "")
            {
                Session["expKey"] = 1;
                Session["id"] = id;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }

            else if (id != "" && from == "" && to == "")
            {
                Session["expKey"] = 2;
                Session["id"] = id;
            }
            
            else if (id == "" && from != "" && to == "")
            {
                Session["expKey"] = 3;
                Session["fromDate"] = from;
            }
            
            else if (id == "" && from == "" && to != "")
            {
                Session["expKey"] = 4;
                Session["toDate"] = to;
            }

            else if (id != "" && from != "" && to == "")
            {
                Session["id"] = id;
                Session["expKey"] = 5;
                Session["fromDate"] = from;
            }

            else if (id != "" && from == "" && to != "")
            {
                Session["expKey"] = 6;
                Session["id"] = id;
                Session["toDate"] = to;
            }
            
            else if (id == "" && from != "" && to != "")
            {
                Session["expKey"] = 7;
                Session["fromDate"] = from;
                Session["toDate"] = to;
            }
 
            return RedirectToAction("expenditureRider");
        }


        public ActionResult showAllRiders()
        {
            Session["expKey"] = null;
            return RedirectToAction("expenditureRider");
        }
        
    }


}