<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminCreateSchedule.aspx.cs" Inherits="ShiftSyncPro.WebForm16" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
        <!-- ScriptManager to enable AJAX Control Toolkit -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <div class="common-form-container">
        <h2 class="login-title">Create Next Week's Schedule</h2>

        <!-- Dropdown for Group Selection -->
        <div class="common-form-group">
            <asp:Label ID="lblGroupSelect" runat="server" Text="Select Group:" CssClass="common-input-label"></asp:Label>
            <asp:DropDownList ID="ddlGroupSelect" runat="server" CssClass="common-select-box" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupSelect_SelectedIndexChanged">
                <asp:ListItem Text="Select Group" Value=""></asp:ListItem>
                <asp:ListItem Text="Group A" Value="A"></asp:ListItem>
                <asp:ListItem Text="Group B" Value="B"></asp:ListItem>
                <asp:ListItem Text="Group C" Value="C"></asp:ListItem>
                <asp:ListItem Text="Group HR" Value="H"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Date Picker for Start Date -->
        <div class="common-form-group">
            <asp:Label ID="lblStartDate" runat="server" Text="Select Start Date:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="common-input-box" AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd" />
        </div>
        
        <!-- Checkboxes for Selecting Working Days -->
        <div class="common-form-group">
            <asp:Label ID="lblSelectDays" runat="server" Text="Select Working Days:" CssClass="common-input-label"></asp:Label>
            <div class="working-days">
                <div class="day-checkbox-group">
                    <asp:CheckBox ID="chkMonday" runat="server" CssClass="common-checkbox" />
                    <asp:Label ID="lblMondayDate" runat="server" Text="Monday" CssClass="common-day-label"></asp:Label>
                    <asp:Label ID="lblMondayDateValue" runat="server" Text="(yyyy-MM-dd)" CssClass="common-date-label"></asp:Label>
                </div>
                <div class="day-checkbox-group">
                    <asp:CheckBox ID="chkTuesday" runat="server" CssClass="common-checkbox" />
                    <asp:Label ID="lblTuesdayDate" runat="server" Text="Tuesday" CssClass="common-day-label"></asp:Label>
                    <asp:Label ID="lblTuesdayDateValue" runat="server" Text="(yyyy-MM-dd)" CssClass="common-date-label"></asp:Label>
                </div>
                <div class="day-checkbox-group">
                    <asp:CheckBox ID="chkWednesday" runat="server" CssClass="common-checkbox" />
                    <asp:Label ID="lblWednesdayDate" runat="server" Text="Wednesday" CssClass="common-day-label"></asp:Label>
                    <asp:Label ID="lblWednesdayDateValue" runat="server" Text="(yyyy-MM-dd)" CssClass="common-date-label"></asp:Label>
                </div>
                <div class="day-checkbox-group">
                    <asp:CheckBox ID="chkThursday" runat="server" CssClass="common-checkbox" />
                    <asp:Label ID="lblThursdayDate" runat="server" Text="Thursday" CssClass="common-day-label"></asp:Label>
                    <asp:Label ID="lblThursdayDateValue" runat="server" Text="(yyyy-MM-dd)" CssClass="common-date-label"></asp:Label>
                </div>
                <div class="day-checkbox-group">
                    <asp:CheckBox ID="chkFriday" runat="server" CssClass="common-checkbox" />
                    <asp:Label ID="lblFridayDate" runat="server" Text="Friday" CssClass="common-day-label"></asp:Label>
                    <asp:Label ID="lblFridayDateValue" runat="server" Text="(yyyy-MM-dd)" CssClass="common-date-label"></asp:Label>
                </div>
                <div class="day-checkbox-group">
                    <asp:CheckBox ID="chkSaturday" runat="server" CssClass="common-checkbox" />
                    <asp:Label ID="lblSaturdayDate" runat="server" Text="Saturday" CssClass="common-day-label"></asp:Label>
                    <asp:Label ID="lblSaturdayDateValue" runat="server" Text="(yyyy-MM-dd)" CssClass="common-date-label"></asp:Label>
                </div>
                <div class="day-checkbox-group">
                    <asp:CheckBox ID="chkSunday" runat="server" CssClass="common-checkbox" />
                    <asp:Label ID="lblSundayDate" runat="server" Text="Sunday" CssClass="common-day-label"></asp:Label>
                    <asp:Label ID="lblSundayDateValue" runat="server" Text="(yyyy-MM-dd)" CssClass="common-date-label"></asp:Label>
                </div>
            </div>
        </div>
        
        <!-- Dropdown for Assigning Shift to the Group -->
        <div class="common-form-group">
            <asp:Label ID="lblShiftSelect" runat="server" Text="Select Shift for Group:" CssClass="common-input-label"></asp:Label>
            <asp:DropDownList ID="ddlShiftSelect" runat="server" CssClass="common-select-box">
                <asp:ListItem Text="Select Shift" Value=""></asp:ListItem>
                
            </asp:DropDownList>
        </div>
        
        <!-- Button to Create the Schedule -->
        <div class="common-form-group">
            <asp:Button ID="btnCreateSchedule" runat="server" Text="Create Schedule" CssClass="common-button-green" OnClick="btnCreateSchedule_Click" />
        </div>
    </div>
    <!-- GridView for Displaying Employees of the Selected Group -->
<div class="common-form-group">
    <asp:Label ID="lblEmployees" runat="server" Text="Employees in Selected Group:" CssClass="common-input-label"></asp:Label>
    <asp:GridView ID="gvEmployees" runat="server" CssClass="common-table" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
            <asp:BoundField DataField="EmpTel" HeaderText="Telephone" />
            <asp:BoundField DataField="EmpMail" HeaderText="Email" />
        </Columns>
    </asp:GridView>
</div>
</asp:Content>
