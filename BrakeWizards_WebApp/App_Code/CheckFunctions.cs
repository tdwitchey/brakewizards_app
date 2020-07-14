using MySql.Data.MySqlClient;
using System;
using System.Web;

namespace BrakeWizards_WebApp.App_Code
{
    public class CheckFunctions
    {
        private CheckFunctions()
        {

        }

        public static bool checkInvoices()
        {
            bool valid = true;

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's active invoices from database
            string sql = "SELECT COUNT(Invoice_ID) AS InvoiceCount FROM invoices i JOIN accounts a ON i.Account_ID = a.Account_ID WHERE Acc_Username = @username AND (Invoice_Total - Payment_Total - Credit_Total) != 0";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            int invoiceCount = 0;
            while (reader.Read())
            {
                invoiceCount = Convert.ToInt32(reader.GetString("InvoiceCount"));
            }

            reader.Close();
            conn.Close();

            if (invoiceCount > 0)
            {
                //do nothing and continue
                valid = true;
            }
            else
            {
                valid = false;
                HttpContext.Current.Response.Write("<script>alert('You do not currently have any active invoices.');window.location='dashboard.aspx';</script>");
            }

            return valid;
        }

        public static bool checkIncidents()
        {
            bool valid = true;

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's active incident requests from database
            string sql = "SELECT COUNT(Incident_ID) AS ActiveIncidents FROM incident_requests ir JOIN accounts a ON ir.Customer_ID = a.Customer_ID WHERE Acc_Username = @username AND Date_Started IS NOT NULL AND Date_Completed IS NULL";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            int activeIncidents = 0;
            while (reader.Read())
            {
                activeIncidents = Convert.ToInt32(reader.GetString("ActiveIncidents"));
            }

            reader.Close();
            conn.Close();

            if (activeIncidents > 0)
            {
                //do nothing and continue
                valid = true;
            }
            else
            {
                valid = false;
                HttpContext.Current.Response.Write("<script>alert('You do not currently have any active incident requests.');window.location='dashboard.aspx';</script>");
            }

            return valid;
        }
    }
}