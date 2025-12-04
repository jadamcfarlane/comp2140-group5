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
    public partial class Sales : System.Web.UI.Page
    {
        private string connStr = @"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
                LoadKPI();
            }
        }

        private void LoadDropdowns()
        {
            // Populate Months dropdown
            ddlMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new ListItem(
                    new DateTime(2000, i, 1).ToString("MMMM"), i.ToString()));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadKPI();
        }

        private void LoadKPI()
        {
            int month = int.Parse(ddlMonth.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Total Accounts
                SqlCommand cmdAccounts = new SqlCommand(
                    "SELECT COUNT(DISTINCT ReceiptNo) FROM Sales WHERE MONTH(DateSold)=@Month ",
                    conn
                );
                cmdAccounts.Parameters.AddWithValue("@Month", month);
                lblTotalAccounts.Text = cmdAccounts.ExecuteScalar().ToString();

                // Orders per Month
                SqlCommand cmdOrders = new SqlCommand(
                    "SELECT COUNT(SaleID) FROM Sales WHERE MONTH(DateSold)=@Month",
                    conn
                );
                cmdOrders.Parameters.AddWithValue("@Month", month);
                lblOrders.Text = cmdOrders.ExecuteScalar().ToString();

                int prevMonth = month == 1 ? 12 : month - 1;
                SqlCommand cmdPrev = new SqlCommand(
                    "SELECT COUNT(*) FROM Sales WHERE MONTH(DateSold)=@PrevMonth", conn
                );
                cmdPrev.Parameters.AddWithValue("@PrevMonth", prevMonth);
                int prevOrders = Convert.ToInt32(cmdPrev.ExecuteScalar());
                int currOrders = int.Parse(lblOrders.Text);
                double growth = prevOrders == 0 ? 0 : ((double)(currOrders - prevOrders) / prevOrders) * 100;
                lblGrowth.Text = growth.ToString("0.0") + "%";

                // Revenue
                SqlCommand cmdRevenue = new SqlCommand(
                    "SELECT ISNULL(SUM(Total),0) FROM Sales WHERE MONTH(DateSold)=@Month", conn
                );
                cmdRevenue.Parameters.AddWithValue("@Month", month);
                lblRevenue.Text = "$" + Convert.ToDecimal(cmdRevenue.ExecuteScalar()).ToString("N2");
            }
        }

    }
}
