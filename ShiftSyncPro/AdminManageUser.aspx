<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminManageUser.aspx.cs" Inherits="ShiftSyncPro.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
      <h2 class="login-title">Manage Users</h2>
  <asp:Label ID="lblMessage" runat="server" CssClass="common-input-label" ForeColor="Red"></asp:Label>

  <asp:GridView ID="gvUsers" runat="server" CssClass="common-table" AutoGenerateColumns="False" DataKeyNames="id"
                OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit"
                OnRowCommand="gvUsers_RowCommand">
      <Columns>
          <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" />
          <asp:BoundField DataField="empId" HeaderText="Employee ID" ReadOnly="True" />
          <asp:BoundField DataField="depId" HeaderText="Department ID" ReadOnly="True" />
          <asp:BoundField DataField="position" HeaderText="Position" ReadOnly="True" />
          <asp:BoundField DataField="fullName" HeaderText="Full Name"  ReadOnly="True" />
          <asp:BoundField DataField="empTel" HeaderText="Telephone"  ReadOnly="True" />
          <asp:BoundField DataField="uRole" HeaderText="Role"   ReadOnly="True"/>
          
          <asp:TemplateField HeaderText="Actions">
              <ItemTemplate>
                  <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="TempAction" CommandArgument='<%# Eval("id") %>' CssClass="common-button" />
              </ItemTemplate>
          </asp:TemplateField>
      </Columns>
  </asp:GridView>

</asp:Content>
