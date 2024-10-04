using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userId"] != null)
                {
                    int userId = Convert.ToInt32(Request.QueryString["userId"]);
                    LoadUserDetails(userId);
                }
                else
                {
                    lblMessage.Text = "Invalid user ID.";
                }
            }
        }

        private void LoadUserDetails(int userId)
        {
            try
            {

                DataSet ds = ums.FindUser(userId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    txtUserId.Text = dr["id"].ToString();
                    txtEmpId.Text = dr["empId"].ToString();
                    txtDepId.Text = dr["depId"].ToString();
                    txtPosition.Text = dr["position"].ToString();
                    txtUserName.Text = dr["uName"].ToString();
                    txtFullName.Text = dr["fullName"].ToString();
                    txtTel.Text = dr["empTel"].ToString();
                    txtMail.Text = dr["empMail"].ToString();
                    ddlGroup.SelectedValue = dr["empGroup"].ToString();
                    ddlRole.SelectedValue = dr["uRole"].ToString();
                }
                else
                {
                    lblMessage.Text = "User not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error loading user details: {ex.Message}";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(txtUserId.Text);
                string fullName = txtFullName.Text.Trim();
                string empTel = txtTel.Text.Trim();
                string empMail = txtMail.Text.Trim();
                string empGroup = ddlGroup.SelectedValue;
                string userRole = ddlRole.SelectedValue;

                int result = ums.UpdateUserDetails(userId, fullName, empTel, empMail, empGroup, userRole);

                if (result > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "requestSuccess", "alert('Profile updated successfully.');window.location.href='AdminManageUser.aspx';", true);
                }
                else
                {
                    lblMessage.Text = "Failed to update user details.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error updating user details: {ex.Message}";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminManageUser.aspx");
        }
    }
}