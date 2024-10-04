<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminUpdateUserForm.aspx.cs" Inherits="ShiftSyncPro.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="common-form-container">
        <h2 class="login-title">Update User Details</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="common-input-label" ForeColor="Red"></asp:Label>

        <!-- Non-editable field for User ID -->
        <div class="common-form-group">
            <asp:Label ID="lblUserID" runat="server" Text="User ID:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server" CssClass="common-input-box" ReadOnly="True"></asp:TextBox>
        </div>

        <!-- Read-only fields for user information -->
        <div class="common-form-group">
            <asp:Label ID="lblEmpID" runat="server" Text="Employee ID:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtEmpId" runat="server" CssClass="common-input-box" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblDepID" runat="server" Text="Department ID:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtDepId" runat="server" CssClass="common-input-box" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblPosition" runat="server" Text="Position:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtPosition" runat="server" CssClass="common-input-box" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="common-input-box" ReadOnly="True"></asp:TextBox>
        </div>

        <!-- Editable fields -->
        <div class="common-form-group">
            <asp:Label ID="lblFullName" runat="server" Text="Full Name:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblTel" runat="server" Text="Telephone:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtTel" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtMail" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>

        <!-- Editable dropdowns for Group and Role -->
        <div class="common-form-group">
            <asp:Label ID="lblGroup" runat="server" Text="Group:" CssClass="common-input-label"></asp:Label>
            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="common-select-box">
                <asp:ListItem>NULL</asp:ListItem>
                <asp:ListItem>A</asp:ListItem>
                <asp:ListItem>B</asp:ListItem>
                <asp:ListItem>C</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblRole" runat="server" Text="Select Role:" CssClass="common-input-label"></asp:Label>
            <asp:DropDownList ID="ddlRole" runat="server" CssClass="common-select-box">
                <asp:ListItem Text="Admin" Value="adm"></asp:ListItem>
                <asp:ListItem Text="HR Assistant" Value="hrm"></asp:ListItem>
                <asp:ListItem Text="Supervisor" Value="sup"></asp:ListItem>
                <asp:ListItem Text="Worker" Value="wkr"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Buttons -->
        <div class="common-form-group">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="common-button-green" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="common-button" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>