using MySql.Data.MySqlClient;
using System;
using System.Web;

namespace BrakeWizards_WebApp.App_Code
{
    public class SessionFunctions
    {
        private SessionFunctions()
        {

        }
        public static void logoutSession()
        {
            if(HttpContext.Current.Session["username"] != null && HttpContext.Current.Session["password"] != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Response.Redirect("login.aspx");
            }
        }

        public static void validateLogin()
        {
            if (HttpContext.Current.Session["username"] == null && HttpContext.Current.Session["password"] == null)
            {
                HttpContext.Current.Response.Redirect("login.aspx");
            }
        }

        public static int getRequestsSubmittedToday()
        {
            int submitted = 0;

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get number of requests user has submitted on the current date
            string sql = "SELECT COUNT(Date_Requested) AS RequestsToday FROM incident_requests ir JOIN accounts a ON ir.Customer_ID = a.Customer_ID WHERE Acc_Username = @username AND Date_Requested = CURDATE()";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                submitted = (Convert.ToInt32(reader.GetString("RequestsToday")));
            }

            return submitted;
        }

        public static void checkRequestsSubmittedToday(int submitted)
        {
            if (submitted < 3)
            {
                //do nothing and continue
            }
            else
            {
                HttpContext.Current.Response.Write("<script>alert('Sorry, you have reached your request limit (3) for today.');window.location='dashboard.aspx';</script>");
            }
        }
    }
}