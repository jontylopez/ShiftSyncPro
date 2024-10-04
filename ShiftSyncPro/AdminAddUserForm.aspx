<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminAddUserForm.aspx.cs" Inherits="ShiftSyncPro.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="common-form-container">
        <asp:Label ID="lblMessage" runat="server" CssClass="common-input-label" ForeColor="Red"></asp:Label>
        <h2 class="login-title">Add New User</h2>
        <div class="common-form-group">
            <asp:Label ID="lblSelectEmp" runat="server" Text="Select Employee:" CssClass="common-input-label"></asp:Label>
            <asp:DropDownList ID="ddlEmployees" runat="server" CssClass="common-select-box" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblEmpID" runat="server" Text="Employee ID:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtEmpId" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblDepID" runat="server" Text="Department ID:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtDepId" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblPosition" runat="server" Text="Position:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtPossition" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblName" runat="server" Text="Name:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblTel" runat="server" Text="Telephone:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtTel" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtMail" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblGroup" runat="server" Text="Group:" CssClass="common-input-label"></asp:Label>
            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="common-select-box">
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
        <div class="common-form-group">
            <asp:Label ID="lblUName" runat="server" Text="User Name:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Label ID="lblPWord" runat="server" Text="Password:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>
        <div class="common-form-group">
            <asp:Button ID="btnAddUser" runat="server" Text="Add User" CssClass="common-button" OnClick="btnAddUser_Click" />
        </div>
    </div>
</asp:Content>
