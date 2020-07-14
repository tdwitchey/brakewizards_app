<%@ Page Title="" Language="C#" MasterPageFile="~/App_Main.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="BrakeWizards_WebApp.dashboard" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="level">
    <div class="level-item has-text-centered">
        <div>
            <p class="heading">Requests in progress</p>
            <p class="title"><asp:Label ID="requestsInProgressLabel" runat="server" Text="0"></asp:Label></p>
        </div>
    </div>
    <div class="level-item has-text-centered">
        <div>
            <p class="heading">Current charges</p>
            <p class="title">$<asp:Label ID="balanceLabel" runat="server" Text=""></asp:Label></p>
        </div>
    </div>            
    <div class="level-item has-text-centered">
        <div>
            <p class="heading">Next payment due</p>
            <p class="title"><asp:Label ID="nextDueDateLabel" runat="server" Text="N/A"></asp:Label></p>
        </div>
    </div>
</div>
<div class="spacer"><!---spacer---></div>
<div class="columns">
    <div class="column is-1"></div>
    <div class="column">
        <div class="card">
            <div class="card-header">
                <div class="card-header-title">
                    History of Brake Wizards
                </div>           
            </div>
            <div class="card-content">
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ultrices condimentum lacus, non bibendum arcu porttitor nec. Sed rhoncus dui eu lectus ullamcorper, quis efficitur arcu facilisis. Cras maximus eu ligula vel euismod. Nunc sed orci id purus fringilla luctus id quis felis. Pellentesque ullamcorper at erat sed facilisis. Praesent turpis nulla, pulvinar et maximus sodales, mattis nec dolor. Vivamus vel blandit quam, in pellentesque lacus. Vivamus porta egestas arcu a gravida. Sed facilisis, leo in dignissim rutrum, sem orci blandit diam, id accumsan quam augue sit amet augue. Aenean pellentesque eget nisl vel consequat. Vestibulum nunc elit, finibus vel metus quis, ultrices facilisis nunc. Fusce congue sit amet metus a commodo. Suspendisse eu tempor neque. </p>
                <div class="spacer"><!---spacer---></div>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ultrices condimentum lacus, non bibendum arcu porttitor nec. Sed rhoncus dui eu lectus ullamcorper, quis efficitur arcu facilisis. Cras maximus eu ligula vel euismod. Nunc sed orci id purus fringilla luctus id quis felis. Pellentesque ullamcorper at erat sed facilisis. Praesent turpis nulla, pulvinar et maximus sodales, mattis nec dolor. Vivamus vel blandit quam, in pellentesque lacus. Vivamus porta egestas arcu a gravida. Sed facilisis, leo in dignissim rutrum, sem orci blandit diam, id accumsan quam augue sit amet augue. Aenean pellentesque eget nisl vel consequat. Vestibulum nunc elit, finibus vel metus quis, ultrices facilisis nunc. Fusce congue sit amet metus a commodo. Suspendisse eu tempor neque. </p>
                <div class="spacer"><!---spacer---></div>
                <div class="level">
                    <div class="level-item has-text-centered">
                        <div>
                            <p class="heading">First Shop Opened</p>
                            <p class="title">1989</p>
                        </div>
                    </div>
                    <div class="level-item has-text-centered">
                        <div>
                            <p class="heading">Lost First Shop to Fire</p>
                            <p class="title">2002</p>
                        </div>
                    </div>
                    <div class="level-item has-text-centered">
                        <div>
                            <p class="heading">Relocation to Minnesota</p>
                            <p class="title">2007</p>
                        </div>
                    </div>    
                    <div class="level-item has-text-centered">
                        <div>
                            <p class="heading">First Employees Hired</p>
                            <p class="title">2009</p>
                        </div>
                    </div>    
                    <div class="level-item has-text-centered">
                        <div>
                            <p class="heading">Second Location Planned</p>
                            <p class="title">2019</p>
                        </div>
                    </div>    
                </div>
            </div>
        </div>
    </div>
    <div class="column is-1"></div>
</div>
</asp:Content>

<asp:Content ID="SideMenu" ContentPlaceHolderID="SideMenuContent" runat="server">
    <div class="menu">
        <div class="spacer"></div>
            <p class="menu-label">General</p>
            <ul class="menu-list">
                <li><a class="is-active" href="dashboard.aspx">Home</a></li>
                <li><a href="account_tab_personal.aspx">My Account</a></li>
            </ul>
            <p class="menu-label">Services</p>
            <ul class="menu-list">
                <li><a href="payment.aspx">Make a Payment</a></li>
                <li><a href="check_progress.aspx">Check Incident Progress</a></li>
                <li><a href="request_service.aspx">Request Service</a></li>
            </ul>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MobileTitle" runat="server">
    <p class="subtitle">Home</p>
</asp:Content>


