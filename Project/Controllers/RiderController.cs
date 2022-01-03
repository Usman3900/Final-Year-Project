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

    }

}