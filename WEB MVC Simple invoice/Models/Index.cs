using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace WEB_MVC_Simple_invoice.Models
{
    public class Index
    {
      public string CustomerCode { get; set; }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string NoOfItems { get; set; }
        public string ReportDetails { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string InvoiceAmount { get; set; }

        public string SelectedItemName { get; set; }
        public IEnumerable<SelectListItem> ItemNames { get; set; }  



    }
}