using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {


        public int ID { get; set; }
        public int EmpId { get; set; }
        public int DepId { get; set; }
        public string Position { get; set; }
        public string FullName { get; set; }
        public string EmpTel { get; set; }
        public string EmpMail { get; set; }
        public string EmpGroup { get; set; }
        public string URole { get; set; }
        public string UName { get; set; }
        public string Password { get; set; }


        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public int Insert()
        {
            string hashedPassword = HashPassword(this.Password);
            string sql = $"INSERT INTO tblUser (empId, depId, position, fullName, empTel, empMail, empGroup, uRole, uName, pWord) " +
                         $"VALUES ({EmpId}, {DepId}, '{Position}', '{FullName}', '{EmpTel}', '{EmpMail}', '{EmpGroup}', '{URole}', '{UName}', '{hashedPassword}')";
            return DatabaseOperations.ExecuteQuery(sql);
        }


        public int UpdateUser(int id, string fullName, string empTel, string empMail, string empGroup, string uRole)
        {
            // SQL query to update only the specified fields
            string sql = "UPDATE tblUser SET fullName=@fullName, empTel=@empTel, empMail=@empMail, empGroup=@empGroup, uRole=@uRole WHERE id=@id";

            // Create the SQL connection and command objects
            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                // Add parameters to the command to prevent SQL injection
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fullName", fullName ?? (object)DBNull.Value); // Use DBNull if fullName is null
                cmd.Parameters.AddWithValue("@empTel", empTel ?? (object)DBNull.Value); // Use DBNull if empTel is null
                cmd.Parameters.AddWithValue("@empMail", empMail ?? (object)DBNull.Value); // Use DBNull if empMail is null
                cmd.Parameters.AddWithValue("@empGroup", empGroup ?? (object)DBNull.Value); // Use DBNull if empGroup is null
                cmd.Parameters.AddWithValue("@uRole", uRole ?? (object)DBNull.Value); // Use DBNull if uRole is null

                // Open the connection and execute the query
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public int Delete()
        {
            string sql = $"DELETE FROM tblUser WHERE id={ID}";
            return DatabaseOperations.ExecuteQuery(sql);
        }


        public DataSet Find()
        {
            string sql = $"SELECT * FROM tblUser WHERE id={ID}";
            return DatabaseOperations.SelectQuery(sql);
        }


        public DataSet ViewAll()
        {
            string sql = "SELECT * FROM tblUser";
            return DatabaseOperations.SelectQuery(sql);
        }


        public string IsValidUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
                {
                    con.Open();
                    string query = "SELECT uRole FROM tblUser WHERE uName = @Username AND pWord = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                return "An unexpected error occurred while validating the user.";
            }
        }


        public string GetUserRole(string uName, string pWord)
        {
            string hashedPassword = HashPassword(pWord);
            string sql = "SELECT uRole FROM tblUser WHERE uName=@uName AND pWord=@hashedPassword";
            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@uName", uName);
                cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                con.Open();
                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }
        public int GetUserID(string uName, string pWord)
        {
            string hashedPassword = User.HashPassword(pWord);
            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            {
                con.Open();
                string query = "SELECT id FROM tblUser WHERE uName = @Username AND pWord = @Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", uName);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int userId))
                    {
                        return userId;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
        public DataSet GetUserByGroup(string groupName)
        {
            string sql = "SELECT * FROM tblUser WHERE empGroup = @groupName";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@groupName", groupName);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    con.Open();

                    da.Fill(ds);
                }
            }
            return ds;
        }
        public DataSet GetUserById(int id)
        {
            string sql = "SELECT * FROM tblUser WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@id", id);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
    }
}
