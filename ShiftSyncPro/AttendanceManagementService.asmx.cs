using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ShiftSyncPro
{
    /// <summary>
    /// Summary description for AttendanceManagementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AttendanceManagementService : System.Web.Services.WebService
    {

        // WebMethod to insert a new attendance batch
        [WebMethod]
        public int InsertAttendanceBatch(List<Attendance> attendanceList)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.InsertAttendanceBatch(attendanceList);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the start time of an attendance record
        [WebMethod]
        public int UpdateStartTime(int attendanceId, string newStartTime)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.UpdateInTime(attendanceId, newStartTime);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the end time of an attendance record
        [WebMethod]
        public int UpdateEndTime(int attendanceId, string newEndTime)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.UpdateOutTime(attendanceId, newEndTime);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the in time of an attendance record (New)
        [WebMethod]
        public int UpdateInTime(int attendanceId, string newInTime)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.UpdateInTime(attendanceId, newInTime);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the out time of an attendance record (New)
        [WebMethod]
        public int UpdateOutTime(int attendanceId, string newOutTime)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.UpdateOutTime(attendanceId, newOutTime);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the attendance status of an attendance record
        [WebMethod]
        public int UpdateAttStatus(int attendanceId, string newStatus)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.UpdateAttStatus(attendanceId, newStatus);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the worker comment (New)
        [WebMethod]
        public int UpdateWorkerComment(int attendanceId, string workerComment)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.UpdateWorkerComment(attendanceId, workerComment);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the supervisor comment (New)
        [WebMethod]
        public int UpdateSupervisorComment(int attendanceId, string supervisorComment)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.UpdateSupervisorComment(attendanceId, supervisorComment);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to retrieve all attendance records
        [WebMethod]
        public DataSet GetAllAttendance()
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.GetAllAttendance();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to retrieve a specific attendance record by its ID
        [WebMethod]
        public DataSet GetAttendanceById(int attendanceId)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.GetAttendanceById(attendanceId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to retrieve attendance records by user ID
        [WebMethod]
        public DataSet GetAttendanceByUser(int userId)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.GetAttendanceByUser(userId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to retrieve attendance records by week start date
        [WebMethod]
        public DataSet GetAttendanceByWeekStartDate(DateTime weekStartDate)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.GetAttendanceByWeekStartDate(weekStartDate);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
        [WebMethod]
        public DataSet GetAttendanceByDate(DateTime selectedDate)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.GetAttendanceByDate(selectedDate);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int InsertInTime(int userId, DateTime date, string inTime)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.InsertInTime(userId, date, inTime);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int InsertOutTime(int userId, DateTime date, string outTime)
        {
            try
            {
                Attendance attendance = new Attendance();
                return attendance.InsertOutTime(userId, date, outTime);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

    }
}
