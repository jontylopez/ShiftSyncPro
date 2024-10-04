using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;

namespace ShiftSyncPro
{
    /// <summary>
    /// Summary description for UserManagementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserManagementService : System.Web.Services.WebService
    {

        [WebMethod]
        public int InsertUser(int empId, int depId, string position, string fullName, string empTel, string empMail, string empGroup, string uRole, string uName, string pWord)
        {
            try
            {
                User user = new User
                {
                    EmpId = empId,
                    DepId = depId,
                    Position = position,
                    FullName = fullName,
                    EmpTel = empTel,
                    EmpMail = empMail,
                    EmpGroup = empGroup,
                    URole = uRole,
                    UName = uName,
                    Password = pWord
                };
                return user.Insert();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int UpdateUserDetails(int id, string fullName, string empTel, string empMail, string empGroup, string uRole)
        {
            try
            {
                User user = new User();
                return user.UpdateUser(id, fullName, empTel, empMail, empGroup, uRole);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int DeleteUser(int id)
        {
            try
            {
                User user = new User { ID = id };
                return user.Delete();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet FindUser(int id)
        {
            try
            {
                User user = new User { ID = id };
                return user.Find();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet findAll()
        {
            try
            {
                User user = new User();
                return user.ViewAll();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public string IsValidUser(string username, string password)
        {
            User user = new User();
            return user.IsValidUser(username, password);
        }
        [WebMethod]
        public int GetUserID(string uName, string pWord)
        {
            User user = new User();
            return user.GetUserID(uName, pWord);
        }
        [WebMethod]
        public DataSet GetUsersByGroup(string groupName)
        {
            try
            {
                User user = new User();
                return user.GetUserByGroup(groupName);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
        [WebMethod]
        public DataSet GetUserById(int id)
        {
            try
            {
                User user = new User();
                return user.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

    }
}
