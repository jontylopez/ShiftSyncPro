using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm16 : System.Web.UI.Page
    {
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService();
        localhostSchedule.ScheduleManagementService sms = new localhostSchedule.ScheduleManagementService();
        localhostShift.ShiftManagementService shms = new localhostShift.ShiftManagementService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadShifts();
            }
        }

        private void ShowErrorMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", $"alert('{message}');", true);
        }

        private void ShowSuccessMessage(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", $"alert('{message}');", true);
        }

        private void LoadShifts()
        {
            try
            {
                DataSet ds = shms.GetAllShifts();
                if (ds != null && ds.Tables.Count > 0)
                {
                    ddlShiftSelect.Items.Clear();
                    ddlShiftSelect.Items.Add(new ListItem("Select Shift", ""));
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ListItem item = new ListItem(row["shiftType"].ToString(), row["id"].ToString());
                        ddlShiftSelect.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading shifts: {ex.Message}");
            }
        }

        protected void ddlGroupSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGroup = ddlGroupSelect.SelectedValue;
            LoadEmployees(selectedGroup);
        }

        private void LoadEmployees(string group)
        {
            try
            {
                DataSet ds = ums.GetUsersByGroup(group);
                if (ds != null && ds.Tables.Count > 0)
                {
                    gvEmployees.DataSource = ds;
                    gvEmployees.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading employees: {ex.Message}");
            }
        }

        protected void btnCreateSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlGroupSelect.SelectedValue))
                {
                    ShowErrorMessage("Please select a group.");
                    return;
                }

                if (string.IsNullOrEmpty(ddlShiftSelect.SelectedValue))
                {
                    ShowErrorMessage("Please select a shift.");
                    return;
                }

                if (!DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
                {
                    ShowErrorMessage("Invalid start date.");
                    return;
                }

                int selectedShiftId = int.Parse(ddlShiftSelect.SelectedValue); // Get the selected shift ID
                string groupId = ddlGroupSelect.SelectedValue;

                // Check for duplicate or conflicting schedules
                if (IsScheduleConflict(groupId, startDate))
                {
                    ShowErrorMessage("Schedule conflict detected. The user already has a schedule for this week.");
                    return;
                }

                // Get the DateTime object from the session
                if (Session["SelectedDateTime"] != null)
                {
                    DateTime selectedDateTime = (DateTime)Session["SelectedDateTime"];
                    TimeSpan selectedTime = selectedDateTime.TimeOfDay; // Get the time portion as TimeSpan

                    // Create and insert the schedule for each employee in the group
                    foreach (GridViewRow row in gvEmployees.Rows)
                    {
                        int userId = int.Parse(row.Cells[0].Text);


                        sms.AddSchedule(userId, groupId, startDate,
                            startDate.AddDays(0), chkMonday.Checked ? selectedShiftId : 6, // Monday
                            startDate.AddDays(1), chkTuesday.Checked ? selectedShiftId : 6, // Tuesday
                            startDate.AddDays(2), chkWednesday.Checked ? selectedShiftId : 6, // Wednesday
                            startDate.AddDays(3), chkThursday.Checked ? selectedShiftId : 6, // Thursday
                            startDate.AddDays(4), chkFriday.Checked ? selectedShiftId : 6, // Friday
                            startDate.AddDays(5), chkSaturday.Checked ? selectedShiftId : 6, // Saturday
                            startDate.AddDays(6), chkSunday.Checked ? selectedShiftId : 6, // Sunday
                            selectedDateTime.Date,
                            selectedTime.ToString());
                    }

                    ShowSuccessMessage("Schedule created successfully.");
                }
                else
                {
                    ShowErrorMessage("DateTime information is missing from the session.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error creating schedule: {ex.Message}");
            }
        }

        private bool IsScheduleConflict(string groupId, DateTime startDate)
        {
            // Fetch existing schedules
            DataSet existingSchedules = sms.GetAllSchedule();

            // Loop through the existing schedules and check for conflicts
            foreach (DataRow row in existingSchedules.Tables[0].Rows)
            {
                string existingGroupId = row["groupId"].ToString();
                DateTime existingStartDate = Convert.ToDateTime(row["weekStartDate"]);
                int existingUserId = Convert.ToInt32(row["userId"]);

                // Check if the group and week start date match the new schedule's group and start date
                if (existingGroupId == groupId && existingStartDate == startDate)
                {
                    // Conflict detected
                    return true;
                }
            }

            // No conflicts found
            return false;
        }

        private DateTime? GetDateForDay(CheckBox chkDay, DateTime dayDate)
        {
            return chkDay.Checked ? dayDate : (DateTime?)null;
        }

        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
            {
                // Set the labels for each day of the week based on the selected start date
                lblMondayDateValue.Text = startDate.ToString("yyyy-MM-dd");
                lblTuesdayDateValue.Text = startDate.AddDays(1).ToString("yyyy-MM-dd");
                lblWednesdayDateValue.Text = startDate.AddDays(2).ToString("yyyy-MM-dd");
                lblThursdayDateValue.Text = startDate.AddDays(3).ToString("yyyy-MM-dd");
                lblFridayDateValue.Text = startDate.AddDays(4).ToString("yyyy-MM-dd");
                lblSaturdayDateValue.Text = startDate.AddDays(5).ToString("yyyy-MM-dd");
                lblSundayDateValue.Text = startDate.AddDays(6).ToString("yyyy-MM-dd");
            }
            else
            {
                // Show an error message if the date format is not valid
                ShowErrorMessage("Please enter a valid date in the format yyyy-MM-dd.");
            }
        }
    }
}