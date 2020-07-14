<%@ Page Title="" Language="C#" MasterPageFile="~/App_Main.Master" AutoEventWireup="true" CodeBehind="check_progress.aspx.cs" Inherits="BrakeWizards_WebApp.check_progress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--- Contact Button --->
    <asp:Button ID="contactButton" runat="server" Text="" Height="5em" BorderStyle="None" CssClass="button is-rounded override-contact-button" Width="5em" OnClick="contactButton_Click" BackColor="#3C2E59" Style="background-image:url('images/env_icon.png'); background-repeat:no-repeat" />

    <div class="level">
        <div class="level-item has-text-centered">
            <div>
                <p class="heading">Incident Request Date</p>
                <div class="dropdown">
                    <asp:DropDownList ID="incidentProgressDropdown" runat="server" Font-Size="X-Large" CssClass="dropdown"></asp:DropDownList>
                </div>
                <asp:Button ID="selectIncidentButton" runat="server" Text="Select" CssClass="button is-link" OnClick="selectIncidentButton_Click" />
            </div>
        </div>
        <div class="level-item has-text-centered">
            <div>
                <p class="heading">Date Started</p>
                <p class="title"><asp:Label ID="dateStartedLabel" runat="server" Text=""></asp:Label></p>
            </div>
        </div>
    </div>
    <div class="columns">
        <div class="column is-1"></div>
        <div class="column">
            <div class="card override-card-height-progress">
                <div class="card-header">
                    <p class="card-header-title">
                        Employees
                    </p>
                </div>
                <div class="card-content">
                    <div class="menu">
                        <p class="menu-label">Supervisor</p>
                        <ul class="menu-list">
                            <li><asp:Label ID="supervisorNameLabel" runat="server" Text=""></asp:Label></li>
                        </ul>
                        <p class="menu-label">Technicians</p>
                        <ul class="menu-list">
                            <li><asp:Label ID="technicianListLabel" runat="server" Text=""></asp:Label></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="card override-card-height-progress">
                <div class="card-header">
                    <p class="card-header-title">
                        Description
                    </p>
                </div>
                <div class="card-content">
                    <div class="menu">
                        <p class="menu-label">Incident Description</p>
                        <ul class="menu-list">
                            <li><asp:Label ID="incidentDescriptionLabel" runat="server" Text=""></asp:Label></li>
                        </ul>
                        <p class="menu-label">Repair Package</p>
                        <ul class="menu-list">
                            <li><asp:Label ID="packageNameLabel" runat="server" Text=""></asp:Label></li>
                        </ul>
                        <p class="menu-label">Package Cost</p>
                        <ul class="menu-list">
                            <li>$<asp:Label ID="packageCostLabel" runat="server" Text=""></asp:Label></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="column">
            <div class="panel is-dark override-card-height-progress">
                <div class="panel-heading">
                    Progress
                </div>
                <div class="panel-block">
                    <p><asp:Label ID="progress1Label" runat="server" Text="" CssClass="title is-4 has-text-primary"></asp:Label> days since request was first submitted</p>
                </div>
                <div class="panel-block">
                    <p><asp:Label ID="progress2Label" runat="server" Text="" CssClass="title is-4 has-text-primary"></asp:Label> days between date requested &amp; date started</p>
                </div>
                <div class="panel-block">
                    <p>Supervisor is managing <asp:Label ID="progress3Label" runat="server" Text="" CssClass="title is-4 has-text-primary"></asp:Label> other concurrent project(s)</p>
                </div>
                <div class="panel-block">
                    <p class="heading">Estimated Progress</p>
                    <progress class="progress is-dark" id="estimatedProgressBar" value="0" max="10" runat="server"></progress>
                </div>
            </div>
        </div>
        <div class="column is-1"></div>
    </div>
    <div class="is-hidden-desktop">
        <div class="spacer"></div>
        <div class="spacer"></div>
        <div class="spacer"></div>
        <div class="spacer"></div>
        <div class="spacer"></div>
    </div>

    <asp:Panel ID="contactPanel" runat="server" Visible="False" BackColor="#3C2E59" Height="12em" Width="15em" CssClass="override-contact-panel has-text-centered">
        <br />
        <p class="subtitle is-5 has-text-white">Contact Supervisor</p>
        <asp:LinkButton ID="btnSupervisor" runat="server" CssClass="button is-link is-medium" ></asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="closePanel" runat="server" Font-Size="Large" OnClick="closePanel_Click">Close</asp:LinkButton>
    </asp:Panel>
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
                <li><a class="is-active" href="check_progress.aspx">Check Incident Progress</a></li>
                <li><a href="request_service.aspx">Request Service</a></li>
            </ul>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MobileTitle" runat="server">
    <p class="subtitle">Check Incident Progress</p>
</asp:Content>
