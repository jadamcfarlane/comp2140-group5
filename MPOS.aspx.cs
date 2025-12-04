using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Aquasol
{
    public partial class POS : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;");

        protected DataTable Cart;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["Cart"] == null)
                CreateCartTable();
            else
                Cart = (DataTable)Session["Cart"];

            gvCart.DataSource = Cart;
            gvCart.DataBind();
        }

        protected void CreateCartTable()
        {
            Cart = new DataTable();
            Cart.Columns.Add("ItemID", typeof(int));
            Cart.Columns.Add("ItemName", typeof(string));
            Cart.Columns.Add("Price", typeof(decimal));
            Cart.Columns.Add("Qty", typeof(int));
            Cart.Columns.Add("Total", typeof(decimal));

            Session["Cart"] = Cart;
        }

        protected void userButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserCred.aspx");
        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            CreateCartTable(); 
            gvCart.DataSource = Cart; 
            gvCart.DataBind(); 
            UpdateTotals();


            receiptBox.Style["display"] = "none";
            ltReceipt.Text = string.Empty;


            txtSearch.Text = "";


            gvItems.DataSource = null;
            gvItems.DataBind();
        }

        protected void salesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sales.aspx");
        }

        protected void inventoryButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inventory.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT ItemID, ItemName, Price FROM Item WHERE ItemName LIKE @search", conn);
            cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            conn.Close();

            gvItems.DataSource = dt;
            gvItems.DataBind();
        }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvItems.Rows[index];
                int itemID = Convert.ToInt32(gvItems.DataKeys[index].Value);
                string name = row.Cells[0].Text;
                decimal price = Decimal.Parse(row.Cells[1].Text.Replace("$", ""));

                DataRow existing = Cart.Rows.Cast<DataRow>().FirstOrDefault(r => r.Field<int>("ItemID") == itemID);
                if (existing != null)
                    existing["Qty"] = (int)existing["Qty"] + 1;
                else
                    Cart.Rows.Add(itemID, name, price, 1, price);

                foreach (DataRow r in Cart.Rows) r["Total"] = (int)r["Qty"] * Convert.ToDecimal(r["Price"]);

                gvCart.DataSource = Cart;
                gvCart.DataBind();
                UpdateTotals();
            }
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                int itemID = Convert.ToInt32(e.CommandArgument);
                DataRow row = Cart.Rows.Cast<DataRow>().FirstOrDefault(r => r.Field<int>("ItemID") == itemID);
                if (row != null) Cart.Rows.Remove(row);

                gvCart.DataSource = Cart;
                gvCart.DataBind();
                UpdateTotals();
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TextBox txtQty = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtQty.NamingContainer;
            int itemID = (int)gvCart.DataKeys[row.RowIndex].Value;

            DataRow dr = Cart.Rows.Cast<DataRow>().FirstOrDefault(r => r.Field<int>("ItemID") == itemID);
            if (dr != null)
            {
                int qty = 1;
                if (int.TryParse(txtQty.Text, out qty) && qty > 0)
                {
                    dr["Qty"] = qty;
                    dr["Total"] = qty * Convert.ToDecimal(dr["Price"]);
                }
                else
                {
                    dr["Qty"] = 1;
                    dr["Total"] = Convert.ToDecimal(dr["Price"]);
                }
            }

            gvCart.DataSource = Cart;
            gvCart.DataBind();
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            foreach (GridViewRow row in gvCart.Rows)
            {
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                int qty = int.Parse(txtQty.Text);
                int itemID = (int)gvCart.DataKeys[row.RowIndex].Value;

                DataRow dr = Cart.Rows.Cast<DataRow>().FirstOrDefault(r => r.Field<int>("ItemID") == itemID);
                if (dr != null)
                {
                    dr["Qty"] = qty;
                    dr["Total"] = qty * Convert.ToDecimal(dr["Price"]);
                }
            }

            decimal subtotal = Cart.AsEnumerable().Sum(r => r.Field<decimal>("Total"));
            decimal tax = subtotal * 0.05m;
            decimal total = subtotal + tax;

            lblSubtotal.Text = "$" + subtotal.ToString("0.00");
            lblTax.Text = "$" + tax.ToString("0.00");
            lblTotal.Text = "$" + total.ToString("0.00");
        }


        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Cart.Rows.Count == 0) return;

            string receiptNo = "R" + DateTime.Now.ToString("yyyyMMddHHmmss");
            StringBuilder sb = new StringBuilder();
            decimal subtotal = 0;

            sb.Append("<div style='text-align:center;'>");
            sb.Append("<h2>MSOL JAMAICA LTD</h2>");
            sb.Append("<h4>Receipt No: " + receiptNo + "</h4>");
            sb.Append("<p>Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</p>");
            sb.Append("</div><hr/>");

            sb.Append("<table style='width:100%; border-collapse:collapse;'>");
            sb.Append("<tr><th>Item</th><th>Qty</th><th>Price</th><th>Total</th></tr>");

            using (SqlConnection con = new SqlConnection(@"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;"))
            {
                con.Open();

                foreach (DataRow row in Cart.Rows)
                {
                    int itemId = Convert.ToInt32(row["ItemID"]);
                    int qtySold = Convert.ToInt32(row["Qty"]);
                    decimal price = Convert.ToDecimal(row["Price"]);
                    decimal total = Convert.ToDecimal(row["Total"]);

                    sb.Append("<tr>");
                    sb.Append($"<td>{row["ItemName"]}</td>");
                    sb.Append($"<td>{qtySold}</td>");
                    sb.Append($"<td>{price:C}</td>");
                    sb.Append($"<td>{total:C}</td>");
                    sb.Append("</tr>");

                    subtotal += total;

                    SqlCommand cmd = new SqlCommand(@"
                INSERT INTO Sales (ReceiptNo, ItemID, Qty, Price, Total, DateSold, ReceiptHTML)
                VALUES (@rno, @id, @qty, @price, @total, GETDATE(), '')
            ", con);

                    cmd.Parameters.AddWithValue("@rno", receiptNo);
                    cmd.Parameters.AddWithValue("@id", itemId);
                    cmd.Parameters.AddWithValue("@qty", qtySold);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.ExecuteNonQuery();

                    SqlCommand updateStock = new SqlCommand(@"
                UPDATE Item
                SET Stock = Stock - @qty
                WHERE ItemID = @id
            ", con);

                    updateStock.Parameters.AddWithValue("@qty", qtySold);
                    updateStock.Parameters.AddWithValue("@id", itemId);
                    updateStock.ExecuteNonQuery();
                }

                SqlCommand cmdUpdate = new SqlCommand(
                    "UPDATE Sales SET ReceiptHTML=@html WHERE ReceiptNo=@rno",
                    con
                );
                cmdUpdate.Parameters.AddWithValue("@html", sb.ToString());
                cmdUpdate.Parameters.AddWithValue("@rno", receiptNo);
                cmdUpdate.ExecuteNonQuery();

                con.Close();
            }

            sb.Append("</table><hr/>");
            decimal tax = subtotal * 0.05m;
            decimal grandTotal = subtotal + tax;

            sb.Append($"<p>Subtotal: {subtotal:C}</p>");
            sb.Append($"<p>Tax (5%): {tax:C}</p>");
            sb.Append($"<p><b>Total: {grandTotal:C}</b></p>");
            sb.Append("<p style='text-align:center;'>Thank you for your purchase!</p>");

            ltReceipt.Text = sb.ToString();
            receiptBox.Style["display"] = "block";

            CreateCartTable();
            gvCart.DataSource = Cart;
            gvCart.DataBind();
            UpdateTotals();

            int userID = Convert.ToInt32(Session["UserID"]);
            ActivityLogger.Log(userID, "Checkout", "Completed a checkout of " + Cart.Rows.Count + " items.");
        }

        protected void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            string script = "window.print();";
            ClientScript.RegisterStartupScript(this.GetType(), "PrintReceipt", script, true);
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("LoginPage.aspx");
        }

        protected void reportsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx");
        }

        
    }
}