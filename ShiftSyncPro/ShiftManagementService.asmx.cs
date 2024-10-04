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
    /// Summary description for ShiftManagementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ShiftManagementService : System.Web.Services.WebService
    {

        [WebMethod]
        public int AddShift(string shiftType, string startTime, string endTime)
        {
            try
            {
                Shift shift = new Shift
                {
                    ShiftType = shiftType,
                    StartTime = startTime, // Use string directly
                    EndTime = endTime      // Use string directly
                };

                return shift.AddShift();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int UpdateShift(int id, string shiftType, string startTime, string endTime)
        {
            try
            {
                Shift shift = new Shift
                {
                    Id = id,
                    ShiftType = shiftType,
                    StartTime = startTime, // Use string directly
                    EndTime = endTime      // Use string directly
                };

                return shift.UpdateShift();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public int DeleteShift(int id)
        {
            try
            {
                Shift shift = new Shift();
                return shift.DeleteShift(id);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet GetAllShifts()
        {
            try
            {
                Shift shift = new Shift();
                return shift.GetAllShifts();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        [WebMethod]
        public DataSet GetShiftById(int id)
        {
            try
            {
                Shift shift = new Shift();
                return shift.GetShiftById(id);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // New WebMethod to get start time of a shift by ID
        [WebMethod]
        public string GetStartTimeById(int id)
        {
            try
            {
                Shift shift = new Shift();
                return shift.GetStartTimeById(id);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // New WebMethod to get end time of a shift by ID
        [WebMethod]
        public string GetEndTimeById(int id)
        {
            try
            {
                Shift shift = new Shift();
                return shift.GetEndTimeById(id);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
        [WebMethod]
        public DataSet GetShiftByType(string shiftType)
        {
            try
            {
                Shift shift = new Shift();
                return shift.GetShiftByType(shiftType);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
    }
}
