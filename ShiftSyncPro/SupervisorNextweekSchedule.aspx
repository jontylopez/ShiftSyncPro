<%@ Page Title="" Language="C#" MasterPageFile="~/SupervisorMaster.Master" AutoEventWireup="true" CodeBehind="SupervisorNextweekSchedule.aspx.cs" Inherits="ShiftSyncPro.WebForm6" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <div class="common-form-container">
     <asp:Label ID="lblMessage" runat="server" CssClass="common-input-label" ForeColor="Red"></asp:Label>
     <h2 class="login-title">Next Week's Schedule</h2>

     <!-- Schedule Details -->
     <div class="common-form-group">
         <asp:Label ID="Label1" runat="server" Text="Schedule ID: " CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblScheduleId" runat="server" Text="" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblUserId" runat="server" Text="User ID: " CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblGroupId" runat="server" Text="Group ID: " CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblDayCreate" runat="server" Text="Day Created: " CssClass="common-input-label"></asp:Label>
     </div>

     <!-- Monday -->
     <div class="common-form-group">
         <asp:Label ID="lblMonday" runat="server" Text="Monday:" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblMondayShift" runat="server" CssClass="common-input-label"></asp:Label>
         <asp:DropDownList ID="ddlMondayChangeShift" runat="server" CssClass="common-select-box"></asp:DropDownList>
     </div>

     <!-- Tuesday -->
     <div class="common-form-group">
         <asp:Label ID="lblTuesday" runat="server" Text="Tuesday:" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblTuesdayShift" runat="server" CssClass="common-input-label"></asp:Label>
         <asp:DropDownList ID="ddlTuesdayChangeShift" runat="server" CssClass="common-select-box"></asp:DropDownList>
     </div>

     <!-- Wednesday -->
     <div class="common-form-group">
         <asp:Label ID="lblWednesday" runat="server" Text="Wednesday:" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblWednesdayShift" runat="server" CssClass="common-input-label"></asp:Label>
         <asp:DropDownList ID="ddlWednesdayChangeShift" runat="server" CssClass="common-select-box"></asp:DropDownList>
     </div>

     <!-- Thursday -->
     <div class="common-form-group">
         <asp:Label ID="lblThursday" runat="server" Text="Thursday:" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblThursdayShift" runat="server" CssClass="common-input-label"></asp:Label>
         <asp:DropDownList ID="ddlThursdayChangeShift" runat="server" CssClass="common-select-box"></asp:DropDownList>
     </div>

     <!-- Friday -->
     <div class="common-form-group">
         <asp:Label ID="lblFriday" runat="server" Text="Friday:" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblFridayShift" runat="server" CssClass="common-input-label"></asp:Label>
         <asp:DropDownList ID="ddlFridayChangeShift" runat="server" CssClass="common-select-box"></asp:DropDownList>
     </div>

     <!-- Saturday -->
     <div class="common-form-group">
         <asp:Label ID="lblSaturday" runat="server" Text="Saturday:" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblSaturdayShift" runat="server" CssClass="common-input-label"></asp:Label>
         <asp:DropDownList ID="ddlSaturdayChangeShift" runat="server" CssClass="common-select-box"></asp:DropDownList>
     </div>

     <!-- Sunday -->
     <div class="common-form-group">
         <asp:Label ID="lblSunday" runat="server" Text="Sunday:" CssClass="common-input-label"></asp:Label>
         <asp:Label ID="lblSundayShift" runat="server" CssClass="common-input-label"></asp:Label>
         <asp:DropDownList ID="ddlSundayChangeShift" runat="server" CssClass="common-select-box"></asp:DropDownList>
     </div>

     <!-- Comment Box -->
     <div class="common-form-group">
         <asp:Label ID="lblComment" runat="server" Text="Comment:" CssClass="common-input-label"></asp:Label>
         <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="common-input-box"></asp:TextBox>
     </div>

     <!-- File Upload -->
     <div class="common-form-group">
         <asp:Label ID="lblFile" runat="server" Text="Attach Proof (Image or PDF):" CssClass="common-input-label"></asp:Label>
         <asp:FileUpload ID="fileUploadProof" runat="server" CssClass="common-input-box" />
     </div>

     <!-- Submit Request Button -->
     <div class="common-form-group">
         <asp:Button ID="btnSendRequest" runat="server" Text="Send Request" CssClass="common-button" OnClick="btnSendRequest_Click" />
     </div>
 </div>
</asp:Content>
