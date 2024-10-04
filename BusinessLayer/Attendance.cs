using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class Attendance
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string InTime { get; set; } // New column
        public string OutTime { get; set; } // New column
        public string AttStatus { get; set; }
        public string WorkerComment { get; set; } // New column
        public string SupervisorComment { get; set; } // New column

        // Method to insert a new attendance batch
        public int InsertAttendanceBatch(List<Attendance> attendanceList)
        {
            try
            {
                if (attendanceList == null || attendanceList.Count == 0)
                {
                    throw new ArgumentException("Attendance list is empty.");
                }

                string sql = @"INSERT INTO tblAttendance 
                               (scheduleId, userId, weekStartDate, date, startTime, endTime, 
                                inTime, outTime, attStatus, workerComment, supervisorComment) 
                               VALUES ";

                List<string> valueList = new List<string>();
                using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    int paramCounter = 0;

                    foreach (var att in attendanceList)
                    {
                        string paramSet = $@"(@scheduleId{paramCounter}, @userId{paramCounter}, 
                                              @weekStartDate{paramCounter}, @date{paramCounter}, 
                                              @startTime{paramCounter}, @endTime{paramCounter}, 
                                              @inTime{paramCounter}, @outTime{paramCounter}, 
                                              @attStatus{paramCounter}, @workerComment{paramCounter}, 
                                              @supervisorComment{paramCounter})";

                        valueList.Add(paramSet);
                        cmd.Parameters.AddWithValue($"@scheduleId{paramCounter}", att.ScheduleId);
                        cmd.Parameters.AddWithValue($"@userId{paramCounter}", att.UserId);
                        cmd.Parameters.AddWithValue($"@weekStartDate{paramCounter}", att.WeekStartDate);
                        cmd.Parameters.AddWithValue($"@date{paramCounter}", att.Date);
                        cmd.Parameters.AddWithValue($"@startTime{paramCounter}", att.StartTime);
                        cmd.Parameters.AddWithValue($"@endTime{paramCounter}", att.EndTime);
                        cmd.Parameters.AddWithValue($"@inTime{paramCounter}", (object)att.InTime ?? DBNull.Value);
                        cmd.Parameters.AddWithValue($"@outTime{paramCounter}", (object)att.OutTime ?? DBNull.Value);
                        cmd.Parameters.AddWithValue($"@attStatus{paramCounter}", att.AttStatus);
                        cmd.Parameters.AddWithValue($"@workerComment{paramCounter}", (object)att.WorkerComment ?? DBNull.Value);
                        cmd.Parameters.AddWithValue($"@supervisorComment{paramCounter}", (object)att.SupervisorComment ?? DBNull.Value);

                        paramCounter++;
                    }

                    sql += string.Join(",", valueList);
                    cmd.CommandText = sql;

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting attendance batch: " + ex.Message);
                throw;
            }
        }

        // Method to update the in time for an attendance record
        public int UpdateInTime(int attendanceId, string newInTime)
        {
            string sql = "UPDATE tblAttendance SET inTime = @inTime WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@inTime", newInTime);
                cmd.Parameters.AddWithValue("@id", attendanceId);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to update the out time for an attendance record
        public int UpdateOutTime(int attendanceId, string newOutTime)
        {
            string sql = "UPDATE tblAttendance SET outTime = @outTime WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@outTime", newOutTime);
                cmd.Parameters.AddWithValue("@id", attendanceId);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to update the worker's comment
        public int UpdateWorkerComment(int attendanceId, string workerComment)
        {
            string sql = "UPDATE tblAttendance SET workerComment = @workerComment WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@workerComment", workerComment);
                cmd.Parameters.AddWithValue("@id", attendanceId);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to update the supervisor's comment
        public int UpdateSupervisorComment(int attendanceId, string supervisorComment)
        {
            string sql = "UPDATE tblAttendance SET supervisorComment = @supervisorComment WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@supervisorComment", supervisorComment);
                cmd.Parameters.AddWithValue("@id", attendanceId);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to update the attendance status
        public int UpdateAttStatus(int attendanceId, string newStatus)
        {
            string sql = "UPDATE tblAttendance SET attStatus = @attStatus WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@attStatus", newStatus);
                cmd.Parameters.AddWithValue("@id", attendanceId);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to retrieve all attendance records
        public DataSet GetAllAttendance()
        {
            string sql = "SELECT * FROM tblAttendance";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        // Method to retrieve a specific attendance record by its ID
        public DataSet GetAttendanceById(int attendanceId)
        {
            string sql = "SELECT * FROM tblAttendance WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@id", attendanceId);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        // Method to retrieve all attendance records for a specific user
        public DataSet GetAttendanceByUser(int userId)
        {
            string sql = "SELECT * FROM tblAttendance WHERE userId = @userId";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@userId", userId);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        // Method to retrieve attendance records by the week start date
        public DataSet GetAttendanceByWeekStartDate(DateTime weekStartDate)
        {
            string sql = "SELECT * FROM tblAttendance WHERE weekStartDate = @weekStartDate";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@weekStartDate", weekStartDate);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        // Method to get attendance by specific date
        public DataSet GetAttendanceByDate(DateTime selectedDate)
        {
            string sql = "SELECT * FROM tblAttendance WHERE date = @date";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@date", selectedDate);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        // Method to insert in-time for a specific user and date
        public int InsertInTime(int userId, DateTime date, string inTime)
        {
            string sql = "UPDATE tblAttendance SET inTime = @inTime WHERE userId = @userId AND date = @date";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@inTime", inTime);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@date", date);

                con.Open();

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Success
                        return rowsAffected;
                    }
                    else
                    {
                        // No rows affected, meaning the record wasn't found
                        Console.WriteLine($"No attendance record found for userId: {userId} on date: {date.ToString("yyyy-MM-dd")}");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating In-Time: {ex.Message}");
                    throw;
                }
            }
        }
        public int InsertOutTime(int userId, DateTime date, string outTime)
        {
            string sql = "UPDATE tblAttendance SET outTime = @outTime WHERE userId = @userId AND date = @date";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@outTime", outTime);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@date", date);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

    }
}
