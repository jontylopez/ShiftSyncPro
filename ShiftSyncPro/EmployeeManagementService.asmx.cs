using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessLayer;

namespace ShiftSyncPro
{
    /// <summary>
    /// Summary description for EmployeeManagementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EmployeeManagementService : System.Web.Services.WebService
    {

        [WebMethod]
        public int InsertEmployee(int id, int nic, int epfNo, int depId, string position, string fullName, string empAddress, int empTel, string empMail, DateTime dob, DateTime dateOfJoining, string empStatus, string emergencyContactName, string emergencyContactNumber, bool emailVerified, string photoUrl, string roleDescription, string departmentName, decimal salary)
        {
            try
            {
                Employee employee = new Employee
                {
                    ID = id,
                    NIC = nic,
                    EPFNo = epfNo,
                    DepId = depId,
                    Position = position,
                    FullName = fullName,
                    EmpAddress = empAddress,
                    EmpTel = empTel,
                    EmpMail = empMail,
                    DOB = dob,
                    DateOfJoining = dateOfJoining,
                    EmpStatus = empStatus,
                    EmergencyContactName = emergencyContactName,
                    EmergencyContactNumber = emergencyContactNumber,
                    EmailVerified = emailVerified,
                    PhotoUrl = photoUrl,
                    RoleDescription = roleDescription,
                    DepartmentName = departmentName,
                    Salary = salary
                };
                return employee.Insert();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int UpdateEmployee(Employee employee)
        {
            try
            {
                return employee.Update();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int DeleteEmployee(int id)
        {
            try
            {
                Employee employee = new Employee { ID = id };
                return employee.Delete();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet FindEmployee(int id)
        {
            try
            {
                Employee employee = new Employee { ID = id };
                return employee.Find();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet GetAllEmployees()
        {
            try
            {
                Employee employee = new Employee();
                return employee.ViewAll();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet GetAllEmployeesNotInUserTable()
        {
            try
            {
                Employee employee = new Employee();

                return employee.GetAllEmployeesNotInUserTable();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error: " + ex.Message, ex);
            }
        }
    }
}
