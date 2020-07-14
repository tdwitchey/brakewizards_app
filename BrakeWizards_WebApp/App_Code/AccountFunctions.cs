using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace BrakeWizards_WebApp.App_Code
{
    public class AccountFunctions
    {
        private AccountFunctions()
        {

        }

        public static void loadAccountData(Label welcomeLabel, Label firstNameLabel, Label lastNameLabel, Label usernameLabel, Label emailLabel, Label addressLabel1, Label addressLabel2, Label phoneLabel)
        {
            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's customer and account data
            string sql = "SELECT * FROM customers c JOIN accounts a ON c.Customer_ID = a.Customer_ID WHERE Acc_Username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                welcomeLabel.Text = reader.GetString("C_First_Name");
                firstNameLabel.Text = reader.GetString("C_First_Name");
                lastNameLabel.Text = reader.GetString("C_Last_Name");
                usernameLabel.Text = reader.GetString("Acc_Username");
                emailLabel.Text = reader.GetString("Acc_Email");
                addressLabel1.Text = reader.GetString("C_Address");
                addressLabel2.Text = reader.GetString("C_City") + ", " + reader.GetString("C_State") + " " + reader.GetString("C_Zip");
                phoneLabel.Text = reader.GetString("C_Phone");
            }
            reader.Close();
            conn.Close();
        }

        //=============================
        //Check & Sanitize input
        //=============================
        public static void checkUserInput_Personal(TextBox txtFirstName, TextBox txtLastName, TextBox txtAddress, TextBox txtCity, DropDownList statesList, TextBox txtZip, TextBox txtPhone)
        {
            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtAddress.Text == "" || txtCity.Text == "" || txtZip.Text == "" || txtPhone.Text == "")
            {
                HttpContext.Current.Response.Write("<script>alert('All fields are required');</script>");
            }
            else
            {
                string input1 = @txtFirstName.Text,
                       input2 = @txtLastName.Text,
                       input3 = @txtAddress.Text,
                       input4 = @txtCity.Text,
                       input5 = @txtZip.Text,
                       input6 = @txtPhone.Text;

                if (!Regex.IsMatch(input5, "^\\d{5}$"))
                {
                    HttpContext.Current.Response.Write("<script>alert('Please enter zip code in correct format #####');</script>");
                }
                else if (!Regex.IsMatch(input6, "^[2-9]\\d{2}-\\d{3}-\\d{4}$"))
                {
                    HttpContext.Current.Response.Write("<script>alert('Please enter phone number in correct format ###-###-####');</script>");
                }
                else
                {
                    txtFirstName.Text = input1;
                    txtLastName.Text = input2;
                    txtAddress.Text = input3;
                    txtCity.Text = input4;
                    txtZip.Text = input5;
                    txtPhone.Text = input6;

                    string connection = SQLFunction.sqlConnection();
                    MySqlConnection conn = new MySqlConnection(connection);
                    conn.Open();

                    string sql = "UPDATE customers c JOIN accounts a ON c.Customer_ID = a.Customer_ID SET C_First_Name = @firstName, C_Last_Name = @lastName, C_Address = @address, C_City = @city, C_State = @state, C_Zip = @zip, C_Phone = @phone WHERE Acc_Username = @username";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
                    cmd.Parameters.Add(new MySqlParameter("@firstName", txtFirstName.Text));
                    cmd.Parameters.Add(new MySqlParameter("@lastName", txtLastName.Text));
                    cmd.Parameters.Add(new MySqlParameter("@address", txtAddress.Text));
                    cmd.Parameters.Add(new MySqlParameter("@city", txtCity.Text));
                    cmd.Parameters.Add(new MySqlParameter("@state", statesList.SelectedValue));
                    cmd.Parameters.Add(new MySqlParameter("@zip", txtZip.Text));
                    cmd.Parameters.Add(new MySqlParameter("@phone", txtPhone.Text));
                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Close();
                    conn.Close();

                    HttpContext.Current.Response.Write("<script>alert('Your information has been updated. Please login again.');window.location='login.aspx';</script>");
                    HttpContext.Current.Session.Clear();
                }
            }
        }

        public static void checkUserInput_Account(TextBox txtEmail)
        {
            if (txtEmail.Text == "")
            {
                HttpContext.Current.Response.Write("<script>alert('All fields are required');</script>");
            }
            else
            {
                string input = @txtEmail.Text;

                if (!Regex.IsMatch(input, "^[A-Za-z0-9]+(-|.)?[A-Za-z0-9]+@[A-Za-z]+.[A-Za-z]+$"))
                {
                    HttpContext.Current.Response.Write("<script>alert('Please enter a valid email address.');</script>");
                }
                else
                {
                    txtEmail.Text = input;

                    string connection = SQLFunction.sqlConnection();
                    MySqlConnection conn = new MySqlConnection(connection);
                    conn.Open();

                    string sql = "UPDATE accounts SET Acc_Email = @email WHERE Acc_Username = @username";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
                    cmd.Parameters.Add(new MySqlParameter("@email", txtEmail.Text));
                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Close();
                    conn.Close();

                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Response.Write("<script>alert('Your information has been updated. Please login again.');window.location='login.aspx';</script>");
                }
            }
        }

        public static void checkUserInput_Password(TextBox txtOldPass, TextBox txtNewPass, TextBox txtRetype)
        {
            bool checkPass = false;

            if (txtOldPass.Text == "" || txtNewPass.Text == "" || txtRetype.Text == "")
            {
                HttpContext.Current.Response.Write("<script>alert('All fields are required');</script>");
            }
            else
            {
                string input1 = @txtOldPass.Text,
                       input2 = @txtNewPass.Text,
                       input3 = @txtRetype.Text;

                string connection = SQLFunction.sqlConnection();
                MySqlConnection conn = new MySqlConnection(connection);
                conn.Open();

                string sql = "SELECT COUNT(Acc_Password) AS Pass FROM accounts WHERE Acc_Username = @username AND Acc_Password = SHA1(@password)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
                cmd.Parameters.Add(new MySqlParameter("@password", input1));
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (Convert.ToInt32(reader.GetString("Pass")) <= 0)
                    {
                        checkPass = false;
                    }
                    else
                    {
                        checkPass = true;
                    }
                }

                reader.Close();
                conn.Close();

                if (checkPass == true)
                {
                    if (input1 == input2)
                    {
                        HttpContext.Current.Response.Write("<script>alert('New password cannot match old password!');</script>");
                    }
                    else if (input2 != input3)
                    {
                        HttpContext.Current.Response.Write("<script>alert('New passwords must match!');</script>");
                    }
                    else
                    {
                        connection = SQLFunction.sqlConnection();
                        conn = new MySqlConnection(connection);
                        conn.Open();

                        sql = "UPDATE accounts SET Acc_Password = SHA1(@newpass) WHERE Acc_Username = @username";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
                        cmd.Parameters.Add(new MySqlParameter("@newpass", txtNewPass.Text));
                        reader = cmd.ExecuteReader();

                        reader.Close();
                        conn.Close();

                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Response.Write("<script>alert('Your information has been updated. Please login again.');window.location='login.aspx';</script>");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Write("<script>alert('Your old password was entered incorrectly, please try again.');</script>");
                }
            }
        }
    }
}