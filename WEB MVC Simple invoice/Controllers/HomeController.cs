using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_MVC_Simple_invoice.Controllers;
using WEB_MVC_Simple_invoice.Models;
namespace WEB_MVC_Simple_invoice.Controllers
{






    public class HomeController : Controller
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["mvcconnectionstring"].ConnectionString;

        [HttpGet]
        public ActionResult Index()
        {
            int newInvoiceNumber = GetNewInvoiceNumber();

            var model = new Index
            {
                InvoiceNumber = newInvoiceNumber
            };

            model.ItemNames = GetItemNames();


            return View(model);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        private int GetNewInvoiceNumber()
        {
            int newInvoiceNumber;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();


                string query = "SELECT ISNULL(MAX(INVO_NO), 0) as maxno  FROM TBL_INVOICE";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();


                    reader.Read();
                    int maxInvoiceNumber = Convert.ToInt32(reader["maxno"].ToString());
                    newInvoiceNumber = maxInvoiceNumber + 1;


                    return newInvoiceNumber;


                }

                con.Close();
            }


        }


        [HttpPost]
        public ActionResult Index(Index model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlTransaction transaction = con.BeginTransaction())
                        {
                            try
                            {

                                //query 1
                                //string query1 = "INSERT INTO TBL_CUSTOMER (CUST_ID , CUST_NAME , CUST_ADDRESS) VALUES (@custid,@custname,@custaddress)";
                                //using (SqlCommand cmd1 = new SqlCommand(query1, con, transaction))
                                //{
                                //    cmd1.Parameters.AddWithValue("@custid", model.CustomerCode);
                                //    cmd1.Parameters.AddWithValue("@custname", model.CustomerName);
                                //    cmd1.Parameters.AddWithValue("@custaddress", model.CustomerAddress);
                                //    cmd1.ExecuteNonQuery();
                                //}
                                ////query 2
                                //string query2 = "INSERT INTO TBL_ITEMS (ITEM_CODE,ITEM_NAME,ITEM_PRICE) VALUES (@itemcode,@itemname,@itemprice)";
                                //using (SqlCommand cmd2 = new SqlCommand(query2, con, transaction))
                                //{
                                //    cmd2.Parameters.AddWithValue("@itemcode", model.ItemCode);
                                //    cmd2.Parameters.AddWithValue("@itemname", model.ItemName);
                                //    cmd2.Parameters.AddWithValue("@itemprice", model.ItemPrice);

                                //    cmd2.ExecuteNonQuery();
                                //}
                                //query3
                                string query3 = "INSERT INTO TBL_INVOICE (INVO_NO,INVO_AMOUNT) VALUES (@invono,@invoamount)";
                                using (SqlCommand cmd3 = new SqlCommand(query3, con, transaction))
                                {
                                    cmd3.Parameters.AddWithValue("@invono", model.InvoiceNumber);
                                    //cmd3.Parameters.AddWithValue("@invodate", model.InvoiceDate);
                                    cmd3.Parameters.AddWithValue("@invoamount", model.InvoiceAmount);
                                    cmd3.ExecuteNonQuery();

                                };
                                //query 4
                                string query4 = "INSERT INTO TBL_INVOICE_DETAILS (ISSUE_QTY , ITEM_AMOOUNT) VALUES (@issueqty, @itemamount)";
                                using (SqlCommand cmd4 = new SqlCommand(query4, con, transaction))
                                {
                                    cmd4.Parameters.AddWithValue("@Vissueqty", model.Quantity);
                                    cmd4.Parameters.AddWithValue("@itemamount", model.Amount);
                                    cmd4.ExecuteNonQuery();
                                };

                                //query 5
                                string query5 = "INSERT INTO Table1 (Column1) VALUES (@Value1)";
                                using (SqlCommand cmd5 = new SqlCommand(query5, con, transaction))
                                {
                                    cmd5.Parameters.AddWithValue("@Value1", model.SelectedItemName);
                                    cmd5.ExecuteNonQuery();
                                };
                                //query 6
                                string query6 = "INSERT INTO Table1 (Column1) VALUES (@Value1)";
                                using (SqlCommand cmd6 = new SqlCommand(query6, con, transaction))
                                {
                                    cmd6.Parameters.AddWithValue("@Value1", model.SelectedItemName);
                                    cmd6.ExecuteNonQuery();
                                };
                            }


                            catch (Exception Ex)
                            {
                                transaction.Rollback();
                                ViewBag.Massage1 = "Error occured" + Ex.Message;
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    ViewBag.Massage1 = "Database Error" + Ex.Message;
                }

            }
            return View(model);
        }
        public IEnumerable<SelectListItem> GetItemNames()
        {
            var itemNames = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT ITEM_NAME FROM TBL_ITEMS";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            itemNames.Add(new SelectListItem
                            {

                                Value = reader["ITEM_NAME"].ToString(),
                                Text = reader["ITEM_NAME"].ToString()


                            });

                        }

                    }
                }

            }
            return itemNames;
        }
    }
}