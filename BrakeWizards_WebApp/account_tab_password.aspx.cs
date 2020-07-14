using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrakeWizards_WebApp.App_Code;

namespace BrakeWizards_WebApp
{
    public partial class account_tab_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Password_Click(object sender, EventArgs e)
        {
            AccountFunctions.checkUserInput_Password(txtOldPass, txtNewPass, txtRetype);
        }
    }
}