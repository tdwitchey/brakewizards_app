using BrakeWizards_WebApp.App_Code;
using System;

namespace BrakeWizards_WebApp
{
    public partial class payment : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            bool valid = CheckFunctions.checkInvoices();
            if (valid)
            {
                PaymentFunctions.loadPayment_Data(invoicePaymentDropDown);
                PaymentFunctions.loadInvoiceInfo(invoicePaymentDropDown, itemDescAndCostList, invoiceItemsLabel, customerLabel, dueDateLabel, totalLabel, packageNameLabel);
            }
            
        }

        protected void selectInvoiceButton_Click(object sender, EventArgs e)
        {
            PaymentFunctions.loadInvoiceInfo(invoicePaymentDropDown, itemDescAndCostList, invoiceItemsLabel, customerLabel, dueDateLabel, totalLabel, packageNameLabel);
        }
    }
}