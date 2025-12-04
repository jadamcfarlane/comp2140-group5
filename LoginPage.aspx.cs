using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aquasol
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginButton1_Click(object sender, EventArgs e)
        {

            string username = usern.Text.Trim();
            string password = pass.Text.Trim();

            if (username == "" || password == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Enter username and password');", true);
                return;
            }

            string connStr = @"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT UserID, Role FROM Users WHERE UserName=@u AND Password=@p";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userID = Convert.ToInt32(reader["UserID"]);
                    string role = reader["Role"].ToString();

                    ActivityLogger.Log(userID, "Login", "User logged in successfully.");

                    if (role == "Admin")
                    {
                        Response.Redirect("MPOS.aspx");   
                    }
                    else if (role == "User")
                    {
                        Response.Redirect("UPOS.aspx");   
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Unknown role');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid login');", true);
                }

                reader.Close();
            }
        }
    }
}