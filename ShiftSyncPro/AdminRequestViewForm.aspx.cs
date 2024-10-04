using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm13 : System.Web.UI.Page
    {
        localhostRequest.RequestManagementService rms = new localhostRequest.RequestManagementService();
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService();
        localhostSchedule.ScheduleManagementService sms = new localhostSchedule.ScheduleManagementService();
        localhostShift.ShiftManagementService shms = new localhostShift.ShiftManagementService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string requestId = Request.QueryString["requestId"];
                if (!string.IsNullOrEmpty(requestId))
                {
                    LoadRequestDetails(Convert.ToInt32(requestId));
                }
            }
        }

        private void LoadRequestDetails(int requestId)
        {
            // Fetch the request details from the database
            DataSet ds = rms.GetRequestById(requestId); // Assuming you have this method
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                // Populate labels with request details
                lblRequestIdValue.Text = row["id"].ToString();
                lblScheduleIdValue.Text = row["scheduleId"].ToString();

                // Load user details if necessary
                int userId = Convert.ToInt32(row["userId"]);
                DataSet userDs = ums.GetUserById(userId);
                if (userDs != null && userDs.Tables.Count > 0 && userDs.Tables[0].Rows.Count > 0)
                {
                    DataRow userRow = userDs.Tables[0].Rows[0];
                    lblUserName.Text = userRow["uName"].ToString();
                }

                // Populate the shifts and days
                lblMondayShift.Text = GetShiftDetails(row["day1ShiftId"], row["day1Date"]);
                lblTuesdayShift.Text = GetShiftDetails(row["day2ShiftId"], row["day2Date"]);
                lblWednesdayShift.Text = GetShiftDetails(row["day3ShiftId"], row["day3Date"]);
                lblThursdayShift.Text = GetShiftDetails(row["day4ShiftId"], row["day4Date"]);
                lblFridayShift.Text = GetShiftDetails(row["day5ShiftId"], row["day5Date"]);
                lblSaturdayShift.Text = GetShiftDetails(row["day6ShiftId"], row["day6Date"]);
                lblSundayShift.Text = GetShiftDetails(row["day7ShiftId"], row["day7Date"]);

                // Populate comments
                txtComment.Text = row["workerComment"].ToString(); // Assuming the worker's comment is stored here
                txtSupervisorComment.Text = row["supervisorComment"].ToString(); // Supervisor's comment

                // Handle proof document
                if (row["proofDocument"] != DBNull.Value)
                {
                    byte[] proofDocument = (byte[])row["proofDocument"];
                    // Logic to handle proof document (e.g., display or allow download)
                }
            }
        }

        private string GetShiftDetails(object shiftId, object date)
        {
            if (shiftId != DBNull.Value && date != DBNull.Value)
            {
                DataSet shiftDs = shms.GetShiftById(Convert.ToInt32(shiftId));
                if (shiftDs != null && shiftDs.Tables.Count > 0 && shiftDs.Tables[0].Rows.Count > 0)
                {
                    string shiftType = shiftDs.Tables[0].Rows[0]["shiftType"].ToString();
                    return $"{Convert.ToDateTime(date):yyyy-MM-dd}: {shiftType}";
                }
            }
            return "No Shift";
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string requestId = Request.QueryString["requestId"];
            int scheduleId = Convert.ToInt32(lblScheduleIdValue.Text); // Assuming lblScheduleIdValue contains the schedule ID

            int result = sms.UpdateSchedule(
                scheduleId,
                Convert.ToDateTime(lblMondayShift.Text.Split(':')[0]), lblMondayShift.Text.Contains("No Shift") ? (int?)null : GetShiftId(lblMondayShift.Text),
                Convert.ToDateTime(lblTuesdayShift.Text.Split(':')[0]), lblTuesdayShift.Text.Contains("No Shift") ? (int?)null : GetShiftId(lblTuesdayShift.Text),
                Convert.ToDateTime(lblWednesdayShift.Text.Split(':')[0]), lblWednesdayShift.Text.Contains("No Shift") ? (int?)null : GetShiftId(lblWednesdayShift.Text),
                Convert.ToDateTime(lblThursdayShift.Text.Split(':')[0]), lblThursdayShift.Text.Contains("No Shift") ? (int?)null : GetShiftId(lblThursdayShift.Text),
                Convert.ToDateTime(lblFridayShift.Text.Split(':')[0]), lblFridayShift.Text.Contains("No Shift") ? (int?)null : GetShiftId(lblFridayShift.Text),
                Convert.ToDateTime(lblSaturdayShift.Text.Split(':')[0]), lblSaturdayShift.Text.Contains("No Shift") ? (int?)null : GetShiftId(lblSaturdayShift.Text),
                Convert.ToDateTime(lblSundayShift.Text.Split(':')[0]), lblSundayShift.Text.Contains("No Shift") ? (int?)null : GetShiftId(lblSundayShift.Text)
            );

            if (result > 0)
            {
                rms.UpdateRequestStatus(Convert.ToInt32(requestId), "Approved", DateTime.Now, txtSupervisorComment.Text);
                Response.Redirect("SupervisorViewRequests.aspx");
            }
            else
            {
                lblMessage.Text = "Error updating the schedule.";
                lblMessage.CssClass = "common-input-label-error";
                lblMessage.Visible = true;
            }
        }

        private int GetShiftId(string shiftDetail)
        {
            string shiftType = shiftDetail.Split(':')[1].Trim();
            DataSet shiftDs = shms.GetShiftByType(shiftType);
            if (shiftDs != null && shiftDs.Tables.Count > 0 && shiftDs.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(shiftDs.Tables[0].Rows[0]["id"]);
            }
            return 0;
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            string requestId = Request.QueryString["requestId"];
            rms.UpdateRequestStatus(Convert.ToInt32(requestId), "Rejected", DateTime.Now, txtSupervisorComment.Text);
            Response.Redirect("SupervisorViewRequests.aspx");
        }

        protected void btnViewProofDocument_Click(object sender, EventArgs e)
        {
            string requestId = Request.QueryString["requestId"];
            if (!string.IsNullOrEmpty(requestId))
            {
                Response.Redirect($"ViewDocument.aspx?requestId={requestId}");
            }
        }
    }
}