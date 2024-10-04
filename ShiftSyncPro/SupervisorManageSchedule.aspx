<%@ Page Title="" Language="C#" MasterPageFile="~/SupervisorMaster.Master" AutoEventWireup="true" CodeBehind="SupervisorManageSchedule.aspx.cs" Inherits="ShiftSyncPro.WebForm7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <!-- Add the ScriptManager here -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="common-form-container">
        <h2 class="login-title">View Schedule by Start Date</h2>

        <!-- Date Picker for Start Date -->
        <div class="common-form-group">
            <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="common-input-label" ForeColor="Red" Visible="false"></asp:Label>
            <asp:Label ID="lblStartDate" runat="server" Text="Select Start Date:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="common-input-box" AutoPostBack="false"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd" />
        </div>

        <!-- Button to Search Schedule by Start Date -->
        <div class="common-form-group">
            <asp:Button ID="btnViewSchedule" runat="server" Text="View Schedule" CssClass="common-button-green" OnClick="btnViewSchedule_Click" />
        </div>
    </div>

    <!-- GridView to Display Schedule -->
    <div class="common-form-group">
        <asp:GridView ID="gvSchedule" runat="server" CssClass="common-table" AutoGenerateColumns="False" EmptyDataText="No schedules found for the selected date.">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="userId" HeaderText="User ID" />
                <asp:BoundField DataField="groupId" HeaderText="Group" />
                <asp:BoundField DataField="weekStartDate" HeaderText="Week Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day1Date" HeaderText="Monday" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day1ShiftId" HeaderText="Monday Shift" />
                <asp:BoundField DataField="day2Date" HeaderText="Tuesday" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day2ShiftId" HeaderText="Tuesday Shift" />
                <asp:BoundField DataField="day3Date" HeaderText="Wednesday" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day3ShiftId" HeaderText="Wednesday Shift" />
                <asp:BoundField DataField="day4Date" HeaderText="Thursday" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day4ShiftId" HeaderText="Thursday Shift" />
                <asp:BoundField DataField="day5Date" HeaderText="Friday" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day5ShiftId" HeaderText="Friday Shift" />
                <asp:BoundField DataField="day6Date" HeaderText="Saturday" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day6ShiftId" HeaderText="Saturday Shift" />
                <asp:BoundField DataField="day7Date" HeaderText="Sunday" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="day7ShiftId" HeaderText="Sunday Shift" />
                <asp:BoundField DataField="dayCreate" HeaderText="Created On" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="timeCreate" HeaderText="Time Created" DataFormatString="{0:HH:mm:ss}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
