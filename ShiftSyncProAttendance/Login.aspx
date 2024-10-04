<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShiftSyncProAttendance.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - ShiftSyncPro</title>
    <link rel="stylesheet" href="~/Css/Style.css" />
</head>
<body class="login-body">
    <form id="form1" runat="server">
        <div class="login-container">
            <!-- Header Section with Logo and Title -->
            <div class="login-header">
                <div class="login-logo-container">
                    <img src="Images/logo.png" alt="Logo" class="login-logo" /> 
                    <h1 class="login-title">ShiftSyncPro Login</h1>
                </div>
            </div>
            
            <!-- Login Form Section -->
            <div class="login-form-group">
                <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="login-input-label"></asp:Label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="login-input-box"></asp:TextBox>
            </div>
            <div class="login-form-group">
                <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="login-input-label"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="login-input-box" TextMode="Password"></asp:TextBox>
            </div>
            
            <!-- Date Picker Section -->
            <div class="login-form-group">
                <asp:Label ID="lblDate" runat="server" Text="Select Date" CssClass="login-input-label"></asp:Label>
                <asp:TextBox ID="txtDate" runat="server" CssClass="login-input-box" TextMode="Date"></asp:TextBox>
            </div>

            <!-- Time Picker Section -->
            <div class="login-form-group">
                <asp:Label ID="lblTime" runat="server" Text="Select Time" CssClass="login-input-label"></asp:Label>
                <asp:TextBox ID="txtTime" runat="server" CssClass="login-input-box" TextMode="Time"></asp:TextBox>
            </div>

            <!-- Error Message Placeholder -->
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="login-error-message" Visible="False"></asp:Label>
            
            <!-- Login Button -->
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="login-button" OnClick="btnLogin_Click" />
            
            <!-- Footer Section for Additional Links -->
            <div class="login-footer">
                <p>Forgot your password? <a href="ForgotPassword.aspx">Click here</a></p>
            </div>
        </div>
    </form>
</body>
</html>