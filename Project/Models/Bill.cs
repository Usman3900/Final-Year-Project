using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Bill
    {
        public int id, riderId, customerId;
        public string riderName, customerName, address;
        public DateTime date;
        public double rate, quantity, bill;

        public Bill(int id, int riderId, int customerId, 
            string riderName, string customerName, DateTime date, double rate, double quantity, string address, double bill)
        {
            this.id = id;
            this.riderId = riderId;
            this.customerId = customerId;
            this.riderName = riderName;
            this.customerName = customerName;
            this.date = date;
            this.rate = rate;
            this.quantity = quantity;
            this.address = address;
            this.bill = bill;
        }
        
        public Bill()
        {
            this.id = 0;
            this.riderId = 0;
            this.customerId = 0;
            this.riderName = null;
            this.customerName = null;
            this.rate = 0;
            this.quantity = 0;
            this.bill = 0;
        }
    }
}