<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="PaymentGateway.aspx.cs" Inherits="PaymentGateway.Test" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Checkout Payment Gateway</h1>
    </div>

    <div id="alertSuccess" visible="false" runat="server" class="alert alert-success" role="alert">
    </div>

    <div id="alertDanger" visible="false" runat="server" class="alert alert-danger" role="alert">
    </div>

    <div class="card mb-3">
        <div class="card-header">Process Payment</div>
        <div class="card-body">

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Card Type</label>
                <asp:DropDownList ID="DrpCardType" runat="server" CssClass="form-control col-md-6">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Visa</asp:ListItem>
                    <asp:ListItem>Mastercard</asp:ListItem>
                    <asp:ListItem>Maestro</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="valDrpCardType" runat="server" ControlToValidate="DrpCardType" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Name On Card</label>
                <asp:TextBox ID="TxtNameOnCard" runat="server" CssClass="form-control col-md-6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valTxtNameOnCard" runat="server" ControlToValidate="TxtNameOnCard" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Card Number</label>
                <asp:TextBox ID="TxtCardNumber" runat="server" CssClass="form-control col-md-6" max-width="inherit !important;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valTxtCardNumber" runat="server" ControlToValidate="TxtCardNumber" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Expiry Month</label>
                <asp:DropDownList ID="DrpExpiryMonth" runat="server" CssClass="form-control col-md-6">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="valDrpExpiryMonth" runat="server" ControlToValidate="DrpExpiryMonth" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Expiry Year</label>
                <asp:TextBox ID="TxtExpiryYear" runat="server" CssClass="form-control col-md-6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valTxtExpiryYear" runat="server" ControlToValidate="TxtExpiryYear" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">CVV</label>
                <asp:TextBox ID="TxtCVV" runat="server" CssClass="form-control col-md-6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valTxtCVV" runat="server" ControlToValidate="TxtCVV" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Currency</label>
                <asp:DropDownList ID="DrpCurrency" runat="server" CssClass="form-control col-md-6" DataValueField="CurrencyID" DataTextField="Name"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="valDrpCurrency" runat="server" ControlToValidate="DrpCurrency" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Amount</label>
                <asp:TextBox ID="TxtAmount" runat="server" CssClass="form-control col-md-6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="valTxtAmount" runat="server" ControlToValidate="TxtAmount" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Merchant</label>
                <asp:DropDownList ID="DrpMerchant" runat="server" CssClass="form-control col-md-6" DataValueField="MerchantID" DataTextField="Name"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="valDrpMerchant" runat="server" ControlToValidate="DrpMerchant" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div class="form-group row">
                <label class="col-md-2 form-control-label">Bank</label>
                <asp:DropDownList ID="DrpBank" runat="server" CssClass="form-control col-md-6" DataValueField="BankID" DataTextField="Name"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="valDrpBank" runat="server" ControlToValidate="DrpBank" ForeColor="Red" ValidationGroup="validatePayment" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>

            <div>
                <asp:Button ID="BtnSubmitPayment" runat="server" Text="Submit Payment" OnClick="BtnSubmitPayment_Click" class="btn btn-primary" ValidationGroup="validatePayment" />
            </div>

        </div>
    </div>

    <div class="card mb-3" id="CardSearchPaymentByTransactionID" runat="server">
        <div class="card-header">Search Payment By Transaction ID</div>
        <div class="card-body">
            <div class="form-group row">
                <div class="col-md-5">
                    <asp:DropDownList ID="DrpSearchByTransactionID" runat="server" CssClass="form-control" DataValueField="TransactionID" DataTextField="TransactionID"></asp:DropDownList>
                </div>
                <asp:Button ID="BtnSearchByTransactionID" class="btn btn-primary col-md-1" runat="server" Text="Search" OnClick="BtnSearchByTransactionID_Click" ValidationGroup="validateDrpSearchByTransactionID" />
                <asp:RequiredFieldValidator ID="valDrpSearchByTransactionID" runat="server" ControlToValidate="DrpSearchByTransactionID" ForeColor="Red" ValidationGroup="validateDrpSearchByTransactionID" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>
            <ul class="list-group">
                <asp:DataList ID="DataListTransactionDetails" runat="server">
                    <ItemTemplate>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Merchant:</b> <%# Eval("Merchant").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Bank:</b> <%# Eval("Bank").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Card Type:</b> <%# Eval("CardType").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Card Number:</b> <%# Eval("cardNumber").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Name On Card:</b> <%# Eval("NameOnCard").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Expiry Month:</b> <%# Eval("ExpiryMonth").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Expiry Year:</b> <%# Eval("ExpiryYear").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>CVV:</b> <%# Eval("CVV").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Amount:</b> <%# Eval("Amount").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Bank Response ID:</b> <%# Eval("BankResponseID").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Bank Response:</b> <%# Eval("BankResponse").ToString()%></li>
                        <li class="list-group-item blue-grey-500" style="border: none;"><b>Status:</b> <%# Eval("Status").ToString()%></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblEmpty" Text="No records to show" runat="server" Visible='<%#bool.Parse((DataListTransactionDetails.Items.Count==0).ToString())%>'></asp:Label>
                    </FooterTemplate>
                </asp:DataList>
            </ul>
        </div>
    </div>

    <div class="card mb-3" id="CardSearchPaymentByMerchant" runat="server">
        <div class="card-header">Search Payment By Merchant</div>
        <div class="card-body">
            <div class="form-group row">
                <div class="col-md-5">
                    <asp:DropDownList ID="DrpSearchByMerchant" runat="server" CssClass="form-control" DataValueField="MerchantID" DataTextField="Name"></asp:DropDownList>
                </div>
                <asp:LinkButton ID="BtnSearchByMerchant" class="btn btn-primary col-md-1" runat="server" Text="Search" OnClick="BtnSearchByMerchant_Click" ValidationGroup="validateDrpSearchByMerchant"></asp:LinkButton>
                <asp:RequiredFieldValidator ID="valDrpSearchByMerchant" runat="server" ControlToValidate="DrpSearchByMerchant" ForeColor="Red" ValidationGroup="validateDrpSearchByMerchant" CssClass="col-md-2">* Required</asp:RequiredFieldValidator>
            </div>
            <asp:GridView ID="GridViewTransactions" runat="server" GridLines="None" CssClass="table table-hover table-responsive" DataKeyNames="transactionID" AutoGenerateColumns="false">
                <EmptyDataTemplate>No records to show</EmptyDataTemplate>
                <Columns>

                    <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" SortExpression="TransactionID"></asp:BoundField>
                    <asp:BoundField DataField="Bank" HeaderText="Bank" SortExpression="Bank"></asp:BoundField>
                    <asp:BoundField DataField="CardType" HeaderText="Card Type" SortExpression="CardType"></asp:BoundField>
                    <asp:BoundField DataField="CardNumber" HeaderText="Card Number" SortExpression="CardNumber"></asp:BoundField>
                    <asp:BoundField DataField="NameOnCard" HeaderText="Name On Card" SortExpression="NameOnCard"></asp:BoundField>
                    <asp:BoundField DataField="ExpiryMonth" HeaderText="Expiry Month" SortExpression="ExpiryMonth"></asp:BoundField>
                    <asp:BoundField DataField="ExpiryYear" HeaderText="Expiry Year" SortExpression="ExpiryYear"></asp:BoundField>
                    <asp:BoundField DataField="CVV" HeaderText="CVV" SortExpression="CVV"></asp:BoundField>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount"></asp:BoundField>
                    <asp:BoundField DataField="BankResponseID" HeaderText="Bank Response ID" SortExpression="BankResponseID"></asp:BoundField>
                    <asp:BoundField DataField="BankResponse" HeaderText="Bank Response" SortExpression="BankResponse"></asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>

