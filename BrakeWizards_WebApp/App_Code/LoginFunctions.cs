using MySql.Data.MySqlClient;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace BrakeWizards_WebApp.App_Code
{
    public class LoginFunctions
    {
        private LoginFunctions()
        {

        }

        public static void check_login(TextBox userText, TextBox passText)
        {
            HttpContext.Current.Session["username"] = userText.Text;
            HttpContext.Current.Session["password"] = passText.Text;

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's number of incident requests from database
            string sql = "SELECT COUNT(Account_ID) AS AccValid FROM accounts WHERE Acc_Username = @username AND Acc_Password = SHA1(@password)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            cmd.Parameters.Add(new MySqlParameter("@password", HttpContext.Current.Session["password"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int validate = Convert.ToInt32(reader.GetString("AccValid"));
                if (validate == 1)
                {
                    HttpContext.Current.Response.Redirect("dashboard.aspx");
                }
                else
                {
                    HttpContext.Current.Response.Write("<script>alert('Incorrect username or password.');</script>");
                    HttpContext.Current.Session["username"] = null;
                    HttpContext.Current.Session["password"] = null;
                }
            }
            reader.Close();
            conn.Close();
        }
    }
}