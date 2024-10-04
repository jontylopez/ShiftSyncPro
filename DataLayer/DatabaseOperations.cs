using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DatabaseOperations
    {
        public static string ConString = "Server=(localdb)\\Local;Database=shiftSyncPro;Trusted_Connection=True;TrustServerCertificate=True";

        public static int ExecuteQuery(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                con.Open();
                return com.ExecuteNonQuery();
            }

        }

        public static DataSet SelectQuery(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }

        }

    }
}
