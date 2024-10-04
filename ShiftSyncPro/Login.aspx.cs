using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class Login : System.Web.UI.Page
    {
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            int userID = ums.GetUserID(userName, password);
            string userRole = ums.IsValidUser(userName, password);

            string selectedDate = txtDate.Text;
            string selectedTime = txtTime.Text;

            if (!string.IsNullOrEmpty(selectedDate) && !string.IsNullOrEmpty(selectedTime))
            {
                DateTime selectedDateTime;
                if (DateTime.TryParse(selectedDate + " " + selectedTime, out selectedDateTime))
                {
                    Session["SelectedDateTime"] = selectedDateTime;
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Invalid date or time format.";
                    return;
                }
            }
            else
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Please select a date and time.";
                return;
            }

            if (userRole != null)
            {
                // Set session variables
                Session["UserID"] = userID;
                Session["LoggedInUser"] = userName;
                Session["UserRole"] = userRole;

                // Debugging logs
                Console.WriteLine($"UserID: {Session["UserID"]}");
                Console.WriteLine($"LoggedInUser: {Session["LoggedInUser"]}");
                Console.WriteLine($"UserRole: {Session["UserRole"]}");
                Console.WriteLine($"SelectedDateTime: {Session["SelectedDateTime"]}");

                // Redirect based on role
                switch (userRole.ToLower())
                {
                    case "adm":
                        Response.Redirect("AdminHome.aspx");
                        break;
                    case "sup":
                        Response.Redirect("SupervisorHome.aspx");
                        break;
                    case "wkr":
                        Response.Redirect("WorkerHome.aspx");
                        break;
                    case "hrm":
                        Response.Redirect("WorkerHome.aspx");
                        break;
                    default:
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Unauthorized access.";
                        break;
                }
            }
            else
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "Invalid username or password.";
            }
        }
    }
}