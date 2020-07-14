using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrakeWizards_WebApp.App_Code;

namespace BrakeWizards_WebApp
{
    public partial class request_service : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionFunctions.checkRequestsSubmittedToday(SessionFunctions.getRequestsSubmittedToday());
            RequestFunctions.loadFormData(lblCustomerName, lblCurrentDate, rbRepairType1, rbRepairType2, carMakeDropdown);
        }

        protected void btnSubmit_Request_Click(object sender, EventArgs e)
        {
            RequestFunctions.submitRequest(rbRepairType1, rbRepairType2, carMakeDropdown);
        }
    }
}