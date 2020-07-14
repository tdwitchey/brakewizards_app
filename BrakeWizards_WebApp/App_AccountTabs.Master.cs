using BrakeWizards_WebApp.App_Code;
using System;

namespace BrakeWizards_WebApp
{
    public partial class App_AccountTabs : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionFunctions.validateLogin();
            AccountFunctions.loadAccountData(welcomeLabel, firstNameLabel, lastNameLabel, usernameLabel, emailLabel, addressLabel1, addressLabel2, phoneLabel);
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            SessionFunctions.logoutSession();
        }
    }
}