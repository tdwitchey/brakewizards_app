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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] != null && Session["password"] != null)
            {
                Response.Redirect("dashboard.aspx");
            }
        }

        protected void submitLoginButton_Click(object sender, EventArgs e)
        {
            LoginFunctions.check_login(userText, passText);
        }
    }
}