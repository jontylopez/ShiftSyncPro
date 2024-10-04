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
    /// Summary description for ScheduleManagementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ScheduleManagementService : System.Web.Services.WebService
    {

        [WebMethod]
        public int AddSchedule(int userId, string groupId, DateTime weekStartDate,
                        DateTime? day1Date, int? day1ShiftId, DateTime? day2Date, int? day2ShiftId,
                        DateTime? day3Date, int? day3ShiftId, DateTime? day4Date, int? day4ShiftId,
                        DateTime? day5Date, int? day5ShiftId, DateTime? day6Date, int? day6ShiftId,
                        DateTime? day7Date, int? day7ShiftId, DateTime dayCreate, string timeCreate)
        {
            try
            {
                Schedule schedule = new Schedule
                {
                    UserId = userId,
                    GroupId = groupId,
                    WeekStartDate = weekStartDate,
                    Day1Date = day1Date,
                    Day1ShiftId = day1ShiftId,
                    Day2Date = day2Date,
                    Day2ShiftId = day2ShiftId,
                    Day3Date = day3Date,
                    Day3ShiftId = day3ShiftId,
                    Day4Date = day4Date,
                    Day4ShiftId = day4ShiftId,
                    Day5Date = day5Date,
                    Day5ShiftId = day5ShiftId,
                    Day6Date = day6Date,
                    Day6ShiftId = day6ShiftId,
                    Day7Date = day7Date,
                    Day7ShiftId = day7ShiftId,
                    DayCreate = dayCreate,
                    TimeCreate = timeCreate
                };
                return schedule.AddScheduleToDb();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int UpdateSchedule(int scheduleId,
                          DateTime? day1Date, int? day1ShiftId,
                          DateTime? day2Date, int? day2ShiftId,
                          DateTime? day3Date, int? day3ShiftId,
                          DateTime? day4Date, int? day4ShiftId,
                          DateTime? day5Date, int? day5ShiftId,
                          DateTime? day6Date, int? day6ShiftId,
                          DateTime? day7Date, int? day7ShiftId)
        {
            try
            {
                Schedule schedule = new Schedule
                {
                    Id = scheduleId,
                    Day1Date = day1Date,
                    Day1ShiftId = day1ShiftId,
                    Day2Date = day2Date,
                    Day2ShiftId = day2ShiftId,
                    Day3Date = day3Date,
                    Day3ShiftId = day3ShiftId,
                    Day4Date = day4Date,
                    Day4ShiftId = day4ShiftId,
                    Day5Date = day5Date,
                    Day5ShiftId = day5ShiftId,
                    Day6Date = day6Date,
                    Day6ShiftId = day6ShiftId,
                    Day7Date = day7Date,
                    Day7ShiftId = day7ShiftId
                };
                return schedule.UpdateSchedule();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int DeleteSchedule(int scheduleId)
        {
            try
            {
                Schedule schedule = new Schedule
                {
                    Id = scheduleId
                };

                return schedule.DeleteSchedule(scheduleId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet GetSchedulesByUser(int userId)
        {
            try
            {
                Schedule schedule = new Schedule();
                return schedule.GetSchedulesByUser(userId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet GetSchedulesByGroupAndDateRange(string groupId, DateTime startDate, DateTime endDate)
        {
            try
            {
                Schedule schedule = new Schedule();
                return schedule.GetSchedulesByGroupAndDateRange(groupId, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet GetScheduleById(int scheduleId)
        {
            try
            {
                Schedule schedule = new Schedule();
                return schedule.GetScheduleById(scheduleId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int UpdateShift(int scheduleId, int day, int newShiftId)
        {
            try
            {
                Schedule schedule = new Schedule();
                return schedule.UpdateShift(scheduleId, day, newShiftId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
        [WebMethod]
        public DataSet GetScheduleByStartDate(DateTime weekStartDate)
        {
            Schedule scheduleManager = new Schedule();
            DataSet ds = scheduleManager.GetScheduleByStartDate(weekStartDate);
            return ds;
        }
        [WebMethod]
        public DataSet GetUpcomingScheduleForUser(int userId, DateTime currentDate)
        {
            try
            {
                Schedule schedule = new Schedule();
                return schedule.GetUpcomingScheduleForUser(userId, currentDate);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
        [WebMethod]
        public DataSet GetAllSchedule()
        {
            try
            {
                Schedule schedule = new Schedule();
                return schedule.GetAllSchedule();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
    }
}
