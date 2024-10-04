using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers(); // Load all users on first load
            }
        }
        private void LoadUsers()
        {
            try
            {
                DataSet ds = ums.findAll(); // Fetch all users using the web service method
                gvUsers.DataSource = ds;
                gvUsers.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error loading users: {ex.Message}";
            }
        }
        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            LoadUsers(); // Reload the GridView with EditIndex set
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            LoadUsers(); // Cancel edit mode and reload the GridView
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // We don't need to implement this for the current requirement
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TempAction")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"AdminUpdateUserForm.aspx?userId={userId}"); // Redirect to the update page
            }
        }
    }
}