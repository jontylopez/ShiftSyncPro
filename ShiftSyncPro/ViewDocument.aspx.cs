using System;
using System.Data;

namespace ShiftSyncPro
{
    public partial class ViewDocument : System.Web.UI.Page
    {
        localhostRequest.RequestManagementService rms = new localhostRequest.RequestManagementService();

        protected void Page_Load(object sender, EventArgs e)
        {
            string requestId = Request.QueryString["requestId"];
            if (!string.IsNullOrEmpty(requestId))
            {
                int id = Convert.ToInt32(requestId);

                // Get proofDocument and proofDocumentType from the service
                DataSet documentData = rms.GetProofDocumentById(id);

                if (documentData != null && documentData.Tables[0].Rows.Count > 0)
                {
                    // Extract the proof document and its type
                    byte[] proofDocument = documentData.Tables[0].Rows[0]["proofDocument"] != DBNull.Value
                        ? (byte[])documentData.Tables[0].Rows[0]["proofDocument"]
                        : null;

                    string proofDocumentType = documentData.Tables[0].Rows[0]["proofDocumentType"] != DBNull.Value
                        ? documentData.Tables[0].Rows[0]["proofDocumentType"].ToString()
                        : null;

                    if (proofDocument != null && !string.IsNullOrEmpty(proofDocumentType))
                    {
                        // Set the correct content type
                        Response.Clear();
                        Response.ContentType = proofDocumentType;

                        // Set the file name to include the requestId and a file extension based on the content type
                        string fileExtension = GetFileExtension(proofDocumentType);
                        string fileName = $"proofDocument_{requestId}{fileExtension}";

                        Response.AddHeader("Content-Disposition", $"inline; filename={fileName}");
                        Response.BinaryWrite(proofDocument);
                        Response.End();
                    }
                    else
                    {
                        Response.Write("No document found for this request.");
                    }
                }
                else
                {
                    Response.Write("No data found.");
                }
            }
        }

        // Helper method to get the correct file extension based on content type
        private string GetFileExtension(string contentType)
        {
            switch (contentType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "image/png":
                    return ".png";
                case "application/pdf":
                    return ".pdf";
                default:
                    return ""; // Return empty if no specific extension is found
            }
        }
    }
}
