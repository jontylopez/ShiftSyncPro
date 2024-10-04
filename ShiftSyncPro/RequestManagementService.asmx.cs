using BusinessLayer;
using System;
using System.Data;
using System.Web;
using System.Web.Services;

namespace ShiftSyncPro
{
    /// <summary>
    /// Summary description for RequestManagementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RequestManagementService : System.Web.Services.WebService
    {
        [WebMethod]
        public int AddWeeklyRequest(int userId, int scheduleId,
                    int? day1ShiftId, DateTime? day1Date,
                    int? day2ShiftId, DateTime? day2Date,
                    int? day3ShiftId, DateTime? day3Date,
                    int? day4ShiftId, DateTime? day4Date,
                    int? day5ShiftId, DateTime? day5Date,
                    int? day6ShiftId, DateTime? day6Date,
                    int? day7ShiftId, DateTime? day7Date,
                    string status, DateTime? requestDate,
                    string workerComment, byte[] proofDocument,
                    string proofDocumentType) // Added new parameter for document type
        {
            try
            {
                // Create a Request object and populate its properties
                Request request = new Request
                {
                    UserId = userId,
                    ScheduleId = scheduleId,
                    Day1ShiftId = day1ShiftId,
                    Day1Date = day1Date,
                    Day2ShiftId = day2ShiftId,
                    Day2Date = day2Date,
                    Day3ShiftId = day3ShiftId,
                    Day3Date = day3Date,
                    Day4ShiftId = day4ShiftId,
                    Day4Date = day4Date,
                    Day5ShiftId = day5ShiftId,
                    Day5Date = day5Date,
                    Day6ShiftId = day6ShiftId,
                    Day6Date = day6Date,
                    Day7ShiftId = day7ShiftId,
                    Day7Date = day7Date,
                    Status = status,
                    RequestDate = requestDate,
                    WorkerComment = workerComment,
                    ProofDocument = proofDocument,
                    ProofDocumentType = proofDocumentType // Assigning the MIME type
                };

                // Call the method to insert the request into the database
                return request.AddWeeklyRequest();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the method execution
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to update the status of a request, now with supervisorComment
        [WebMethod]
        public int UpdateRequestStatus(int requestId, string status, DateTime changeDate, string supervisorComment = null)
        {
            try
            {
                Request request = new Request();
                return request.UpdateRequestStatus(requestId, status, changeDate, supervisorComment);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to get all requests for a specific user
        [WebMethod]
        public DataSet GetRequestsByUser(int userId)
        {
            try
            {
                Request request = new Request();
                return request.GetRequestsByUser(userId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to get all requests for a specific schedule
        [WebMethod]
        public DataSet GetRequestsBySchedule(int scheduleId)
        {
            try
            {
                Request request = new Request();
                return request.GetRequestsBySchedule(scheduleId);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to get all requests by status
        [WebMethod]
        public DataSet GetRequestsByStatus(string status)
        {
            try
            {
                Request request = new Request();
                return request.GetRequestsByStatus(status);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        // WebMethod to get a request by its ID
        [WebMethod]
        public DataSet GetRequestById(int id)
        {
            try
            {
                Request request = new Request();
                return request.GetRequestById(id);
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }
        [WebMethod]
        public DataSet GetProofDocumentById(int id)
        {
            Request request = new Request();
            return request.GetProofDocumentById(id);
        }
    }
}
