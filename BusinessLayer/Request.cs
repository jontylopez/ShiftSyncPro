using DataLayer;
using System;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLayer
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }

        
        public int? Day1ShiftId { get; set; }
        public DateTime? Day1Date { get; set; }
        public int? Day2ShiftId { get; set; }
        public DateTime? Day2Date { get; set; }
        public int? Day3ShiftId { get; set; }
        public DateTime? Day3Date { get; set; }
        public int? Day4ShiftId { get; set; }
        public DateTime? Day4Date { get; set; }
        public int? Day5ShiftId { get; set; }
        public DateTime? Day5Date { get; set; }
        public int? Day6ShiftId { get; set; }
        public DateTime? Day6Date { get; set; }
        public int? Day7ShiftId { get; set; }
        public DateTime? Day7Date { get; set; }

        public string Status { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string WorkerComment { get; set; } 
        public string SupervisorComment { get; set; } 
        public byte[] ProofDocument { get; set; }
        public string ProofDocumentType { get; set; }

        // Method to add a new weekly request
        public int AddWeeklyRequest()
        {
            string sql = @"INSERT INTO tblRequest 
               (userId, scheduleId, day1ShiftId, day1Date, 
                day2ShiftId, day2Date, day3ShiftId, day3Date, 
                day4ShiftId, day4Date, day5ShiftId, day5Date, 
                day6ShiftId, day6Date, day7ShiftId, day7Date, 
                status, requestDate, workerComment, supervisorComment, proofDocument, proofDocumentType) 
               VALUES 
               (@userId, @scheduleId, @day1ShiftId, @day1Date, 
                @day2ShiftId, @day2Date, @day3ShiftId, @day3Date, 
                @day4ShiftId, @day4Date, @day5ShiftId, @day5Date, 
                @day6ShiftId, @day6Date, @day7ShiftId, @day7Date, 
                @status, @requestDate, @workerComment, @supervisorComment, @proofDocument, @proofDocumentType)";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@userId", UserId);
                cmd.Parameters.AddWithValue("@scheduleId", ScheduleId);
                cmd.Parameters.AddWithValue("@day1ShiftId", (object)Day1ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day1Date", (object)Day1Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day2ShiftId", (object)Day2ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day2Date", (object)Day2Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day3ShiftId", (object)Day3ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day3Date", (object)Day3Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day4ShiftId", (object)Day4ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day4Date", (object)Day4Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day5ShiftId", (object)Day5ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day5Date", (object)Day5Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day6ShiftId", (object)Day6ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day6Date", (object)Day6Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day7ShiftId", (object)Day7ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day7Date", (object)Day7Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@status", Status);
                cmd.Parameters.AddWithValue("@requestDate", (object)RequestDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@workerComment", (object)WorkerComment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@supervisorComment", (object)SupervisorComment ?? DBNull.Value);

                // Check if proofDocument is null or not
                if (ProofDocument != null)
                {
                    cmd.Parameters.Add("@proofDocument", SqlDbType.VarBinary).Value = ProofDocument;
                    cmd.Parameters.AddWithValue("@proofDocumentType", ProofDocumentType); // Insert the document type
                }
                else
                {
                    cmd.Parameters.AddWithValue("@proofDocument", DBNull.Value);
                    cmd.Parameters.AddWithValue("@proofDocumentType", DBNull.Value); // Insert null for document type if no file is uploaded
                }

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to update the status, change date, and supervisor's comment
        public int UpdateRequestStatus(int requestId, string status, DateTime changeDate, string supervisorComment = null)
        {
            string sql = @"UPDATE tblRequest 
                           SET status = @status, changeDate = @changeDate, supervisorComment = @supervisorComment 
                           WHERE id = @requestId";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@requestId", requestId);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@changeDate", changeDate);
                cmd.Parameters.AddWithValue("@supervisorComment", (object)supervisorComment ?? DBNull.Value);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Method to get all requests for a specific user
        public DataSet GetRequestsByUser(int userId)
        {
            string sql = "SELECT * FROM tblRequest WHERE userId = @userId";

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

        // Method to get all requests for a specific schedule
        public DataSet GetRequestsBySchedule(int scheduleId)
        {
            string sql = "SELECT * FROM tblRequest WHERE scheduleId = @scheduleId";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@scheduleId", scheduleId);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        // Method to get requests by status
        public DataSet GetRequestsByStatus(string status)
        {
            string sql = "SELECT * FROM tblRequest WHERE status = @status";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@status", status);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        // Method to get request by ID
        public DataSet GetRequestById(int id)
        {
            string sql = "SELECT * FROM tblRequest WHERE id = @id";

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
        public DataSet GetProofDocumentById(int id)
        {
            string sql = @"SELECT proofDocument, proofDocumentType FROM tblRequest WHERE id = @id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                con.Open();
                adapter.Fill(ds);
                return ds;
            }
        }
    }
}
