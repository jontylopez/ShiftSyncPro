<%@ Page Title="" Language="C#" MasterPageFile="~/SupervisorMaster.Master" AutoEventWireup="true" CodeBehind="SupervisorRequestViewForm.aspx.cs" Inherits="ShiftSyncPro.WebForm12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="common-form-container">
        <h2 class="common-title">Request Details</h2>
        <asp:Label ID="lblMessage" runat="server" CssClass="common-input-label" Text=""></asp:Label>

        <!-- Request ID -->
        <div class="common-form-group">
            <asp:Label ID="lblRequestId" runat="server" Text="Request ID:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblRequestIdValue" runat="server" CssClass="common-input-label"></asp:Label>
        </div>

        <!-- User Name and Schedule ID -->
        <div class="common-form-group">
            <asp:Label ID="lblUserId" runat="server" Text="User Name:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblUserName" runat="server" CssClass="common-input-label"></asp:Label>
        </div>

        <div class="common-form-group">
            <asp:Label ID="lblScheduleId" runat="server" Text="Schedule ID:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblScheduleIdValue" runat="server" CssClass="common-input-label"></asp:Label>
        </div>

        <!-- Shift Details -->
        <div class="common-form-group">
            <h3>Weekly Shift Details</h3>
            <asp:Label ID="lblMonday" runat="server" Text="Monday:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblMondayShift" runat="server" CssClass="common-input-label"></asp:Label>

            <asp:Label ID="lblTuesday" runat="server" Text="Tuesday:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblTuesdayShift" runat="server" CssClass="common-input-label"></asp:Label>

            <asp:Label ID="lblWednesday" runat="server" Text="Wednesday:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblWednesdayShift" runat="server" CssClass="common-input-label"></asp:Label>

            <asp:Label ID="lblThursday" runat="server" Text="Thursday:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblThursdayShift" runat="server" CssClass="common-input-label"></asp:Label>

            <asp:Label ID="lblFriday" runat="server" Text="Friday:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblFridayShift" runat="server" CssClass="common-input-label"></asp:Label>

            <asp:Label ID="lblSaturday" runat="server" Text="Saturday:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblSaturdayShift" runat="server" CssClass="common-input-label"></asp:Label>

            <asp:Label ID="lblSunday" runat="server" Text="Sunday:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblSundayShift" runat="server" CssClass="common-input-label"></asp:Label>
        </div>
        <div class="common-form-group">
    <asp:Button ID="btnViewProofDocument" runat="server" Text="View Proof Document" CssClass="common-button" OnClick="btnViewProofDocument_Click" />
</div>
        <!-- Comment Section -->
        <div class="common-form-group">
            <asp:Label ID="lblComment" runat="server" Text="Worker Comment:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="common-input-box" Rows="4" ReadOnly="True"></asp:TextBox>
        </div>

        <!-- Supervisor Comment Section -->
        <div class="common-form-group">
            <asp:Label ID="lblSupervisorComment" runat="server" Text="Supervisor Comment:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtSupervisorComment" runat="server" CssClass="common-input-box" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>

        <!-- Approve/Reject Buttons -->
        <div class="common-form-group">
            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="common-button-green" OnClick="btnApprove_Click" />
            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="common-button-red" OnClick="btnReject_Click" />
        </div>
    </div>
</asp:Content>
