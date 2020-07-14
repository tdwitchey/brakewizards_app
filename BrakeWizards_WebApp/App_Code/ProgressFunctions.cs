using MySql.Data.MySqlClient;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace BrakeWizards_WebApp.App_Code
{
    public class ProgressFunctions
    {
        private static string[] incidentIDs = new string[] { };

        private ProgressFunctions()
        {

        }

        private static void setArray(int length)
        {
            System.Array.Resize(ref incidentIDs, length);
        }

        private static string[] getArray()
        {
            return incidentIDs;
        }

        public static void loadProgress_Data(DropDownList incidentProgressDropdown)
        {
            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's active incident_request ids from database
            string sqlCount = "SELECT COUNT(Incident_ID) AS ArraySize FROM incident_requests ir JOIN accounts a ON ir.Customer_ID = a.Customer_ID WHERE Acc_Username = @username";
            MySqlCommand cmdCount = new MySqlCommand(sqlCount, conn);
            cmdCount.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader readerCount = cmdCount.ExecuteReader();

            while (readerCount.Read())
            {
                setArray(Convert.ToInt32(readerCount.GetString("ArraySize")));
            }
            readerCount.Close();

            //-------------------------------------------------------------------------------------------------
            //Get user's active incident requests from database
            string sql = "SELECT DATE_FORMAT(Date_Requested, '%m/%d/%Y') AS Incident_Date, Incident_ID FROM incident_requests ir JOIN accounts a ON ir.Customer_ID = a.Customer_ID WHERE Acc_Username = @username AND Date_Started IS NOT NULL AND Date_Completed IS NULL";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            int index = 0;
            if (incidentProgressDropdown.Items.Count <= 0)
            {
                while (reader.Read())
                {
                    if (index != 0 && incidentProgressDropdown.Items[index - 1].Value == reader.GetString("Incident_Date"))
                    {
                        incidentProgressDropdown.Items.Insert(index, reader.GetString("Incident_Date") + " (" + (index + 1) + ")");
                    }
                    else
                    {
                        incidentProgressDropdown.Items.Insert(index, reader.GetString("Incident_Date"));
                    }
                    getArray().SetValue(reader.GetString("Incident_ID"), index);
                    index++;
                }
            }

            reader.Close();
            conn.Close();
        }

        public static void loadIncidentInfo(DropDownList incidentProgressDropdown, Label technicianListLabel, Label dateStartedLabel, Label incidentDescriptionLabel, Label supervisorNameLabel, Label packageNameLabel, Label packageCostLabel)
        {
            string incidentID = getArray()[incidentProgressDropdown.SelectedIndex];

            //clear list labels
            technicianListLabel.Text = "";

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get incident details based on selected incident
            string sql = "SELECT DATE_FORMAT(Date_Started, '%m/%d/%Y') AS Date_Started, Incident_Description FROM incident_requests WHERE Incident_ID = @incidentID";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dateStartedLabel.Text = reader.GetString("Date_Started");
                incidentDescriptionLabel.Text = reader.GetString("Incident_Description");
            }
            reader.Close();

            //---------------------------------------
            sql = "SELECT S_First_Name, S_Last_Name FROM supervisors s JOIN incident_requests ir ON s.Supervisor_ID = ir.Supervisor_ID WHERE Incident_ID = @incidentID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                supervisorNameLabel.Text = (reader.GetString("S_First_Name") + " " + reader.GetString("S_Last_Name"));
            }
            reader.Close();

            //---------------------------------------
            sql = "SELECT E_First_Name, E_Last_Name FROM employees e JOIN incident_requests ir ON e.Supervisor_ID = ir.Supervisor_ID WHERE Incident_ID = @incidentID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                technicianListLabel.Text += (reader.GetString("E_First_Name") + " " + reader.GetString("E_Last_Name") + "<br />");
            }
            reader.Close();

            //---------------------------------------
            sql = "SELECT Package_Name FROM repair_packages rp JOIN incident_requests ir ON rp.Package_ID = ir.Package_ID WHERE Incident_ID = @incidentID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                packageNameLabel.Text = reader.GetString("Package_Name");
            }
            reader.Close();

            //---------------------------------------
            sql = "SELECT ROUND(SUM(Unit_Price * Amount_Needed), 2) AS PackageCost FROM inventory i JOIN package_items pi ON i.Part_ID = pi.Part_ID JOIN incident_requests ir ON pi.Package_ID = ir.Package_ID WHERE Incident_ID = @incidentID LIMIT 1";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                packageCostLabel.Text = reader.GetString("PackageCost");
            }
            reader.Close();
        }

        public static int loadProgressStats(DropDownList incidentProgressDropdown, Label progress1Label, Label progress2Label, Label progress3Label)
        {
            string incidentID = getArray()[incidentProgressDropdown.SelectedIndex];

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string sql = "SELECT DATEDIFF(CURRENT_DATE(), Date_Requested) AS daysSince FROM incident_requests WHERE Incident_ID = @incidentID";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                progress1Label.Text = reader.GetString("daysSince");
            }
            reader.Close();

            //---------------------------------------
            sql = "SELECT DATEDIFF(Date_Started, Date_Requested) AS daysBetween FROM incident_requests WHERE Incident_ID = @incidentID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                progress2Label.Text = reader.GetString("daysBetween");
            }
            reader.Close();

            //---------------------------------------
            sql = "SELECT Supervisor_ID FROM incident_requests WHERE Incident_ID = @incidentID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@incidentID", incidentID));
            reader = cmd.ExecuteReader();

            string supervisorID = "";

            while (reader.Read())
            {
                supervisorID = reader.GetString("Supervisor_ID");
            }
            reader.Close();


            sql = "SELECT (COUNT(Supervisor_ID) - 1) AS Projects FROM incident_requests WHERE Supervisor_ID = @supervisorID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@supervisorID", supervisorID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                progress3Label.Text = reader.GetString("Projects");
            }
            reader.Close();
            conn.Close();

            //-----------------------------------------------------------
            //Estimate progress on a scale from 0 to 10
            int daysBetween = Convert.ToInt32(progress2Label.Text);
            int otherProjects = Convert.ToInt32(progress3Label.Text);

            int progress1, progress2, calculation;

            switch (daysBetween)
            {
                case 0:
                case 1:
                case 2:
                    progress1 = 5;
                    break;
                case 3:
                case 4:
                    progress1 = 4;
                    break;
                case 5:
                case 6:
                    progress1 = 3;
                    break;
                case 7:
                case 8:
                    progress1 = 2;
                    break;
                case 9:
                case 10:
                    progress1 = 1;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                    progress1 = 0;
                    break;
                default:
                    progress1 = 0;
                    break;
            }

            switch (otherProjects)
            {
                case 0:
                    progress2 = 4;
                    break;
                case 1:
                    progress2 = 3;
                    break;
                case 2:
                    progress2 = 2;
                    break;
                case 3:
                case 4:
                    progress2 = 1;
                    break;
                case 5:
                    progress2 = 0;
                    break;
                default:
                    progress2 = 0;
                    break;
            }

            calculation = (progress1 + progress2);

            return calculation;
        }

        //=====================change to supervisor only
        public static void loadContactButtonInfo(LinkButton btnSupervisor, Label supervisorNameLabel)
        {
            string supervisorName = supervisorNameLabel.Text;
            btnSupervisor.Text = supervisorName;

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            string sql = "SELECT S_Email FROM supervisors WHERE CONCAT(S_First_Name, ' ', S_Last_Name) = @supervisor";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@supervisor", supervisorName));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                btnSupervisor.OnClientClick = "window.open('mailto:" + reader.GetString("S_Email") + "', 'email')";
            }
            reader.Close();
        }
    }
}