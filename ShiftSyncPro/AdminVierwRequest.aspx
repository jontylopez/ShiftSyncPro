<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminVierwRequest.aspx.cs" Inherits="ShiftSyncPro.WebForm15" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <h2 class="common-title">Pending Requests</h2>

 <asp:GridView ID="gvPendingRequests" runat="server" AutoGenerateColumns="False" CssClass="common-table" OnRowCommand="gvPendingRequests_RowCommand">
     <Columns>

         <asp:BoundField DataField="id" HeaderText="Request ID" />
         <asp:BoundField DataField="userId" HeaderText="User ID" />
         <asp:BoundField DataField="scheduleId" HeaderText="Schedule ID" />
         <asp:BoundField DataField="requestDate" HeaderText="Request Date" DataFormatString="{0:yyyy-MM-dd}" />

         <asp:ButtonField ButtonType="Button" CommandName="ViewRequest" Text="View Request" />
     </Columns>
 </asp:GridView>
</asp:Content>
