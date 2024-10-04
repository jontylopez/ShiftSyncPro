using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WorkerMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if session variables are set
                if (Session["UserID"] != null && Session["LoggedInUser"] != null && Session["UserRole"] != null)
                {
                    lblUserId.Text = Session["UserID"].ToString();
                    lblWelcome.Text = Session["LoggedInUser"].ToString();
                    lblUserRole.Text = Session["UserRole"].ToString();

                    if (Session["SelectedDateTime"] != null)
                    {
                        DateTime selectedDateTime = (DateTime)Session["SelectedDateTime"];
                        lblDate.Text = selectedDateTime.ToString("yyyy-MM-dd");
                        lblTime.Text = selectedDateTime.ToString("HH:mm");
                    }

                    // Debugging logs
                    Console.WriteLine($"UserID: {Session["UserID"]}");
                    Console.WriteLine($"LoggedInUser: {Session["LoggedInUser"]}");
                    Console.WriteLine($"UserRole: {Session["UserRole"]}");
                    Console.WriteLine($"SelectedDateTime: {Session["SelectedDateTime"]}");
                }
                else
                {
                    Console.WriteLine("Session is invalid or expired. Redirecting to login.");
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}