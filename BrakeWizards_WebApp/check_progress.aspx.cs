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
    public partial class check_progress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool valid = CheckFunctions.checkIncidents();
            if (valid)
            {
                ProgressFunctions.loadProgress_Data(incidentProgressDropdown);
                ProgressFunctions.loadIncidentInfo(incidentProgressDropdown, technicianListLabel, dateStartedLabel, incidentDescriptionLabel, supervisorNameLabel, packageNameLabel, packageCostLabel);
                estimatedProgressBar.Attributes["value"] = ProgressFunctions.loadProgressStats(incidentProgressDropdown, progress1Label, progress2Label, progress3Label).ToString();
            }
            
        }

        protected void selectIncidentButton_Click(object sender, EventArgs e)
        {
            ProgressFunctions.loadIncidentInfo(incidentProgressDropdown, technicianListLabel, dateStartedLabel, incidentDescriptionLabel, supervisorNameLabel, packageNameLabel, packageCostLabel);
        }

        protected void contactButton_Click(object sender, EventArgs e)
        {
            contactPanel.Visible = true;
            ProgressFunctions.loadContactButtonInfo(btnSupervisor, supervisorNameLabel);
        }

        protected void closePanel_Click(object sender, EventArgs e)
        {
            contactPanel.Visible = false;
        }

        protected void btnContact_Select_Click(object sender, EventArgs e)
        {

        }
    }
}