using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aquasol
{
    public partial class UserActivity : System.Web.UI.Page
    {
        private string connStr = @"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadUsersDropdown();
                LoadActivityReport();
            }
        }

        private void LoadUsersDropdown()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserID, UserName FROM Users ORDER BY UserName", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlUsers.DataSource = dt;
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataValueField = "UserID";
                ddlUsers.DataBind();

                ddlUsers.Items.Insert(0, new ListItem("All Users", "0"));
            }
        }

        private void LoadActivityReport()
        {
            int selectedUserId = int.Parse(ddlUsers.SelectedValue);
            DateTime startDate = DateTime.Parse(txtStartDate.Text);
            DateTime endDate = DateTime.Parse(txtEndDate.Text).AddDays(1).AddSeconds(-1); // include entire day

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
            SELECT ua.ActivityTime, u.UserName, ua.ActivityType, ua.ActivityDescription
            FROM UserActivity ua
            INNER JOIN Users u ON ua.UserID = u.UserID
            WHERE ua.ActivityTime BETWEEN @start AND @end";

                if (selectedUserId > 0)
                {
                    query += " AND ua.UserID = @userId";
                }

                query += " ORDER BY ua.ActivityTime DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", startDate);
                cmd.Parameters.AddWithValue("@end", endDate);
                if (selectedUserId > 0)
                    cmd.Parameters.AddWithValue("@userId", selectedUserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvUserActivity.DataSource = dt;
                gvUserActivity.DataBind();
            }
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadActivityReport();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadActivityReport();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadActivityReport();
        }
    }
}