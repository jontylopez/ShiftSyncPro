using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Employee
    {
        public int ID { get; set; }
        public int NIC { get; set; }
        public int EPFNo { get; set; }
        public int DepId { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; }
        public string EmpAddress { get; set; }
        public int EmpTel { get; set; }
        public string EmpMail { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string EmpStatus { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public bool EmailVerified { get; set; }
        public string PhotoUrl { get; set; }
        public string RoleDescription { get; set; }
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public int Insert()
        {
            string sql = $"INSERT INTO tblEmployee (nic, epfNo, depId, position, fullName, empAddress, empTel, empMail, dob, dateOfJoining, empStatus, " +
                         $"emergencyContactName, emergencyContactNumber, emailVerified, photoUrl, roleDescription, departmentName, salary, createdAt, updatedAt) VALUES " +
                         $"({NIC}, {EPFNo}, {DepId}, '{Position}', '{FullName}', '{EmpAddress}', {EmpTel}, '{EmpMail}', '{DOB}', '{DateOfJoining}', '{EmpStatus}', " +
                         $"'{EmergencyContactName}', '{EmergencyContactNumber}', '{EmailVerified}', '{PhotoUrl}', '{RoleDescription}', '{DepartmentName}', {Salary}, GETDATE(), GETDATE())";
            return DatabaseOperations.ExecuteQuery(sql);
        }

        // Method to update an existing employee's data in the database
        public int Update()
        {
            string sql = $"UPDATE tblEmployee SET nic={NIC}, epfNo={EPFNo}, depId={DepId}, position='{Position}', fullName='{FullName}', empAddress='{EmpAddress}', " +
                         $"empTel={EmpTel}, empMail='{EmpMail}', dob='{DOB}', dateOfJoining='{DateOfJoining}', empStatus='{EmpStatus}', emergencyContactName='{EmergencyContactName}', " +
                         $"emergencyContactNumber='{EmergencyContactNumber}', emailVerified='{EmailVerified}', photoUrl='{PhotoUrl}', roleDescription='{RoleDescription}', " +
                         $"departmentName='{DepartmentName}', salary={Salary}, updatedAt=GETDATE() WHERE id={ID}";
            return DatabaseOperations.ExecuteQuery(sql);
        }

        // Method to delete an employee from the database
        public int Delete()
        {
            string sql = $"DELETE FROM tblEmployee WHERE id={ID}";
            return DatabaseOperations.ExecuteQuery(sql);
        }

        // Method to find a specific employee by ID
        public DataSet Find()
        {
            string sql = $"SELECT * FROM tblEmployee WHERE id={ID}";
            return DatabaseOperations.SelectQuery(sql);
        }

        // Method to retrieve all employees from the database
        public DataSet ViewAll()
        {
            string sql = "SELECT * FROM tblEmployee";
            return DatabaseOperations.SelectQuery(sql);
        }
        public DataSet GetAllEmployeesNotInUserTable()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
                {
                    string query = @"SELECT e.* FROM tblEmployee e 
                                     LEFT JOIN tblUser u ON e.ID = u.EmpId 
                                     WHERE u.EmpId IS NULL";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving employees not in user table.", ex);
            }
        }
    }
}
