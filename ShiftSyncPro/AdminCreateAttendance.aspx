<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminCreateAttendance.aspx.cs" Inherits="ShiftSyncPro.WebForm18" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
        <!-- ScriptManager is required to use AJAX controls -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h2 class="common-title">Create Attendance</h2>

    <!-- Week Start Date Picker -->
    <div class="common-form-group">
        <asp:Label ID="lblWeekStartDate" runat="server" Text="Select Week Start Date:" CssClass="common-input-label"></asp:Label>
        <asp:TextBox ID="txtWeekStartDate" runat="server" CssClass="common-input-box" AutoPostBack="true" OnTextChanged="txtWeekStartDate_TextChanged"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="calWeekStartDate" runat="server" TargetControlID="txtWeekStartDate" Format="yyyy-MM-dd" />
    </div>

    <!-- GridView to display schedules -->
    <div class="common-form-group">
        <asp:Label ID="lblSchedules" runat="server" Text="Schedules for the Week:" CssClass="common-input-label"></asp:Label>
        <asp:GridView ID="gvSchedules" runat="server" CssClass="common-table" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="UserId" HeaderText="User ID" />
        <asp:BoundField DataField="ScheduleId" HeaderText="Schedule ID" />
        <asp:BoundField DataField="WeekStartDate" HeaderText="Week Start Date" />
        <asp:BoundField DataField="GroupId" HeaderText="Group" />

        <asp:BoundField DataField="Day1Date" HeaderText="Monday" />
        <asp:BoundField DataField="Day1ShiftName" HeaderText="Monday Shift" />

   
        <asp:BoundField DataField="Day2Date" HeaderText="Tuesday" />
        <asp:BoundField DataField="Day2ShiftName" HeaderText="Tuesday Shift" />

 
        <asp:BoundField DataField="Day3Date" HeaderText="Wednesday" />
        <asp:BoundField DataField="Day3ShiftName" HeaderText="Wednesday Shift" />

 
        <asp:BoundField DataField="Day4Date" HeaderText="Thursday" />
        <asp:BoundField DataField="Day4ShiftName" HeaderText="Thursday Shift" />


        <asp:BoundField DataField="Day5Date" HeaderText="Friday" />
        <asp:BoundField DataField="Day5ShiftName" HeaderText="Friday Shift" />

        <asp:BoundField DataField="Day6Date" HeaderText="Saturday" />
        <asp:BoundField DataField="Day6ShiftName" HeaderText="Saturday Shift" />

        <asp:BoundField DataField="Day7Date" HeaderText="Sunday" />
        <asp:BoundField DataField="Day7ShiftName" HeaderText="Sunday Shift" />
    </Columns>
</asp:GridView>

    </div>

    <!-- Button to Create Attendance Batch -->
    <div class="common-form-group">
        <asp:Button ID="btnCreateAttendanceBatch" runat="server" Text="Create Attendance Batch" CssClass="common-button-green" OnClick="btnCreateAttendanceBatch_Click" />
    </div>

    <!-- GridView to display the generated attendance batch -->
    <div class="common-form-group">
        <asp:Label ID="lblGeneratedBatch" runat="server" Text="Generated Attendance Batch:" CssClass="common-input-label"></asp:Label>
        <asp:GridView ID="gvAttendanceBatch" runat="server" CssClass="common-table" AutoGenerateColumns="False" Visible="false">
            <Columns>
                <asp:BoundField DataField="ScheduleId" HeaderText="Schedule ID" />
                <asp:BoundField DataField="UserId" HeaderText="User ID" />
                <asp:BoundField DataField="WeekStartDate" HeaderText="Week Start Date" />
                <asp:BoundField DataField="Date" HeaderText="Date" />
                <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                <asp:BoundField DataField="AttStatus" HeaderText="Status" />
            </Columns>
        </asp:GridView>
    </div>

    <!-- Button to Insert Attendance into the database -->
    <div class="common-form-group">
        <asp:Button ID="btnInsertAttendance" runat="server" Text="Insert Attendance" CssClass="common-button-green" Visible="false" OnClick="btnInsertAttendance_Click" />
    </div>
</asp:Content>
<%--test--%>