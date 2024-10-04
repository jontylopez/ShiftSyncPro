using DataLayer;
using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class Shift
    {
        public int Id { get; set; }
        public string ShiftType { get; set; }
        public string StartTime { get; set; } // Changed to string
        public string EndTime { get; set; } // Changed to string

        // Method to add a new shift
        public int AddShift()
        {
            string sql = @"INSERT INTO tblShift (shiftType, startTime, endTime) 
                           VALUES (@shiftType, @startTime, @endTime)";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@shiftType", ShiftType);
                cmd.Parameters.AddWithValue("@startTime", StartTime); // Using string
                cmd.Parameters.AddWithValue("@endTime", EndTime); // Using string

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to update an existing shift
        public int UpdateShift()
        {
            string sql = @"UPDATE tblShift SET 
                           shiftType = @shiftType, 
                           startTime = @startTime, 
                           endTime = @endTime 
                           WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@shiftType", ShiftType);
                cmd.Parameters.AddWithValue("@startTime", StartTime); // Using string
                cmd.Parameters.AddWithValue("@endTime", EndTime); // Using string

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to delete a shift by its ID
        public int DeleteShift(int id)
        {
            string sql = "DELETE FROM tblShift WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to retrieve all shifts
        public DataSet GetAllShifts()
        {
            string sql = "SELECT * FROM tblShift";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        // Method to retrieve a shift by its ID
        public DataSet GetShiftById(int id)
        {
            string sql = "SELECT * FROM tblShift WHERE id = @id";

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

        // Method to retrieve the start time of a shift by its ID
        public string GetStartTimeById(int id)
        {
            string sql = "SELECT startTime FROM tblShift WHERE id = @id";
            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return result.ToString(); // Return the string value directly
                }
                else
                {
                    throw new Exception("Shift not found.");
                }
            }
        }

        // Method to retrieve the end time of a shift by its ID
        public string GetEndTimeById(int id)
        {
            string sql = "SELECT endTime FROM tblShift WHERE id = @id";
            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return result.ToString(); // Return the string value directly
                }
                else
                {
                    throw new Exception("Shift not found.");
                }
            }
        }
        // Method to retrieve a shift by its type
        public DataSet GetShiftByType(string shiftType)
        {
            string sql = "SELECT * FROM tblShift WHERE shiftType = @shiftType";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@shiftType", shiftType); // Bind the shift type parameter

                DataSet ds = new DataSet();
                da.Fill(ds); // Fill the DataSet with the result of the query
                return ds;
            }
        }

    }
}
