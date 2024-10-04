using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShiftSyncPro
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        localhostUser.UserManagementService ums = new localhostUser.UserManagementService();
        localhostEmployee.EmployeeManagementService ems = new localhostEmployee.EmployeeManagementService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
            }
        }

        private void LoadEmployees()
        {
            try
            {
                var allEmployees = ems.GetAllEmployees();
                var allUsers = ums.findAll();

                DataTable employees = allEmployees.Tables[0];
                DataTable users = allUsers.Tables[0];

                var filteredEmployees = from employee in employees.AsEnumerable()
                                        where !users.AsEnumerable().Any(user => user.Field<int>("EmpId") == employee.Field<int>("ID"))
                                        select employee;

                if (filteredEmployees.Any())
                {
                    ddlEmployees.DataSource = filteredEmployees.CopyToDataTable();
                    ddlEmployees.DataTextField = "FullName";
                    ddlEmployees.DataValueField = "ID";
                    ddlEmployees.DataBind();
                }

                ddlEmployees.Items.Insert(0, new ListItem("--Select Employee--", ""));
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error loading employees: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployees.SelectedIndex > 0)
            {
                int empId = Convert.ToInt32(ddlEmployees.SelectedValue);

                DataSet ds = ems.FindEmployee(empId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    txtEmpId.Text = dr["ID"].ToString();
                    txtDepId.Text = dr["DepId"].ToString();
                    txtPossition.Text = dr["Position"].ToString();
                    txtName.Text = dr["FullName"].ToString();
                    txtTel.Text = dr["EmpTel"].ToString();
                    txtMail.Text = dr["EmpMail"].ToString();
                    txtUserName.Text = dr["ID"].ToString();
                    txtPassword.Text = "aaaa@1111";

                    int groupId = empId % 3;

                    if (txtDepId.Text == "1")
                    {
                        ddlGroup.Enabled = false;
                    }
                    else
                    {
                        ddlGroup.Enabled = true;
                        ddlGroup.SelectedIndex = groupId;
                    }
                }
                else
                {
                    ClearForm();
                }
            }
            else
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtEmpId.Text = "";
            txtDepId.Text = "";
            txtPossition.Text = "";
            txtName.Text = "";
            txtTel.Text = "";
            txtMail.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            ddlGroup.SelectedIndex = -1;
            ddlRole.SelectedIndex = -1;
            lblMessage.Text = "";
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;

                if (ddlEmployees.SelectedIndex <= 0)
                {
                    lblMessage.Text = "Please select an employee.";
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    lblMessage.Text = "Please enter a username and password.";
                    return;
                }

                int empId = Convert.ToInt32(txtEmpId.Text);
                int depId = Convert.ToInt32(txtDepId.Text);
                string position = txtPossition.Text;
                string fullName = txtName.Text;
                string empTel = txtTel.Text;
                string empMail = txtMail.Text;
                string empGroup = ddlGroup.SelectedValue;
                string uRole = ddlRole.SelectedValue;
                string uName = txtUserName.Text;
                string password = txtPassword.Text;

                int result = ums.InsertUser(empId, depId, position, fullName, empTel, empMail, empGroup, uRole, uName, password);

                if (result > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "requestSuccess", "alert('Profile added successfully.');window.location.href='AdminAddUserForm.aspx';", true);
                    ClearForm();
                    LoadEmployees();
                }
                else
                {
                    lblMessage.Text = "Failed to add user.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"An error occurred: {ex.Message}";
            }
        }
    }
}
