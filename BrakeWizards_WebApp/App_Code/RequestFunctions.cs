using MySql.Data.MySqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace BrakeWizards_WebApp.App_Code
{
    public class RequestFunctions
    {
        private RequestFunctions()
        {

        }

        public static void loadFormData(Label lblCustomerName, Label lblCurrentDate, RadioButton rbRepairType1, RadioButton rbRepairType2, DropDownList carMakeDropdown)
        {
            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string sql = "SELECT C_First_Name, C_Last_Name, DATE_FORMAT(CURDATE(), '%m/%d/%Y') AS Date FROM customers c JOIN accounts a ON c.Customer_ID = a.Customer_ID WHERE Acc_Username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lblCustomerName.Text = (reader.GetString("C_First_Name") + " " + reader.GetString("C_Last_Name"));
                lblCurrentDate.Text = reader.GetString("Date");
            }

            reader.Close();
            conn.Close();

            string[] makeArray = { "Dodge", "Ford", "General Motors", "Honda", "Hyundai", "Mercedes-Benz", "Nissan", "Subaru", "Toyota", "Volkswagen" };
            for (int i = 0; i < makeArray.Length; i++)
            {
                carMakeDropdown.Items.Add(makeArray[i]);
            }
        }

        public static void submitRequest(RadioButton rbRepairType1, RadioButton rbRepairType2, DropDownList carMakeDropdown)
        {
            string repairType = "";

            if (rbRepairType1.Checked)
            {
                repairType = "Basic Repair";
            }
            else if (rbRepairType2.Checked)
            {
                repairType = "Complete Repair";
            }

            string carMake = carMakeDropdown.SelectedValue;

            string incidentDescription = carMake + " " + repairType;

            string chooseKit = incidentDescription + " Kit";

            //default supervisor id for all requests will be 7 for assistant location manager to reassign to a technician supervisor

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Insert new Incident Request
            string sql = "INSERT INTO incident_requests (Customer_ID, Date_Completed, Date_Requested, Date_Started, Incident_Description, Package_ID, Supervisor_ID) VALUES ((SELECT c.Customer_ID FROM customers c JOIN accounts a ON c.Customer_ID = a.Customer_ID WHERE Acc_Username = @username), NULL, CURDATE(), NULL, @incidentDescription, (SELECT Package_ID FROM repair_packages WHERE Package_Name = @packageName), 7)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            cmd.Parameters.Add(new MySqlParameter("@incidentDescription", @incidentDescription));
            cmd.Parameters.Add(new MySqlParameter("@packageName", @chooseKit));
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Close();
            conn.Close();

            HttpContext.Current.Response.Write("<script>alert('Thank you for submitting your request.');window.location='dashboard.aspx';</script>");
        }
    }
}