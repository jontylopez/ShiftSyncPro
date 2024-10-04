using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        localhostSchedule.ScheduleManagementService sms = new localhostSchedule.ScheduleManagementService();
        localhostShift.ShiftManagementService shms = new localhostShift.ShiftManagementService();
        localhostRequest.RequestManagementService rms = new localhostRequest.RequestManagementService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["SelectedDateTime"] != null)
                {
                    try
                    {
                        int userId = (int)Session["UserId"];
                        DateTime currentDate = (DateTime)Session["SelectedDateTime"];
                        LoadScheduleForNextWeek(userId, currentDate);
                    }
                    catch (InvalidCastException)
                    {
                        lblMessage.Text = "Invalid date format in session. Please log in again.";
                        lblMessage.CssClass = "common-input-label-error";
                        lblMessage.Visible = true;
                        Response.Redirect("Login.aspx");
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error loading schedule: " + ex.Message;
                        lblMessage.CssClass = "common-input-label-error";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Text = "Session expired or invalid. Please log in again.";
                    lblMessage.CssClass = "common-input-label-error";
                    lblMessage.Visible = true;
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void LoadScheduleForNextWeek(int userId, DateTime currentDate)
        {
            try
            {
                DataSet ds = sms.GetUpcomingScheduleForUser(userId, currentDate);

                if (ds == null || ds.Tables.Count == 0)
                {
                    lblMessage.Text = "Error: Dataset is null or contains no tables.";
                    lblMessage.CssClass = "common-input-label-error";
                    lblMessage.Visible = true;
                    return;
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    lblMessage.Text = "Error: No rows in the dataset.";
                    lblMessage.CssClass = "common-input-label-error";
                    lblMessage.Visible = true;
                    return;
                }

                DataRow scheduleRow = ds.Tables[0].Rows[0];
                lblScheduleId.Text = scheduleRow["id"].ToString();
                lblUserId.Text = "User ID: " + scheduleRow["userId"].ToString();
                lblGroupId.Text = "Group ID: " + scheduleRow["groupId"].ToString();
                lblDayCreate.Text = "Day Created: " + ((DateTime)scheduleRow["dayCreate"]).ToString("yyyy-MM-dd");
                PopulateDayLabels(scheduleRow);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading schedule: " + ex.Message;
                lblMessage.CssClass = "common-input-label-error";
                lblMessage.Visible = true;
            }
        }

        private void PopulateDayLabels(DataRow scheduleRow)
        {
            if (scheduleRow["day1Date"] != DBNull.Value)
            {
                PopulateDayDetails(scheduleRow["day1ShiftId"], lblMondayShift, ddlMondayChangeShift, (DateTime)scheduleRow["day1Date"]);
            }
            if (scheduleRow["day2Date"] != DBNull.Value)
            {
                PopulateDayDetails(scheduleRow["day2ShiftId"], lblTuesdayShift, ddlTuesdayChangeShift, (DateTime)scheduleRow["day2Date"]);
            }
            if (scheduleRow["day3Date"] != DBNull.Value)
            {
                PopulateDayDetails(scheduleRow["day3ShiftId"], lblWednesdayShift, ddlWednesdayChangeShift, (DateTime)scheduleRow["day3Date"]);
            }
            if (scheduleRow["day4Date"] != DBNull.Value)
            {
                PopulateDayDetails(scheduleRow["day4ShiftId"], lblThursdayShift, ddlThursdayChangeShift, (DateTime)scheduleRow["day4Date"]);
            }
            if (scheduleRow["day5Date"] != DBNull.Value)
            {
                PopulateDayDetails(scheduleRow["day5ShiftId"], lblFridayShift, ddlFridayChangeShift, (DateTime)scheduleRow["day5Date"]);
            }
            if (scheduleRow["day6Date"] != DBNull.Value)
            {
                PopulateDayDetails(scheduleRow["day6ShiftId"], lblSaturdayShift, ddlSaturdayChangeShift, (DateTime)scheduleRow["day6Date"]);
            }
            if (scheduleRow["day7Date"] != DBNull.Value)
            {
                PopulateDayDetails(scheduleRow["day7ShiftId"], lblSundayShift, ddlSundayChangeShift, (DateTime)scheduleRow["day7Date"]);
            }
        }

        private void PopulateDayDetails(object shiftId, Label shiftLabel, DropDownList shiftDropdown, DateTime scheduleDate)
        {
            // Load shifts into the dropdown first
            LoadShiftsIntoDropdown(shiftDropdown);

            string shiftName = GetShiftName(shiftId);
            shiftLabel.Text = $"Current Shift: {shiftName}";

            if (shiftId != DBNull.Value && shiftId != null)
            {
                // Check if the shift is "Holiday" (shiftId == 6) and disable the dropdown if true
                int id = Convert.ToInt32(shiftId);
                if (id == 6)
                {
                    shiftDropdown.Enabled = false;  // Disable dropdown for "Holiday"
                }
                else
                {
                    shiftDropdown.Enabled = true;  // Enable for other shifts
                }

                // Set the selected item in the dropdown based on shiftId
                ListItem currentItem = shiftDropdown.Items.FindByValue(id.ToString());
                if (currentItem != null)
                {
                    currentItem.Selected = true;  // Set the selected item
                }
                else
                {
                    shiftDropdown.Items.Insert(0, new ListItem(shiftName, id.ToString())); // Manually insert if not found
                    shiftDropdown.SelectedIndex = 0; // Select the manually added item
                }
            }

            // Store the date for later use
            shiftLabel.Attributes["ScheduleDate"] = scheduleDate.ToString("yyyy-MM-dd");
        }

        private void LoadShiftsIntoDropdown(DropDownList ddlShift)
        {
            try
            {
                DataSet shiftDataSet = shms.GetAllShifts();
                if (shiftDataSet != null && shiftDataSet.Tables.Count > 0)
                {
                    ddlShift.Items.Clear();
                    ddlShift.Items.Add(new ListItem("Select Shift", ""));
                    foreach (DataRow row in shiftDataSet.Tables[0].Rows)
                    {
                        ddlShift.Items.Add(new ListItem(row["shiftType"].ToString(), row["id"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading shifts: " + ex.Message);
            }
        }

        private string GetShiftName(object shiftId)
        {
            if (shiftId != DBNull.Value && shiftId != null)
            {
                try
                {
                    int id = Convert.ToInt32(shiftId);
                    DataSet shiftDataSet = shms.GetShiftById(id);
                    if (shiftDataSet != null && shiftDataSet.Tables.Count > 0 && shiftDataSet.Tables[0].Rows.Count > 0)
                    {
                        return shiftDataSet.Tables[0].Rows[0]["shiftType"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving shift details: " + ex.Message);
                    return "Error";
                }
            }
            return "No Shift";
        }

        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // Gather values from labels and dropdowns
                int userId = (int)Session["UserId"];
                int scheduleId = Convert.ToInt32(lblScheduleId.Text);
                DateTime requestDate = DateTime.Now;

                // Get the week start date
                DateTime weekStartDate = DateTime.Parse(lblDayCreate.Text.Replace("Day Created: ", ""));

                // Calculate dates for each day
                DateTime day1Date = weekStartDate;
                DateTime day2Date = weekStartDate.AddDays(1);
                DateTime day3Date = weekStartDate.AddDays(2);
                DateTime day4Date = weekStartDate.AddDays(3);
                DateTime day5Date = weekStartDate.AddDays(4);
                DateTime day6Date = weekStartDate.AddDays(5);
                DateTime day7Date = weekStartDate.AddDays(6);

                // Get shift IDs from dropdowns
                int? day1ShiftId = !string.IsNullOrEmpty(ddlMondayChangeShift.SelectedValue) ? int.Parse(ddlMondayChangeShift.SelectedValue) : (int?)null;
                int? day2ShiftId = !string.IsNullOrEmpty(ddlTuesdayChangeShift.SelectedValue) ? int.Parse(ddlTuesdayChangeShift.SelectedValue) : (int?)null;
                int? day3ShiftId = !string.IsNullOrEmpty(ddlWednesdayChangeShift.SelectedValue) ? int.Parse(ddlWednesdayChangeShift.SelectedValue) : (int?)null;
                int? day4ShiftId = !string.IsNullOrEmpty(ddlThursdayChangeShift.SelectedValue) ? int.Parse(ddlThursdayChangeShift.SelectedValue) : (int?)null;
                int? day5ShiftId = !string.IsNullOrEmpty(ddlFridayChangeShift.SelectedValue) ? int.Parse(ddlFridayChangeShift.SelectedValue) : (int?)null;
                int? day6ShiftId = !string.IsNullOrEmpty(ddlSaturdayChangeShift.SelectedValue) ? int.Parse(ddlSaturdayChangeShift.SelectedValue) : (int?)null;
                int? day7ShiftId = !string.IsNullOrEmpty(ddlSundayChangeShift.SelectedValue) ? int.Parse(ddlSundayChangeShift.SelectedValue) : (int?)null;

                // Get the comment
                string workerComment = txtComment.Text;

                // Handle file upload and use default image if no file is uploaded
                byte[] proofDocument = null;
                string proofDocumentType = null;

                if (fileUploadProof.HasFile)
                {
                    proofDocument = fileUploadProof.FileBytes;
                    proofDocumentType = fileUploadProof.PostedFile.ContentType; // Get the MIME type
                }
                else
                {
                    // Use default image
                    string defaultImagePath = Server.MapPath("~/Images/DefaultImage.jpg");
                    proofDocument = System.IO.File.ReadAllBytes(defaultImagePath);
                    proofDocumentType = "image/jpeg"; // Assuming the default image is a JPG
                }

                // Call the AddWeeklyRequest WebMethod to insert the request
                rms.AddWeeklyRequest(userId, scheduleId, day1ShiftId, day1Date, day2ShiftId, day2Date, day3ShiftId, day3Date,
                                     day4ShiftId, day4Date, day5ShiftId, day5Date, day6ShiftId, day6Date, day7ShiftId, day7Date,
                                     "Pending", requestDate, workerComment, proofDocument, proofDocumentType);

                // Provide feedback to the user
                lblMessage.Text = "Request submitted successfully.";
                lblMessage.CssClass = "common-input-label-success";
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error submitting request: " + ex.Message;
                lblMessage.CssClass = "common-input-label-error";
                lblMessage.Visible = true;
            }
        }
    }
}
