using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PaymentGateway
{
    public partial class Test : System.Web.UI.Page
    {
        Transaction.Transaction transaction = new Transaction.Transaction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (!IsPostBack)
            {
                loadDrpMerchant();
                loadDrpTransactionID();
                loadCurrency();
                loadBank();

                if (Request.QueryString["action"] == "success")
                {
                    alertSuccess.Visible = true;
                    alertSuccess.InnerHtml = "Thank you! Your payment has been received!<br/>Your Transaction ID is " + Request.QueryString["transactionID"] + ".";
                }
                else if (Request.QueryString["action"] == "failed")
                {
                    alertDanger.Visible = true;
                    if (string.IsNullOrEmpty(Request.QueryString["bankResponse"]) == true)
                    {
                        alertDanger.InnerHtml = "Sorry, payment failed!";
                    }
                    else
                    {
                        alertDanger.InnerHtml = "Sorry, payment failed!<br/>Bank Response: " + Request.QueryString["bankResponse"];
                    }
                }
            }
        }

        protected void BtnSubmitPayment_Click(object sender, EventArgs e)
        {

            string redirectURL = "frmPaymentGateway.aspx";
            try
            {
                Guid transactionID = Guid.NewGuid();
                transaction.processPayment(transactionID.ToString(), DrpMerchant.SelectedValue, DrpBank.SelectedValue, DrpCurrency.SelectedValue, DrpCardType.SelectedValue, TxtCardNumber.Text, TxtNameOnCard.Text, DrpExpiryMonth.SelectedItem.Text, TxtExpiryYear.Text, TxtCVV.Text, TxtAmount.Text);

                var bankResponseDataTypes = getBankResponse(TxtCardNumber.Text, DrpExpiryMonth.SelectedValue, TxtExpiryYear.Text, TxtCVV.Text, TxtAmount.Text);

                string status = "";

                if (bankResponseDataTypes.BankResponse != "Successful")
                    status = "Failed";
                else status = "Success";

                transaction.UpdateStatusPaymentTransaction(transactionID.ToString(), bankResponseDataTypes.BankResponse, status, bankResponseDataTypes.BankResponseID);

                if (bankResponseDataTypes.BankResponse != "Successful")
                {
                    redirectURL = "PaymentGateway.aspx?action=failed&bankResponse=" + bankResponseDataTypes.BankResponse;
                }
                else
                {
                    redirectURL = "PaymentGateway.aspx?action=success&transactionID=" + transactionID.ToString();
                }

            }
            catch (Exception)
            {
                redirectURL = "PaymentGateway.aspx?action=failed";
            }

            Response.Redirect(redirectURL, false);
        }

        protected void BtnSearchByMerchant_Click(object sender, EventArgs e)
        {
            LoadGridViewTransactions();
        }

        protected void LoadGridViewTransactions()
        {
            try
            {
                GridViewTransactions.DataSource = transaction.retrievePaymentTransactionByMerchantID(DrpSearchByMerchant.SelectedValue);
                GridViewTransactions.DataBind();
            }
            catch { }
        }

        protected void BtnSearchByTransactionID_Click(object sender, EventArgs e)
        {
            loadDataListTransactionDetails();
        }

        protected DataTypes.viewBankResponseDataType getBankResponse(string cardNumber, string expiryMonth, string expiryYear, string CVV, string Amount)
        {
            int i = 0;
            Guid bankResponseID = Guid.NewGuid();
            var bankResponseDataType = new DataTypes.viewBankResponseDataType();

            if (!(CVV.Length == 3) && (int.TryParse(CVV, out i) == true))
                bankResponseDataType.BankResponse = "Security Breach";
            else if (Int32.Parse(Amount) > 100000)
                bankResponseDataType.BankResponse = "Not enough fund";
            else if (!(cardNumber.Length == 12) && (int.TryParse(cardNumber, out i) == true))
                bankResponseDataType.BankResponse = "Invalid Card Number";
            else if (Int32.Parse(expiryYear) < DateTime.Now.Year)
                bankResponseDataType.BankResponse = "Card has expired";
            else if ((Int32.Parse(expiryYear) == DateTime.Now.Year) && (Int32.Parse(expiryMonth) < DateTime.Now.Month))
                bankResponseDataType.BankResponse = "Card has expired";
            else
                bankResponseDataType.BankResponse = "Successful";

            bankResponseDataType.BankResponseID = bankResponseID;

            return bankResponseDataType;
        }

        protected void loadDrpMerchant()
        {
            try
            {
                DrpMerchant.DataSource = transaction.getMerchants();
                DrpMerchant.DataBind();

                DrpSearchByMerchant.DataSource = transaction.getMerchants();
                DrpSearchByMerchant.DataBind();

                ListItem liFirst = new ListItem("", "");
                DrpMerchant.Items.Insert(0, liFirst);
                DrpSearchByMerchant.Items.Insert(0, liFirst);
            }
            catch { }
        }

        protected void loadDrpTransactionID()
        {
            try
            {
                DrpSearchByTransactionID.DataSource = transaction.getTransactions();
                DrpSearchByTransactionID.DataBind();

                ListItem liFirst = new ListItem("", "");
                DrpSearchByTransactionID.Items.Insert(0, liFirst);
            }
            catch { }
        }

        protected void loadCurrency()
        {
            try
            {
                DrpCurrency.DataSource = transaction.getCurrencies();
                DrpCurrency.DataBind();

                ListItem liFirst = new ListItem("", "");
                DrpCurrency.Items.Insert(0, liFirst);
            }
            catch { }
        }

        protected void loadBank()
        {
            try
            {
                DrpBank.DataSource = transaction.getBanks();
                DrpBank.DataBind();

                ListItem liFirst = new ListItem("", "");
                DrpBank.Items.Insert(0, liFirst);
            }
            catch { }
        }

        protected void loadDataListTransactionDetails()
        {
            try
            {
                DataListTransactionDetails.DataSource = transaction.retrievePaymentTransactionByTransactionID(DrpSearchByTransactionID.SelectedValue);
                DataListTransactionDetails.DataBind();
            }
            catch { }
        }

        public class DataTypes
        {
            public class viewBankResponseDataType
            {
                public Guid BankResponseID;
                public string BankResponse;
            }
        }
    }
}