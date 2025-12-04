using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aquasol
{
    public static class ActivityLogger
    {
        private static string connStr = @"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;";

        public static void Log(int userID, string type, string description)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO UserActivity (UserID, ActivityTime, ActivityType, ActivityDescription) " +
                               "VALUES (@UserID, GETDATE(), @Type, @Desc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Desc", description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}