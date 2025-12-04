using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aquasol
{
    public partial class UserCred : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-KIRN4D4\SQLEXPRESS;Database=Aquasol;Trusted_Connection=True;");

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ClearFunction();
            }
        }

        private void LoadUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return;

            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT FirstName, LastName, Password, Role FROM Users WHERE UserName = @ID",
                conn
            );
            cmd.Parameters.AddWithValue("@ID", userId);

            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                txtID.Text = userId;
                Fname.Text = sdr["FirstName"].ToString();
                Lname.Text = sdr["LastName"].ToString();
                Passw.Text = sdr["Password"].ToString();
                password.Text = sdr["Password"].ToString();
                DropDownList1.SelectedValue = sdr["Role"].ToString();
                LblMess.Text = "";
            }
            else
            {
                LblMess.Text = "User not found.";
            }

            conn.Close();
        }
        protected void UpdateUser_Click1(object sender, EventArgs e)
        {
            conn.Open();

            
            SqlCommand checkCmd = new SqlCommand("SELECT UserID FROM Users WHERE UserName=@UserName", conn);
            checkCmd.Parameters.AddWithValue("@UserName", txtID.Text);
            object userIdObj = checkCmd.ExecuteScalar();

            int userId;

            if (userIdObj != null) 
            {
                userId = (int)userIdObj;

                SqlCommand updateUsers = new SqlCommand(
                    "UPDATE Users SET Password=@Password, Role=@Role WHERE UserID=@UserID", conn);
                updateUsers.Parameters.AddWithValue("@Password", Passw.Text);
                updateUsers.Parameters.AddWithValue("@Role", DropDownList1.SelectedValue);
                updateUsers.Parameters.AddWithValue("@UserID", userId);
                updateUsers.ExecuteNonQuery();

                SqlCommand updateCreate = new SqlCommand(
                    "UPDATE UserCreate SET FirstName=@FirstName, LastName=@LastName WHERE UserID=@UserID", conn);
                updateCreate.Parameters.AddWithValue("@FirstName", Fname.Text);
                updateCreate.Parameters.AddWithValue("@LastName", Lname.Text);
                updateCreate.Parameters.AddWithValue("@UserID", userId);
                updateCreate.ExecuteNonQuery();
            }
            else 
            {
                SqlCommand insertUsers = new SqlCommand(
                    "INSERT INTO Users (UserName, Password, Role) OUTPUT INSERTED.UserID VALUES (@UserName, @Password, @Role)", conn);
                insertUsers.Parameters.AddWithValue("@UserName", txtID.Text);
                insertUsers.Parameters.AddWithValue("@Password", Passw.Text);
                insertUsers.Parameters.AddWithValue("@Role", DropDownList1.SelectedValue);

                userId = (int)insertUsers.ExecuteScalar(); // get new UserID

                SqlCommand insertCreate = new SqlCommand(
                    "INSERT INTO UserCreate (UserID, FirstName, LastName) VALUES (@UserID, @FirstName, @LastName)", conn);
                insertCreate.Parameters.AddWithValue("@UserID", userId);
                insertCreate.Parameters.AddWithValue("@FirstName", Fname.Text);
                insertCreate.Parameters.AddWithValue("@LastName", Lname.Text);
                insertCreate.ExecuteNonQuery();
            }

            LblMess.Text = "User saved successfully!";
            ClearFunction();
}

          protected void DeleteUser_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                LblMess.Text = "Enter a UserName to delete.";
                return;
            }

            try
            {
                conn.Open();

                SqlCommand deleteUser = new SqlCommand("DELETE FROM Users WHERE UserName=@UserName", conn);
                deleteUser.Parameters.AddWithValue("@UserName", txtID.Text);
                int rows = deleteUser.ExecuteNonQuery();

                if (rows > 0)
                {
                    LblMess.Text = "User deleted successfully!";
                    ClearFunction();
                }
                else
                {
                    LblMess.Text = "User not found.";
                }
            }
            catch (Exception ex)
            {
                LblMess.Text = "Error: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("MPOS.aspx");
        }

        protected void ClearFunction()
        {
            txtID.Text = "";
            Fname.Text = "";
            Lname.Text = "";
            Passw.Text = "";
            password.Text = "";
            DropDownList1.SelectedIndex = 0;
        }

    }
}