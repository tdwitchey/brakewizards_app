using BrakeWizards_WebApp.App_Code;
using MySql.Data.MySqlClient;
using System;

namespace BrakeWizards_WebApp
{
    public partial class App_Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionFunctions.validateLogin();

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's customer and account data
            string sql = "SELECT C_First_Name FROM customers c JOIN accounts a ON c.Customer_ID = a.Customer_ID WHERE Acc_Username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                welcomeLabel.Text = reader.GetString("C_First_Name");
            }
            reader.Close();
            conn.Close();

        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            SessionFunctions.logoutSession();
        }
    }
}