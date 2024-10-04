using System;
using System.Data;
using System.Web.UI;

namespace ShiftSyncProAttendance
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService();
        localhostAttendance.AttendanceManagementService attendanceService = new localhostAttendance.AttendanceManagementService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the current date and time from session or fallback to current date/time
                DateTime selectedDate;
                if (Session["SelectedDateTime"] != null)
                {
                    selectedDate = (DateTime)Session["SelectedDateTime"];
                    lblCurrentDate.Text = selectedDate.ToString("yyyy-MM-dd");
                    lblCurrentTime.Text = selectedDate.ToString("HH:mm:ss");
                }
                else
                {
                    selectedDate = DateTime.Now;
                    lblCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
                }

                // Pre-populate the week start date (assume Monday as week start)
                DateTime weekStartDate = selectedDate.AddDays(-(int)selectedDate.DayOfWeek + (int)DayOfWeek.Monday);
                txtWeekStartDate.Text = weekStartDate.ToString("yyyy-MM-dd");

                // Load attendance records for the selected date
                LoadAttendanceRecords(selectedDate);
            }
        }

        // Method to load attendance records for the selected date
        private void LoadAttendanceRecords(DateTime selectedDate)
        {
            DataSet attendanceData = attendanceService.GetAttendanceByDate(selectedDate);

            if (attendanceData != null && attendanceData.Tables.Count > 0 && attendanceData.Tables[0].Rows.Count > 0)
            {
                gvAttendanceRecords.DataSource = attendanceData;
                gvAttendanceRecords.DataBind();
            }
            else
            {
                lblMessage.Text = "No attendance records found for the selected date.";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Get the current clock time from the hidden field
            string currentClockTime = hfClockTime.Value;

            // Validate user
            string userRole = ums.IsValidUser(username, password);
            int userId = ums.GetUserID(username, password);

            if (!string.IsNullOrEmpty(userRole) && userId > 0)
            {
                // Check if attendance record exists for the user and the current date
                DateTime selectedDate = DateTime.Parse(lblCurrentDate.Text);
                DataSet attendanceData = attendanceService.GetAttendanceByUser(userId);

                if (attendanceData != null && attendanceData.Tables.Count > 0 && attendanceData.Tables[0].Rows.Count > 0)
                {
                    DataRow attendanceRow = attendanceData.Tables[0].Rows[0];
                    string inTime = attendanceRow["inTime"].ToString();
                    string outTime = attendanceRow["outTime"].ToString();
                    string startTime = attendanceRow["startTime"].ToString(); // Assuming these values exist in the table
                    string endTime = attendanceRow["endTime"].ToString();    // Assuming these values exist in the table
                    int attendanceId = Convert.ToInt32(attendanceRow["id"]); // Attendance record ID

                    DateTime startTimeDT = DateTime.Parse(startTime);
                    DateTime endTimeDT = DateTime.Parse(endTime);
                    DateTime currentClockTimeDT = DateTime.Parse(currentClockTime);

                    // If inTime is null, update inTime
                    if (string.IsNullOrEmpty(inTime))
                    {
                        int result = attendanceService.InsertInTime(userId, selectedDate, currentClockTime);

                        if (result > 0)
                        {
                            lblMessage.Text = "In-Time successfully recorded.";
                        }
                        else
                        {
                            lblMessage.Text = "Error updating In-Time. No record found.";
                        }
                    }
                    // If inTime is present, update outTime and status
                    else if (!string.IsNullOrEmpty(inTime) && string.IsNullOrEmpty(outTime))
                    {
                        int result = attendanceService.InsertOutTime(userId, selectedDate, currentClockTime);

                        if (result > 0)
                        {
                            lblMessage.Text = "Out-Time successfully recorded.";

                            // Now check the status based on inTime and outTime
                            DateTime inTimeDT = DateTime.Parse(inTime);

                            // Check the 30 minutes before and 10 minutes after startTime condition
                            if (inTimeDT >= startTimeDT.AddMinutes(-30) && inTimeDT <= startTimeDT.AddMinutes(10) && currentClockTimeDT >= endTimeDT)
                            {
                                // Normal status
                                attendanceService.UpdateAttStatus(attendanceId, "Normal");
                                lblMessage.Text += " Status: Normal.";
                            }
                            else
                            {
                                // Irregular status
                                attendanceService.UpdateAttStatus(attendanceId, "Irregular");
                                lblMessage.Text += " Status: Irregular.";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Error updating Out-Time.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Attendance for the day already complete.";
                    }
                }
                else
                {
                    lblMessage.Text = "No attendance record found for the user.";
                }
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }
    }
}
