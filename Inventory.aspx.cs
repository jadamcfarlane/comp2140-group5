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
    public partial class Inventory : System.Web.UI.Page
    {
        private string connStr = @"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadKPIs();
                LoadAllItemsIntoCard();
                LoadLowStockItems();

            }
        }

        private void LoadKPIs()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand totalCmd = new SqlCommand(
                "SELECT SUM(Stock) FROM Item", conn);
                lblTotalItems.Text = totalCmd.ExecuteScalar().ToString() ?? "0";

                SqlCommand skuCmd = new SqlCommand(
                    "SELECT COUNT(ItemID) FROM Item", conn);
                lblUniqueSKU.Text = skuCmd.ExecuteScalar().ToString() ?? "0";

                SqlDataAdapter da = new SqlDataAdapter(
                "SELECT ItemID, ItemName, Stock FROM Item ORDER BY ItemName ASC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptUniqueSKU.DataSource = dt;
                rptUniqueSKU.DataBind();

                SqlCommand lowStockCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Item WHERE Stock <= 10", conn);
                lblLowStock.Text = lowStockCmd.ExecuteScalar().ToString() ?? "0";

                SqlCommand valueCmd = new SqlCommand(
                    "SELECT SUM(Price * Stock) FROM Item", conn);
                object val = valueCmd.ExecuteScalar();
                lblInventoryValue.Text = val == DBNull.Value ? "0" : Convert.ToDecimal(val).ToString("N2"); ;
            }
        }

        public string GetStockClass(int stock)
        {
            if (stock <= 5)
                return "stock-critical";  
            if (stock <= 10)
                return "stock-low";       
            return "stock-safe";          
        }

        private void LoadAllItemsIntoCard()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand skuCmd = new SqlCommand(
                "SELECT COUNT(ItemID) FROM Item", conn);
                lblUniqueSKU.Text = skuCmd.ExecuteScalar().ToString() ?? "0";

                SqlCommand cmd = new SqlCommand(
                    "SELECT ItemID, ItemName, Stock FROM Item ORDER BY ItemName ASC", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptAllItems.DataSource = dt;
                rptAllItems.DataBind();

            }
        }

        private void LoadLowStockItems()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT ItemName, Stock FROM Item WHERE Stock <= 10 ORDER BY Stock ASC", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptLowStock.DataSource = dt;
                rptLowStock.DataBind();
            }
        }


    }
}