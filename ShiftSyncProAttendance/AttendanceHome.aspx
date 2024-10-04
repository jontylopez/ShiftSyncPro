<%@ Page Title="" Language="C#" MasterPageFile="~/AttendanceMaster.Master" AutoEventWireup="true" CodeBehind="AttendanceHome.aspx.cs" Inherits="ShiftSyncProAttendance.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="form-container">
        <h2>Attendance Form</h2>

        <!-- Message Display -->
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="common-input-label-error"></asp:Label>

        <!-- Display Current Date -->
        <div class="common-form-group">
            <asp:Label ID="lblCurrentDateLabel" runat="server" Text="Date:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblCurrentDate" runat="server" Text="" CssClass="common-input-label"></asp:Label>
        </div>

        <!-- Display Current Time (with a starting value from the session) -->
        <div class="common-form-group">
            <asp:Label ID="lblCurrentTimeLabel" runat="server" Text="Current Time:" CssClass="common-input-label"></asp:Label>
            <asp:Label ID="lblCurrentTime" runat="server" Text="" CssClass="common-input-label"></asp:Label>
            <span id="clock" class="clock-display"></span>
        </div>
        <asp:HiddenField ID="hfClockTime" runat="server" />


        <!-- Week Start Date (date picker) -->
        <div class="common-form-group">
            <asp:Label ID="lblWeekStartDate" runat="server" Text="Week Start Date:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtWeekStartDate" runat="server" TextMode="Date" CssClass="common-input-box"></asp:TextBox>
        </div>

        <!-- Username -->
        <div class="common-form-group">
            <asp:Label ID="lblUserName" runat="server" Text="Username:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="common-input-box"></asp:TextBox>
        </div>

        <!-- Password -->
        <div class="common-form-group">
            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="common-input-label"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="common-input-box"></asp:TextBox>
        </div>

        <!-- Submit Button -->
        <div class="common-form-group">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit Attendance" CssClass="common-button" OnClick="btnSubmit_Click" />
        </div>

        <!-- Attendance Records Table -->
        <h3>Attendance Records for Selected Date</h3>
        <asp:GridView ID="gvAttendanceRecords" runat="server" CssClass="common-table">
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="User ID" />
                <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                <asp:BoundField DataField="InTime" HeaderText="In Time" />
                <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                <asp:BoundField DataField="AttStatus" HeaderText="Status" />
            </Columns>
        </asp:GridView>

    </div>

    <script type="text/javascript">
        // Initialize the clock with the selected session time
        window.onload = function () {
            var sessionTime = '<%= Session["SelectedDateTime"] != null ? ((DateTime)Session["SelectedDateTime"]).ToString("HH:mm:ss") : "" %>';
            if (sessionTime) {
                startClock(sessionTime);
            } else {
                startClock(); // Start with current time if no session time
            }
        }

        // Initialize the clock with the selected session time
        window.onload = function () {
            var sessionTime = '<%= Session["SelectedDateTime"] != null ? ((DateTime)Session["SelectedDateTime"]).ToString("HH:mm:ss") : "" %>';
            if (sessionTime) {
                startClock(sessionTime);
            } else {
                startClock(); // Start with current time if no session time
            }
        }

        // JavaScript clock
        function startClock(startTime) {
            var clockElement = document.getElementById("clock");
            var time = startTime ? new Date('1970-01-01T' + startTime + 'Z') : new Date();
            var tick = function () {
                time.setSeconds(time.getSeconds() + 1);
                clockElement.innerHTML = time.toISOString().substr(11, 8); // Display HH:mm:ss
                document.getElementById("<%= hfClockTime.ClientID %>").value = time.toISOString().substr(11, 8); // Set hidden field value
            };
            setInterval(tick, 1000);
            tick();
        }

    </script>
</asp:Content>
