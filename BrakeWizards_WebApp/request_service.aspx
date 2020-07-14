<%@ Page Title="" Language="C#" MasterPageFile="~/App_Main.Master" AutoEventWireup="true" CodeBehind="request_service.aspx.cs" Inherits="BrakeWizards_WebApp.request_service" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="level">
        <div class="level-item">
            <figure class="image is-128x128">
                <img src="images/logo.png" alt="Brake Wizards Logo" />
            </figure>
        </div>
    </div>

    <div class="columns">
        <div class="column is-4"></div>
        <div class="column">
            <div class="panel is-dark">
                <div class="panel-heading">Request Service Form</div>
                <div class="override-padding-sides-narrow">
                    <div class="spacer"></div>
                    <label class="label">Customer Name</label>
                    <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                    <div class="spacer"></div>

                    <label class="label">Date</label>
                    <asp:Label ID="lblCurrentDate" runat="server" Text=""></asp:Label>
                    <div class="spacer"></div>

                    <label class="label">Service Type</label>
                        <asp:RadioButton ID="rbRepairType1" runat="server" Text=" Basic Repair" GroupName="ServiceTypeGroup" Checked="True" />
                        <br />
                        <asp:RadioButton ID="rbRepairType2" runat="server" Text=" Complete Repair" GroupName="ServiceTypeGroup" />
                    <div class="spacer"></div>

                    <label class="label">Car Make</label>
                    <asp:DropDownList ID="carMakeDropdown" runat="server" CssClass="select"></asp:DropDownList>
                    <div class="spacer"></div>

                    <div class="level">
                        <div class="level-item">
                            <asp:Button ID="btnSubmit_Request" runat="server" Text="Submit" CssClass="button is-link" OnClick="btnSubmit_Request_Click"/>
                        </div>
                    </div>
                    <div class="spacer"></div>

                </div>
            </div>
        </div>
        <div class="column is-4"></div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SideMenuContent" runat="server">
    <div class="menu">
        <div class="spacer"></div>
            <p class="menu-label">General</p>
            <ul class="menu-list">
                <li><a href="dashboard.aspx">Home</a></li>
                <li><a href="account_tab_personal.aspx">My Account</a></li>
            </ul>
            <p class="menu-label">Services</p>
            <ul class="menu-list">
                <li><a href="payment.aspx">Make a Payment</a></li>
                <li><a href="check_progress.aspx">Check Incident Progress</a></li>
                <li><a class="is-active" href="request_service.aspx">Request Service</a></li>
            </ul>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MobileTitle" runat="server">
    <p class="subtitle">Request Service</p>
</asp:Content>
