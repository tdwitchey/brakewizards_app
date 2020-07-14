using MySql.Data.MySqlClient;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace BrakeWizards_WebApp.App_Code
{
    public class PaymentFunctions
    {
        private static string[] invoiceIDs = new string[] { };

        private PaymentFunctions()
        {
            
        }

        private static void setArray(int length)
        {
            System.Array.Resize(ref invoiceIDs, length);
        }

        private static string[] getArray()
        {
            return invoiceIDs;
        }

        public static void loadPayment_Data(DropDownList invoiceDropDown)
        {
            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get user's completed invoice ids from database
            string sqlCount = "SELECT COUNT(Invoice_ID) AS ArraySize FROM invoices i JOIN accounts a ON i.Account_ID = a.Account_ID WHERE Acc_Username = @username";
            MySqlCommand cmdCount = new MySqlCommand(sqlCount, conn);
            cmdCount.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader readerCount = cmdCount.ExecuteReader();

            while (readerCount.Read())
            {
                setArray(Convert.ToInt32(readerCount.GetString("ArraySize")));
            }
            readerCount.Close();

            string sql = "SELECT DATE_FORMAT(Invoice_Date, '%m/%d/%Y') AS Invoice_Date, Invoice_ID FROM invoices i JOIN accounts a ON i.Account_ID = a.Account_ID WHERE Acc_Username = @username";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@username", HttpContext.Current.Session["username"]));
            MySqlDataReader reader = cmd.ExecuteReader();

            int index = 0;
            if (invoiceDropDown.Items.Count <= 0)
            {
                while (reader.Read())
                {
                    invoiceDropDown.Items.Insert(index, reader.GetString("Invoice_Date"));
                    getArray().SetValue(reader.GetString("Invoice_ID"), index);
                    index++;
                }
            }

            reader.Close();
        }

        public static void loadInvoiceInfo(DropDownList invoiceDropDown, Label itemDCList, Label invoiceItems, Label customerLabel, Label dueDateLabel, Label totalLabel, Label packageNameLabel)
        {
            string invoiceID = getArray()[invoiceDropDown.SelectedIndex];

            //clear list labels
            itemDCList.Text = "";
            invoiceItems.Text = "";

            string connection = SQLFunction.sqlConnection();
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();

            //-------------------------------------------------------------------------------------------------
            //Get invoice details based on selected invoiceID
            string sql = "SELECT C_First_Name, C_Last_Name, DATE_FORMAT(Invoice_Due_Date, '%m/%d/%Y') AS Date, Invoice_Total FROM invoices i JOIN invoice_line_items ili ON i.Invoice_ID = ili.Invoice_ID JOIN accounts a ON i.Account_ID = a.Account_ID JOIN customers c ON a.Customer_ID = c.Customer_ID WHERE i.Invoice_ID = @invoiceID";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@invoiceID", invoiceID));
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                customerLabel.Text = (reader.GetString("C_First_Name") + " " + reader.GetString("C_Last_Name"));
                dueDateLabel.Text = reader.GetString("Date");
                totalLabel.Text = "$" + reader.GetString("Invoice_Total");
            }
            reader.Close();

            //--------------------------------------------------------------------
            sql = "SELECT * FROM invoice_line_items WHERE Invoice_ID = @invoiceID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@invoiceID", invoiceID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                invoiceItems.Text += (reader.GetString("Item_Description") + " - $" + reader.GetString("Item_Amount") + "<br />");
            }
            reader.Close();

            //--------------------------------------------------------------------
            sql = "SELECT * FROM invoices i JOIN incident_requests ir ON i.Incident_ID = ir.Incident_ID JOIN repair_packages rp ON ir.Package_ID = rp.Package_ID JOIN package_items pi ON rp.Package_ID = pi.Package_ID JOIN inventory inv ON pi.Part_ID = inv.Part_ID WHERE Invoice_ID = @invoiceID";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@invoiceID", invoiceID));
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                packageNameLabel.Text = reader.GetString("Package_Name");
                itemDCList.Text += (reader.GetString("Description") + " - $" + reader.GetString("Unit_Price") + " x " + reader.GetString("Amount_Needed") + "<br />");
            }
            reader.Close();
        }
    }
}