<%@ Page Title="" Language="C#" MasterPageFile="~/App_Main.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="BrakeWizards_WebApp.payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="level">
    <div class="level-item has-text-centered">
        <div>
            <p class="heading">Invoice Date</p>
            <div class="dropdown is-large">
                <asp:DropDownList ID="invoicePaymentDropDown" runat="server" Font-Size="X-Large" CssClass="dropdown"></asp:DropDownList>
            </div>
            <asp:Button ID="selectInvoiceButton" runat="server" Text="Select" CssClass="button is-link" OnClick="selectInvoiceButton_Click" />
        </div>
    </div>
</div>
<div class="spacer"><!---spacer---></div>
<div class="columns">
    <div class="column is-1"></div>
    <div class="column">
        <div class="card override-card-height-payment">
            <div class="card-header">
                <div class="card-header-title">
                    Invoice Details
                </div>           
            </div>
            <div class="card-content">
                <div class="menu">
                    <p class="menu-label">Summary</p>
                    <ul class="menu-list">
                        <li>Customer: <asp:Label ID="customerLabel" runat="server" Text="" CssClass="tag is-white has-text-weight-bold"></asp:Label></li>
                        <li>Due Date: <asp:Label ID="dueDateLabel" runat="server" Text="" CssClass="tag is-white has-text-weight-bold"></asp:Label></li>
                        <li>Total Due: <asp:Label ID="totalLabel" runat="server" Text="" CssClass="tag is-white has-text-weight-bold"></asp:Label></li>
                    </ul>
                    <p class="menu-label">Invoice Items</p>
                    <ul class="menu-list">
                        <li><asp:Label ID="invoiceItemsLabel" runat="server" Text="" CssClass="is-size-7 has-text-weight-bold"></asp:Label></li>
                    </ul>
                    <p class="menu-label">Repair Package</p>
                    <ul class="menu-list">
                        <li>Package Name: <asp:Label ID="packageNameLabel" runat="server" Text="" CssClass="tag is-white has-text-weight-bold"></asp:Label></li>
                        <li>Package Items:
                            <ul>
                                <li>
                                    <asp:Label ID="itemDescAndCostList" runat="server" Text="" CssClass="is-size-7 has-text-weight-bold"></asp:Label>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="column">
        <div class="card override-card-height-payment">
            <div class="card-header">
                <div class="card-header-title">
                    Payment
                </div>           
            </div>
            <div class="card-content has-text-centered" id="checkoutCard">
                <a id="checkoutCardButton" class="button is-link is-large" target="_blank" href="https://checkout.stripe.com/pay/ppage_1GT6fPFKnpzPB0MXpfLOhRVJ#fidkdWxOYHwnPyd1blpxYHZxWm9CPHY2XUhhVm9fQzxOYWgwYjA8f2lcYScpJ3dgY2B3d2B3SndsYmxrJz8nbXFxdXY%2FKip2cXdsdWArZmpoJyknaWpmZGlgJz9rcGlpKSdobGF2Jz9%2BJ2JwbGEnPyc9PGQzZjJnMihgMDIzKDExYDAoZDE1NygyM2A1NzQxMjUxMWQnKSdocGxhJz8nNzE0MzNhMGYoPT02ZigxZz02KGdgNGQoMT09ZDZkYTNkYGQwJykndmxhJz8nMTFkNTYwZjQoPTQ3NigxZmY0KDwyM2QoMWA1ZmM3N2RhNzc8J3gpJ2dgcWR2Jz9eWHgl">
                    Pay Now</a>
            </div>
            <div class="card-footer">
                <div class="card-footer-item">
                    <img src="images/powered_by_stripe.png" />
                </div>
            </div>
        </div>
    </div>
    <div class="column is-1"></div>
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
                <li><a class="is-active" href="payment.aspx">Make a Payment</a></li>
                <li><a href="check_progress.aspx">Check Incident Progress</a></li>
                <li><a href="request_service.aspx">Request Service</a></li>
            </ul>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MobileTitle" runat="server">
    <p class="subtitle">Make A Payment</p>
</asp:Content>
