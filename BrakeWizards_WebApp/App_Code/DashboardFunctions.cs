using MySql.Data.MySqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace BrakeWizards_WebApp.App_Code
{
    public class DashboardFunctions
    {
        private DashboardFunctions()
        {
            
        }

        public static void loadDashboard_Data(Label request, Label balance, Label dueDate)
        {
            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's number of incident requests from database
            string sql = "SELECT COUNT(*) AS NumberOfRequests FROM incident_requests i JOIN accounts a ON i.Customer_ID = a.Customer_ID WHERE Acc_Username = @username AND Date_Started IS NOT NULL AND Date_Completed IS NULL";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) { request.Text = reader.GetString("NumberOfRequests"); }
            reader.Close();

            //-------------------------------------------------------------------------------------------------
            //Get user's balance due from database
            sql = "SELECT COALESCE(SUM(ROUND(Invoice_Total - Payment_Total - Credit_Total, 2)), '0.00') AS Balance FROM invoices i JOIN accounts a ON i.Account_ID = a.Account_ID WHERE Acc_Username = @username";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            reader = cmd.ExecuteReader();

            while (reader.Read()) {
                balance.Text = reader.GetString("Balance");
            }
            reader.Close();

            //-------------------------------------------------------------------------------------------------
            //Get user's next due date from database
            sql = "SELECT DATE_FORMAT(Invoice_Due_Date, '%m/%d/%Y') AS Due_Date FROM invoices i JOIN accounts a ON i.Account_ID = a.Account_ID WHERE Acc_Username = @username ORDER BY Invoice_Due_Date DESC LIMIT 1";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            reader = cmd.ExecuteReader();

            while (reader.Read()) { dueDate.Text = reader.GetString("Due_Date"); }
            reader.Close();

            conn.Close();
        }

    }
}