using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm18 : System.Web.UI.Page
    {
        localhostSchedule.ScheduleManagementService sms = new localhostSchedule.ScheduleManagementService();
        localhostAttendance.AttendanceManagementService ams = new localhostAttendance.AttendanceManagementService();
        localhostShift.ShiftManagementService shms = new localhostShift.ShiftManagementService();

        private List<Attendance> attendanceBatch = new List<Attendance>();  // Store attendance batch in session

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["attendanceBatch"] = null; // Clear the batch on page load
            }
        }

        protected void txtWeekStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtWeekStartDate.Text, out DateTime weekStartDate))
            {
                LoadSchedules(weekStartDate);
            }
            else
            {
                lblSchedules.Text = "Please select a valid date.";
            }
        }

        // Load Schedules based on Week Start Date and bind the shift names
        private void LoadSchedules(DateTime weekStartDate)
        {
            DataSet ds = sms.GetScheduleByStartDate(weekStartDate);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                // Add new columns for shift names
                dt.Columns.Add("Day1ShiftName", typeof(string));
                dt.Columns.Add("Day2ShiftName", typeof(string));
                dt.Columns.Add("Day3ShiftName", typeof(string));
                dt.Columns.Add("Day4ShiftName", typeof(string));
                dt.Columns.Add("Day5ShiftName", typeof(string));
                dt.Columns.Add("Day6ShiftName", typeof(string));
                dt.Columns.Add("Day7ShiftName", typeof(string));

                // Loop through each row and populate the shift names
                foreach (DataRow row in dt.Rows)
                {
                    // Fetch shift names for each day based on ShiftId
                    row["Day1ShiftName"] = GetShiftNameById(Convert.ToInt32(row["Day1ShiftId"]));
                    row["Day2ShiftName"] = GetShiftNameById(Convert.ToInt32(row["Day2ShiftId"]));
                    row["Day3ShiftName"] = GetShiftNameById(Convert.ToInt32(row["Day3ShiftId"]));
                    row["Day4ShiftName"] = GetShiftNameById(Convert.ToInt32(row["Day4ShiftId"]));
                    row["Day5ShiftName"] = GetShiftNameById(Convert.ToInt32(row["Day5ShiftId"]));
                    row["Day6ShiftName"] = GetShiftNameById(Convert.ToInt32(row["Day6ShiftId"]));
                    row["Day7ShiftName"] = GetShiftNameById(Convert.ToInt32(row["Day7ShiftId"]));
                }

                gvSchedules.DataSource = dt;
                gvSchedules.DataBind();
            }
            else
            {
                gvSchedules.DataSource = null;
                gvSchedules.DataBind();
                lblSchedules.Text = "No schedules found for the selected week.";
            }
        }

        // Retrieve the Shift Name from Shift Id
        private string GetShiftNameById(int shiftId)
        {
            if (shiftId == 5) // No shift logic
            {
                return "No Shift";
            }

            // Use the ShiftManagementService to get shift details
            DataSet shiftData = shms.GetShiftById(shiftId);
            if (shiftData != null && shiftData.Tables.Count > 0 && shiftData.Tables[0].Rows.Count > 0)
            {
                DataRow shiftRow = shiftData.Tables[0].Rows[0];
                return shiftRow["ShiftType"].ToString(); // Assume "ShiftType" holds the shift name
            }

            return "Unknown Shift"; // Default if not found
        }

        // Create Attendance Batch and bind it to the grid
        protected void btnCreateAttendanceBatch_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtWeekStartDate.Text, out DateTime weekStartDate))
            {
                attendanceBatch = new List<Attendance>();  // Reset batch

                foreach (GridViewRow row in gvSchedules.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string scheduleIdText = row.Cells[1].Text; // ScheduleId is in column 1
                        string userIdText = row.Cells[0].Text;     // UserId is in column 0

                        if (int.TryParse(scheduleIdText, out int scheduleId) && int.TryParse(userIdText, out int userId))
                        {
                            // Loop through each day (Monday to Sunday)
                            for (int day = 1; day <= 7; day++)
                            {
                                DateTime currentDate = weekStartDate.AddDays(day - 1); // Get relevant date

                                // Retrieve the shift name for each day (e.g., "Morning", "Evening")
                                string shiftName = GetShiftNameForDay(row, day);  // Custom method to get shift name

                                // Get the corresponding shift ID using the shift name
                                DataSet shiftData = shms.GetShiftByType(shiftName);
                                if (shiftData != null && shiftData.Tables.Count > 0 && shiftData.Tables[0].Rows.Count > 0)
                                {
                                    DataRow shiftRow = shiftData.Tables[0].Rows[0];
                                    int shiftId = Convert.ToInt32(shiftRow["Id"]);

                                    if (shiftId != 5) // Skip if no shift
                                    {
                                        string startTime = shiftRow["startTime"].ToString();
                                        string endTime = shiftRow["endTime"].ToString();

                                        // Add the generated attendance entry to the batch
                                        attendanceBatch.Add(new Attendance
                                        {
                                            ScheduleId = scheduleId,
                                            UserId = userId,
                                            WeekStartDate = weekStartDate,
                                            Date = currentDate,
                                            StartTime = startTime,
                                            EndTime = endTime,
                                            AttStatus = "Pending"
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                // Bind the attendance batch to the GridView
                if (attendanceBatch.Count > 0)
                {
                    gvAttendanceBatch.DataSource = attendanceBatch;
                    gvAttendanceBatch.DataBind();
                    gvAttendanceBatch.Visible = true;
                    btnInsertAttendance.Visible = true;

                    Session["attendanceBatch"] = attendanceBatch;
                }
                else
                {
                    lblSchedules.Text = "No attendance records to display.";
                    gvAttendanceBatch.Visible = false;
                    btnInsertAttendance.Visible = false;
                }
            }
            else
            {
                lblSchedules.Text = "Please select a valid week start date.";
            }
        }
        private string GetShiftNameForDay(GridViewRow row, int dayOfWeek)
        {
            // Map the dayOfWeek to the corresponding column for ShiftName in the GridView
            string shiftName = string.Empty;

            // Adjust the column index based on your actual GridView structure
            switch (dayOfWeek)
            {
                case 1:
                    shiftName = row.Cells[5].Text;
                    break;
                case 2:
                    shiftName = row.Cells[7].Text;
                    break;
                case 3:
                    shiftName = row.Cells[9].Text;
                    break;
                case 4:
                    shiftName = row.Cells[11].Text;
                    break;
                case 5:
                    shiftName = row.Cells[13].Text;
                    break;
                case 6:
                    shiftName = row.Cells[15].Text;
                    break;
                case 7:
                    // Ensure there are enough cells in the row
                    if (row.Cells.Count > 17)
                    {
                        shiftName = row.Cells[17].Text; // Sunday Shift column
                    }
                    else
                    {
                        // Handle the case where the cell is out of range
                        shiftName = "No Shift"; // Default or handle appropriately
                    }
                    break;
                default:
                    shiftName = "Invalid Day"; // Handle invalid dayOfWeek
                    break;
            }

            return shiftName;
        }
        // Insert the attendance batch into the database
        protected void btnInsertAttendance_Click(object sender, EventArgs e)
        {

            // Retrieve existing attendance records
            DataSet existingAttendanceRecords = ams.GetAllAttendance();

            attendanceBatch = (List<Attendance>)Session["attendanceBatch"];
            if (attendanceBatch != null && attendanceBatch.Count > 0)
            {
                // Filter out duplicates based on userId and Date
                List<Attendance> validAttendanceBatch = new List<Attendance>();

                foreach (var newAttendance in attendanceBatch)
                {
                    bool isDuplicate = false;

                    // Check against existing records for duplicates
                    foreach (DataRow existingRow in existingAttendanceRecords.Tables[0].Rows)
                    {
                        int existingUserId = Convert.ToInt32(existingRow["userId"]);
                        DateTime existingDate = Convert.ToDateTime(existingRow["date"]);

                        if (newAttendance.UserId == existingUserId && newAttendance.Date == existingDate)
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    // Only add to valid batch if it's not a duplicate
                    if (!isDuplicate)
                    {
                        validAttendanceBatch.Add(newAttendance);
                    }
                }

                if (validAttendanceBatch.Count > 0)
                {
                    localhostAttendance.Attendance[] attendanceArray = validAttendanceBatch
                        .Select(a => new localhostAttendance.Attendance
                        {
                            ScheduleId = a.ScheduleId,
                            UserId = a.UserId,
                            WeekStartDate = a.WeekStartDate,
                            Date = a.Date,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            AttStatus = a.AttStatus
                        })
                        .ToArray();

                    // Insert the valid attendance batch into the database
                    int result = ams.InsertAttendanceBatch(attendanceArray);
                    lblSchedules.Text = result > 0 ? "Attendance batch inserted successfully." : "Failed to insert attendance batch.";
                }
                else
                {
                    lblSchedules.Text = "No new attendance records to insert. All entries are duplicates.";
                }
            }
            else
            {
                lblSchedules.Text = "No attendance records to insert.";
            }
        }

        // Helper method to get ShiftId for each day from GridView row
        private int GetShiftIdForDay(GridViewRow row, int dayOfWeek)
        {
            // Map the dayOfWeek to the corresponding column for ShiftId in the GridView
            string shiftIdText = "5"; // Default to no shift
            switch (dayOfWeek)
            {
                case 1:
                    shiftIdText = row.Cells[5].Text; // Monday shift column
                    break;
                case 2:
                    shiftIdText = row.Cells[7].Text; // Tuesday shift column
                    break;
                case 3:
                    shiftIdText = row.Cells[9].Text; // Wednesday shift column
                    break;
                case 4:
                    shiftIdText = row.Cells[11].Text; // Thursday shift column
                    break;
                case 5:
                    shiftIdText = row.Cells[13].Text; // Friday shift column
                    break;
                case 6:
                    shiftIdText = row.Cells[15].Text; // Saturday shift column
                    break;
                case 7:
                    shiftIdText = row.Cells[17].Text; // Sunday shift column
                    break;
            }

            // Convert the shiftIdText to integer, return 5 if not valid
            return int.TryParse(shiftIdText, out int shiftId) ? shiftId : 5;
        }
    }
}