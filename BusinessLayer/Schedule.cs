using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class Schedule
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GroupId { get; set; }
        public DateTime WeekStartDate { get; set; }

        public DateTime? Day1Date { get; set; }
        public int? Day1ShiftId { get; set; }
        public DateTime? Day2Date { get; set; }
        public int? Day2ShiftId { get; set; }
        public DateTime? Day3Date { get; set; }
        public int? Day3ShiftId { get; set; }
        public DateTime? Day4Date { get; set; }
        public int? Day4ShiftId { get; set; }
        public DateTime? Day5Date { get; set; }
        public int? Day5ShiftId { get; set; }
        public DateTime? Day6Date { get; set; }
        public int? Day6ShiftId { get; set; }
        public DateTime? Day7Date { get; set; }
        public int? Day7ShiftId { get; set; }

        public DateTime DayCreate { get; set; }
        public string TimeCreate { get; set; }

        public int AddScheduleToDb()
        {
            string sql = @"INSERT INTO tblSchedule 
                            (userId, groupId, weekStartDate, day1Date, day1ShiftId, day2Date, day2ShiftId, day3Date, day3ShiftId, day4Date, day4ShiftId, day5Date, day5ShiftId, day6Date, day6ShiftId, day7Date, day7ShiftId, dayCreate, timeCreate)
                            VALUES (@userId, @groupId, @weekStartDate, @day1Date, @day1ShiftId, @day2Date, @day2ShiftId, @day3Date, @day3ShiftId, @day4Date, @day4ShiftId, @day5Date, @day5ShiftId, @day6Date, @day6ShiftId, @day7Date, @day7ShiftId, @dayCreate, @timeCreate)";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@userId", UserId);
                cmd.Parameters.AddWithValue("@groupId", GroupId);
                cmd.Parameters.AddWithValue("@weekStartDate", WeekStartDate);
                cmd.Parameters.AddWithValue("@day1Date", (object)Day1Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day1ShiftId", (object)Day1ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day2Date", (object)Day2Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day2ShiftId", (object)Day2ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day3Date", (object)Day3Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day3ShiftId", (object)Day3ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day4Date", (object)Day4Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day4ShiftId", (object)Day4ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day5Date", (object)Day5Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day5ShiftId", (object)Day5ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day6Date", (object)Day6Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day6ShiftId", (object)Day6ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day7Date", (object)Day7Date ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@day7ShiftId", (object)Day7ShiftId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@dayCreate", DayCreate);
                cmd.Parameters.AddWithValue("@timeCreate", TimeCreate);

                //string timeString = "12:30:45"; // Custom format hh-mm-ss
                //TimeSpan timeSpan = TimeSpan.ParseExact(timeString, @"hh\:mm\:ss", null);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateSchedule()
        {
            string sql = @"UPDATE tblSchedule SET 
                     day1Date = @day1Date, 
                     day1ShiftId = @day1ShiftId,
                     day2Date = @day2Date, 
                     day2ShiftId = @day2ShiftId,
                     day3Date = @day3Date, 
                     day3ShiftId = @day3ShiftId,
                     day4Date = @day4Date, 
                     day4ShiftId = @day4ShiftId,
                     day5Date = @day5Date, 
                     day5ShiftId = @day5ShiftId,
                     day6Date = @day6Date, 
                     day6ShiftId = @day6ShiftId,
                     day7Date = @day7Date, 
                     day7ShiftId = @day7ShiftId
                     WHERE Id = @scheduleId";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@scheduleId", Id); // Schedule Id to update the correct record
                cmd.Parameters.AddWithValue("@day1Date", (object)Day1Date ?? DBNull.Value); // Day 1 date
                cmd.Parameters.AddWithValue("@day1ShiftId", (object)Day1ShiftId ?? DBNull.Value); // Day 1 shift ID
                cmd.Parameters.AddWithValue("@day2Date", (object)Day2Date ?? DBNull.Value); // Day 2 date
                cmd.Parameters.AddWithValue("@day2ShiftId", (object)Day2ShiftId ?? DBNull.Value); // Day 2 shift ID
                cmd.Parameters.AddWithValue("@day3Date", (object)Day3Date ?? DBNull.Value); // Day 3 date
                cmd.Parameters.AddWithValue("@day3ShiftId", (object)Day3ShiftId ?? DBNull.Value); // Day 3 shift ID
                cmd.Parameters.AddWithValue("@day4Date", (object)Day4Date ?? DBNull.Value); // Day 4 date
                cmd.Parameters.AddWithValue("@day4ShiftId", (object)Day4ShiftId ?? DBNull.Value); // Day 4 shift ID
                cmd.Parameters.AddWithValue("@day5Date", (object)Day5Date ?? DBNull.Value); // Day 5 date
                cmd.Parameters.AddWithValue("@day5ShiftId", (object)Day5ShiftId ?? DBNull.Value); // Day 5 shift ID
                cmd.Parameters.AddWithValue("@day6Date", (object)Day6Date ?? DBNull.Value); // Day 6 date
                cmd.Parameters.AddWithValue("@day6ShiftId", (object)Day6ShiftId ?? DBNull.Value); // Day 6 shift ID
                cmd.Parameters.AddWithValue("@day7Date", (object)Day7Date ?? DBNull.Value); // Day 7 date
                cmd.Parameters.AddWithValue("@day7ShiftId", (object)Day7ShiftId ?? DBNull.Value); // Day 7 shift ID

                con.Open();
                return cmd.ExecuteNonQuery(); // Execute the update
            }
        }


        public int DeleteSchedule(int Id)
        {
            string sql = "DELETE FROM tblSchedule WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public DataSet GetSchedulesByUser(int userId)
        {
            string sql = "SELECT * FROM tblSchedule WHERE userId = @userId";

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
        public DataSet GetSchedulesByGroupAndDateRange(string groupId, DateTime startDate, DateTime endDate)
        {
            string sql = @"SELECT * FROM tblSchedule 
                            WHERE groupId = @groupId 
                            AND weekStartDate BETWEEN @startDate AND @endDate";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@groupId", groupId);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        public DataSet GetScheduleById(int Id)
        {
            string sql = "SELECT * FROM tblSchedule WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public int UpdateShift(int scheduleId, int day, int newShiftId)
        {
            string sql = @"
            UPDATE tblSchedule
            SET 
                day1ShiftId = CASE WHEN @day = 1 THEN @newShiftId ELSE day1ShiftId END,
                day2ShiftId = CASE WHEN @day = 2 THEN @newShiftId ELSE day2ShiftId END,
                day3ShiftId = CASE WHEN @day = 3 THEN @newShiftId ELSE day3ShiftId END,
                day4ShiftId = CASE WHEN @day = 4 THEN @newShiftId ELSE day4ShiftId END,
                day5ShiftId = CASE WHEN @day = 5 THEN @newShiftId ELSE day5ShiftId END,
                day6ShiftId = CASE WHEN @day = 6 THEN @newShiftId ELSE day6ShiftId END,
                day7ShiftId = CASE WHEN @day = 7 THEN @newShiftId ELSE day7ShiftId END
            WHERE id = @scheduleId";


            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@scheduleId", scheduleId);
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@newShiftId", newShiftId);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public DataSet GetScheduleByStartDate(DateTime weekStartDate)
        {
            // Modify the SQL query to alias the 'id' column as 'ScheduleId'
            string sqlQuery = @"SELECT id AS ScheduleId, userId, groupId, weekStartDate, 
                        day1Date, day1ShiftId, day2Date, day2ShiftId, day3Date, day3ShiftId, 
                        day4Date, day4ShiftId, day5Date, day5ShiftId, day6Date, day6ShiftId, 
                        day7Date, day7ShiftId, dayCreate, timeCreate 
                        FROM tblSchedule 
                        WHERE weekStartDate = @weekStartDate";

            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@weekStartDate", weekStartDate);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            da.Fill(ds);
                        }
                    }
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string timeCreateString = row["timeCreate"].ToString();
                    if (!string.IsNullOrEmpty(timeCreateString))
                    {
                        try
                        {
                            TimeSpan parsedTime = ParseTimeCreate(timeCreateString);
                            row["timeCreate"] = parsedTime.ToString(@"hh\:mm\:ss");
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing timeCreate: {ex.Message}");
                            row["timeCreate"] = "Invalid Format";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return ds;
        }
        public static TimeSpan ParseTimeCreate(string timeCreate)
        {
            if (TimeSpan.TryParseExact(timeCreate, @"hh\:mm\:ss", null, out TimeSpan parsedTime))
            {
                return parsedTime;
            }
            else
            {
                throw new FormatException($"Invalid time format for timeCreate: {timeCreate}. Expected format is HH:mm:ss.");
            }
        }
        public DataSet GetUpcomingScheduleForUser(int userId, DateTime currentDate)
        {
            string sqlQuery = @"SELECT * FROM tblSchedule 
                                WHERE userId = @userId 
                                AND weekStartDate >= @currentDate
                                ORDER BY weekStartDate";

            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@currentDate", currentDate);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            con.Open();
                            da.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return ds;
        }
        public DataSet GetAllSchedule()
        {
            string sql = "SELECT * FROM tblSchedule";

            using (SqlConnection con = new SqlConnection(DatabaseOperations.ConString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
    }
}


