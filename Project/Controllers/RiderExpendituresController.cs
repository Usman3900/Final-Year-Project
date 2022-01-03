using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class RiderExpendituresController : Controller
    {

        DataClasses1DataContext dc = new DataClasses1DataContext();

        // GET: RiderExpenditures
        public ActionResult Index()
        {
            Session["RiderExpendituresError"] = null;
            return RedirectToAction("expenditureRider");
        }

        public ActionResult addExpenditure()
        {
            string id = Request["rid"];
            string date = Request["date"];
            string description = Request["description"];
            string amount = Request["amount"];
            DateTime oDate = Convert.ToDateTime(date);

            try
            {
                Rider c = dc.Riders.First(std => std.Id == float.Parse(id));

                if (c.Active == 0)
                {
                    Session["RiderExpendituresError"] = 1;
                    Session["ExpenditureMessage0"] = "Expenditures cannot be added!";
                    Session["ExpenditureMessage1"] = "Rider status is inactive!";
                    return RedirectToAction("expenditureRider");
                }
                ////////////////////////////////// Confirmation is needed ///////////////////////////////////////
                Rider_Expenditure r = new Rider_Expenditure();

                r.Date = oDate;
                r.Description = description;
                r.RiderID = (int)float.Parse(id);
                r.RiderName = c.Name;
                r.Amount = float.Parse(amount);
                dc.Rider_Expenditures.InsertOnSubmit(r);

                dc.SubmitChanges();
                
            }

            catch (Exception ex)
            {
                Session["RiderExpendituresError"] = 1;
                Session["ExpenditureMessage0"] = "Expenditures cannot be added!";
                Session["ExpenditureMessage1"] = "Rider not found!";
                return RedirectToAction("expenditureRider");
            }

            return RedirectToAction("Index");
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
            if (re.RiderID == float.Parse(riderID))
            {
                Rider c = dc.Riders.First(std => std.Id == re.RiderID);
                if (c.Active == 0) {

                    Session["RiderExpendituresError"] = 1;
                    Session["ExpenditureMessage0"] = "Expenditures cannot be modified!";
                    Session["ExpenditureMessage1"] = "Rider status is inactive!";
                    return RedirectToAction("expenditureRider");
                }
            }

            else {

                try
                {
                    Rider c = dc.Riders.First(std => std.Id == float.Parse(riderID));
                    if (c.Active == 0)
                    {

                        Session["RiderExpendituresError"] = 1;
                        Session["ExpenditureMessage0"] = "Expenditures cannot be modified!";
                        Session["ExpenditureMessage1"] = "New Entered Rider status is inactive!";
                        return RedirectToAction("expenditureRider");
                    }

                }

                catch (Exception ex)
                {
                    Session["RiderExpendituresError"] = 1;
                    Session["ExpenditureMessage0"] = "Expenditures cannot be modified!";
                    Session["ExpenditureMessage1"] = "New Entered Rider not found!";
                    return RedirectToAction("expenditureRider");
                }

            }
            
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
            return RedirectToAction("Index");
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
                    return View(dc.Rider_Expenditures.Where(st => st.Date >= fDate).ToList());
                }

                else if (x == 4)
                {
                    string to = Session["toDate"].ToString();
                    DateTime tDate = Convert.ToDateTime(to);
                    return View(dc.Rider_Expenditures.Where(st => st.Date <= tDate).ToList());
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
                    return View(dc.Rider_Expenditures.Where(st => st.Date >= fDate && st.Date <= tDate).ToList());
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

            return RedirectToAction("Index");
        }


        public ActionResult showAllRiders()
        {
            Session["expKey"] = null;
            return RedirectToAction("Index");
        }

    
}
}