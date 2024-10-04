using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm17 : System.Web.UI.Page
    {
        localhostSchedule.ScheduleManagementService sms = new localhostSchedule.ScheduleManagementService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optional
            }
        }
        protected void btnViewSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessage.Visible = false;
                if (DateTime.TryParse(txtStartDate.Text, out DateTime selectedStartDate))
                {
                    DataSet ds = sms.GetScheduleByStartDate(selectedStartDate);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (ds.Tables[0].Columns.Contains("timeCreate") && row["timeCreate"] != DBNull.Value)
                            {
                                string timeCreatedString = row["timeCreate"].ToString();
                                if (!string.IsNullOrEmpty(timeCreatedString))
                                {
                                    try
                                    {
                                        TimeSpan timeCreated = TimeSpan.ParseExact(timeCreatedString, @"hh\:mm\:ss", null);
                                        Console.WriteLine($"Time Created: {timeCreated}");
                                    }
                                    catch (FormatException ex)
                                    {
                                        lblErrorMessage.Visible = true;
                                        lblErrorMessage.Text = "Error converting time format: " + ex.Message;
                                    }
                                }
                                else
                                {
                                    lblErrorMessage.Visible = true;
                                    lblErrorMessage.Text = "timeCreate value is empty.";
                                }
                            }
                            else
                            {
                                lblErrorMessage.Visible = true;
                                lblErrorMessage.Text = "timeCreate column is missing or has null values.";
                            }
                        }
                        gvSchedule.DataSource = ds;
                        gvSchedule.DataBind();
                    }
                    else
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "No schedules found for the selected date.";
                    }
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Please enter a valid start date.";
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Error loading schedule: " + ex.Message;
            }
        }
    }
}