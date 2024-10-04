using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class SupervisorMaster : System.Web.UI.MasterPage
    {
        public DateTime CurrentDate
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(lblDate.Text, out date))
                {
                    return date;
                }
                return DateTime.Now; // Default value
            }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                TimeSpan time;
                if (TimeSpan.TryParse(lblTime.Text, out time))
                {
                    return time;
                }
                return DateTime.Now.TimeOfDay; // Default value
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}