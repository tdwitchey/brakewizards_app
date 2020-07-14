<%@ Page Title="" Language="C#" MasterPageFile="~/App_AccountTabs.Master" AutoEventWireup="true" CodeBehind="account_tab_password.aspx.cs" Inherits="BrakeWizards_WebApp.account_tab_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="tabs is-boxed">
        <ul>
            <li><a href="account_tab_personal.aspx">Personal</a></li>
            <li><a href="account_tab_account.aspx">Account</a></li>
            <li class="is-active"><a href="account_tab_password.aspx">Password</a></li>
        </ul>
    </div>
    <div class="field">
        <label class="label">Old Password</label>
        <div class="control">
            <asp:TextBox ID="txtOldPass" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>
    </div>
    <div class="field">
        <label class="label">New Password</label>
        <div class="control">
            <asp:TextBox ID="txtNewPass" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>
    </div>
    <div class="field">
        <label class="label">Re-Type New Password</label>
        <div class="control">
            <asp:TextBox ID="txtRetype" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>
    </div>
    <div class="field">
        <div class="control">
            <asp:Button ID="btnSubmit_Password" runat="server" Text="Submit" CssClass="button is-link" OnClick="btnSubmit_Password_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="SideMenu" ContentPlaceHolderID="SideMenuContent" runat="server">
    <div class="menu">
        <div class="spacer"></div>
            <p class="menu-label">General</p>
            <ul class="menu-list">
                <li><a href="dashboard.aspx">Home</a></li>
                <li><a class="is-active" href="account_tab_personal.aspx">My Account</a></li>
            </ul>
            <p class="menu-label">Services</p>
            <ul class="menu-list">
                <li><a href="payment.aspx">Make a Payment</a></li>
                <li><a href="check_progress.aspx">Check Incident Progress</a></li>
                <li><a href="request_service.aspx">Request Service</a></li>
            </ul>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MobileTitle" runat="server">
    <p class="subtitle">My Account</p>
</asp:Content>
