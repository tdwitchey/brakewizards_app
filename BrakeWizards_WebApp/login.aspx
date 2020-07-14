<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BrakeWizards_WebApp.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="css/bulma.css" rel="stylesheet" />
    <link href="css/override.css" rel="stylesheet" />
</head>
<body>
    <div class="columns">
        <div class="column"></div>
        <div class="column">
            <div class="card">
                <div class="card-image">
                    <div class="level">
                        <div class="level-item">
                            <figure class="image is-128x128">
                                <img src="images/logo.png" alt="Brake Wizards Logo" />
                            </figure>
                        </div>
                    </div>
                    <div class="has-text-centered">
                        <h1 class="title">Incident Tracker&trade;</h1>
                        <p class="subtitle">Web Application</p>
                    </div>
                </div>
                <div class="card-content">
                    <form id="loginForm" runat="server">
                        <div class="field">
                            <label class="label">Username</label>
                            <div class="control">
                                <asp:TextBox ID="userText" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                        </div>
                        <div class="field">
                            <label class="label">Password</label>
                            <div class="control">
                                <asp:TextBox ID="passText" runat="server" CssClass="input" ReadOnly="False" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="level">
                            <div class="level-item">
                                <div class="field">
                                    <div class="control">
                                        <asp:Button ID="submitLoginButton" runat="server" Text="Submit" CssClass="button is-link is-medium" OnClick="submitLoginButton_Click"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <div class="column"></div>
    </div>
    <div class="level">
        <div class="level-item">
            <a href="brakewizards.com">Back to Brakewizards.com</a>
        </div>
    </div>
</body>
</html>
