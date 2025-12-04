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
    public partial class Report : System.Web.UI.Page
    {
        private string connStr = @"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInventorySalesReport();
            }
        }

        private void LoadInventorySalesReport()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        i.ItemID,
                        i.ItemName,
                        i.Stock AS RemainingStock,
                        ISNULL(SUM(s.Qty), 0) AS TotalSold,
                        ISNULL(SUM(s.Qty * s.Price), 0) AS Revenue
                    FROM Item i
                    LEFT JOIN Sales s ON i.ItemID = s.ItemID
                    GROUP BY i.ItemID, i.ItemName, i.Stock
                    ORDER BY i.ItemName", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptInventorySales.DataSource = dt;
                rptInventorySales.DataBind();
            }
        }
    }

}