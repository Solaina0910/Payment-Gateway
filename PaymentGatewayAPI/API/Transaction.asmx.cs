using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PaymentGatewayAPI
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Transaction : System.Web.Services.WebService
    {

        [WebMethod()]
        public string processPayment(string transactionID, string merchantID, string bankID, string currencyID, string cardType, string cardNumber, string nameOnCard, string expiryMonth, string expiryYear, string CVV, string amount)
        {
            SqlConnection con = new System.Data.SqlClient.SqlConnection();
            SqlCommand cmd;
            SqlDataReader dtr;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            string sqlString = "";

            try
            {
                con.Open();

                sqlString = "INSERT INTO PaymentTransaction([TransactionID],[MerchantID],[BankID],[CurrencyID],[CardType],[CardNumber],[NameOnCard],[ExpiryMonth],[ExpiryYear],[CVV],[Amount]) VALUES (@TransactionID,@MerchantID,@BankID,@CurrencyID,@CardType,@CardNumber,@NameOnCard,@ExpiryMonth,@ExpiryYear,@CVV,@Amount)";

                cmd = new SqlCommand(sqlString, con);

                cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                cmd.Parameters.AddWithValue("@MerchantID", merchantID);
                cmd.Parameters.AddWithValue("@BankID", bankID);
                cmd.Parameters.AddWithValue("@CurrencyID", currencyID);
                cmd.Parameters.AddWithValue("@CardType", cardType);
                cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
                cmd.Parameters.AddWithValue("@NameOnCard", nameOnCard);
                cmd.Parameters.AddWithValue("@ExpiryMonth", expiryMonth);
                cmd.Parameters.AddWithValue("@ExpiryYear", expiryYear);
                cmd.Parameters.AddWithValue("@CVV", CVV);
                cmd.Parameters.AddWithValue("@Amount", amount);

                dtr = cmd.ExecuteReader();
                dtr.Close();
            }
            catch (Exception Ex) {
                throw Ex;
            }

            finally
            {
                con.Close();
                con.Dispose();
            }
            return "Success";
        }

        [WebMethod()]
        public DataSet retrievePaymentTransactionByMerchantID(string merchantID)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            SqlCommand cm;
            DataSet ds_paymentTransaction = new DataSet();
            SqlDataAdapter da;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cm = new SqlCommand();
                cm.Connection = cn;

                string sqlString = "";
                sqlString = "Select t.TransactionID, t.MerchantID, b.Name AS Bank, ISNULL(c.Abbreviation, '') + ' ' + ISNULL(CAST(t.Amount AS NVARCHAR(50)), '') AS Amount, t.CardType, " +
                            "REPLICATE('X',LEN(t.CardNumber)-4)+ RIGHT(t.CardNumber,4) AS CardNumber, t.NameOnCard, t.ExpiryMonth, t.ExpiryYear, t.CVV, t.BankResponse, t.BankResponseID, t.Status " +
                            "From PaymentTransaction t " +
                            "Inner Join Bank b on t.BankID = b.BankID " +
                            "Inner Join Currency c on t.CurrencyID = c.CurrencyID " +
                            "Where MerchantID = @MerchantID";

                cm.CommandText = sqlString;
                cm.Parameters.AddWithValue("@MerchantID", merchantID);

                da = new SqlDataAdapter(cm);
                da.Fill(ds_paymentTransaction);

            }
            catch (SqlException) { }

            catch (Exception) { }

            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return ds_paymentTransaction;
        }

        [WebMethod()]
        public DataSet retrievePaymentTransactionByTransactionID(string transactionID)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            SqlCommand cm;
            DataSet ds_paymentTransaction = new DataSet();
            SqlDataAdapter da;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cm = new SqlCommand();
                cm.Connection = cn;

                string sqlString = "";
                sqlString = "Select m.Name AS Merchant, b.Name AS Bank, ISNULL(c.Abbreviation, '') + ' ' + ISNULL(CAST(t.Amount AS NVARCHAR(50)), '') AS Amount, t.CardType, " +
                            "REPLICATE('X',LEN(t.CardNumber)-4)+ RIGHT(t.CardNumber,4) AS CardNumber, t.NameOnCard, t.ExpiryMonth, t.ExpiryYear, t.CVV, t.BankResponse, t.BankResponseID, t.Status " +
                            "From PaymentTransaction t " +
                            "Inner Join Bank b on t.BankID = b.BankID " +
                            "Inner Join Currency c on t.CurrencyID = c.CurrencyID " +
                            "Inner Join Merchant m on t.MerchantID = m.MerchantID " +
                            "Where TransactionID = @TransactionID";

                cm.CommandText = sqlString;
                cm.Parameters.AddWithValue("@TransactionID", transactionID);

                da = new SqlDataAdapter(cm);
                da.Fill(ds_paymentTransaction);

            }
            catch (SqlException) { }

            catch (Exception) { }

            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return ds_paymentTransaction;
        }


        //[WebMethod()]
        //public Transaction.DataTypes.viewPaymentTransactionDataType retrievePaymentTransactionByTransactionID(string transactionID)
        //{
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
        //    SqlCommand sqlcmd = new SqlCommand();
        //    SqlDataReader dr;

        //    var paymentTransactionDataType = new Transaction.DataTypes.viewPaymentTransactionDataType();

        //    try
        //    {
        //        sqlcmd.CommandText = "SELECT * FROM PaymentTransaction WHERE TransactionID = @TransactionID ";
        //        sqlcmd.Parameters.AddWithValue("@TransactionID", transactionID);

        //        sqlcmd.Connection = con;
        //        con.Open();
        //        dr = sqlcmd.ExecuteReader();



        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                paymentTransactionDataType.merchantID = dr["MerchantID"].ToString();
        //                paymentTransactionDataType.bankID = dr["BankID"].ToString();
        //                paymentTransactionDataType.currencyID = dr["CurrencyID"].ToString();
        //                paymentTransactionDataType.cardType = dr["CardType"].ToString();
        //                paymentTransactionDataType.cardNumber = dr["CardNumber"].ToString();
        //                paymentTransactionDataType.nameOnCard = dr["NameOnCard"].ToString();
        //                paymentTransactionDataType.expiryMonth = dr["ExpiryMonth"].ToString();
        //                paymentTransactionDataType.expiryYear = dr["ExpiryYear"].ToString();
        //                paymentTransactionDataType.CVV = dr["CVV"].ToString();
        //                paymentTransactionDataType.amount = dr["Amount"].ToString();
        //            }
        //        }

        //        dr.Close();
        //    }
        //    catch (Exception) { }

        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }

        //    return paymentTransactionDataType;
        //}

        [WebMethod()]
        public void UpdateStatusPaymentTransaction(string transactionID, string bankResponse, string status, Guid bankResponseID)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;
            SqlDataReader dtr;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            string sqlString = "";

            try
            {
                con.Open();

                sqlString = "UPDATE PaymentTransaction SET bankResponse=@BankResponse, bankResponseID=@bankResponseID, status= @status WHERE TransactionID=@TransactionID ";

                cmd = new System.Data.SqlClient.SqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@TransactionID", transactionID);
                cmd.Parameters.AddWithValue("@BankResponse", bankResponse);
                cmd.Parameters.AddWithValue("@BankResponseID", bankResponseID);
                cmd.Parameters.AddWithValue("@Status", status);

                dtr = cmd.ExecuteReader();
                dtr.Close();
            }
            catch (Exception Ex) {
                throw Ex;
            }

            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        [WebMethod()]
        public DataSet getMerchants()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            SqlCommand cm;
            DataSet ds_merchant = new DataSet();
            SqlDataAdapter da;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cm = new SqlCommand();
                cm.Connection = cn;

                string sqlString = "";
                sqlString = "Select * From Merchant WHERE Active='True'";


                cm.CommandText = sqlString;

                da = new SqlDataAdapter(cm);
                da.Fill(ds_merchant);

            }
            catch (SqlException) { }

            catch (Exception) { }

            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return ds_merchant;
        }

        [WebMethod()]
        public DataSet getBanks()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            SqlCommand cm;
            DataSet ds_bank = new DataSet();
            SqlDataAdapter da;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cm = new SqlCommand();
                cm.Connection = cn;

                string sqlString = "";
                sqlString = "Select * From Bank WHERE Active='True'";


                cm.CommandText = sqlString;

                da = new SqlDataAdapter(cm);
                da.Fill(ds_bank);

            }
            catch (SqlException) { }

            catch (Exception) { }

            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return ds_bank;
        }

        [WebMethod()]
        public DataSet getCurrencies()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            SqlCommand cm;
            DataSet ds_currency = new DataSet();
            SqlDataAdapter da;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cm = new SqlCommand();
                cm.Connection = cn;

                string sqlString = "";
                sqlString = "Select CurrencyID AS CurrencyID, ISNULL(Name,'') + ' - ' + ISNULL(Abbreviation,'') AS Name From Currency";

                cm.CommandText = sqlString;

                da = new SqlDataAdapter(cm);
                da.Fill(ds_currency);

            }
            catch (SqlException) { }

            catch (Exception) { }

            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return ds_currency;
        }

        [WebMethod()]
        public DataSet getTransactions()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            SqlCommand cm;
            DataSet ds_transactions = new DataSet();
            SqlDataAdapter da;

            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cm = new SqlCommand();
                cm.Connection = cn;

                string sqlString = "";
                sqlString = "Select * From PaymentTransaction";


                cm.CommandText = sqlString;

                da = new SqlDataAdapter(cm);
                da.Fill(ds_transactions);

            }
            catch (SqlException) { }

            catch (Exception) { }

            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            return ds_transactions;
        }

        //public class DataTypes
        //{
        //    public class viewPaymentTransactionDataType
        //    {
        //        public object merchantID;
        //        public object bankID;
        //        public object currencyID;
        //        public object cardType;
        //        public object cardNumber;
        //        public object nameOnCard;
        //        public object expiryMonth;
        //        public object expiryYear;
        //        public object CVV;
        //        public object amount;
        //    }
        //}



    }

}
