using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using BrakeWizards_WebApp.App_Code;

namespace BrakeWizards_WebApp
{
    public partial class dashboard_account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> states = new List<string>(50) { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };
            for (int i = 0; i < 50; i++)
            {
                statesList.Items.Insert(i, states[i]);
            }

            
        }

        protected void btnSubmit_Personal_Click(object sender, EventArgs e)
        {
            AccountFunctions.checkUserInput_Personal(txtFirstName, txtLastName, txtAddress, txtCity, statesList, txtZip, txtPhone);
        }
    }
}