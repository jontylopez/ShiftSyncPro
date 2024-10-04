using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        localhostRequest.RequestManagementService rms = new localhostRequest.RequestManagementService();
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService(); // Add user service reference

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPendingRequests();
            }
        }

        private void LoadPendingRequests()
        {
            // Get all pending requests
            DataSet requestDs = rms.GetRequestsByStatus("Pending");

            // Create a new DataTable to store filtered requests
            DataTable filteredRequests = requestDs.Tables[0].Clone(); // Clone the structure of the original table

            // Iterate through each request and check the user's role
            foreach (DataRow requestRow in requestDs.Tables[0].Rows)
            {
                int userId = Convert.ToInt32(requestRow["userId"]);

                // Get the user information by userId
                DataSet userDs = ums.GetUserById(userId);

                if (userDs != null && userDs.Tables[0].Rows.Count > 0)
                {
                    DataRow userRow = userDs.Tables[0].Rows[0];
                    string userRole = userRow["uRole"].ToString();

                    // If the user's role is 'wkr', add the request to the filteredRequests table
                    if (userRole == "wkr")
                    {
                        filteredRequests.ImportRow(requestRow);
                    }
                }
            }

            // Bind the filtered requests to the GridView
            gvPendingRequests.DataSource = filteredRequests;
            gvPendingRequests.DataBind();
        }

        protected void gvPendingRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRequest")
            {
                // Get the selected Request ID
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gvPendingRequests.Rows[index];
                string requestId = selectedRow.Cells[0].Text;

                // Redirect to the detailed view page, passing the requestId as a query string parameter
                Response.Redirect($"SupervisorRequestViewForm.aspx?requestId={requestId}");
            }
        }
    }
}