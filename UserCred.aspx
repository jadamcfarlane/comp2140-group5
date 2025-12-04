<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCred.aspx.cs" Inherits="Aquasol.UserCred" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Credential</title>
    <link href="UserCred.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <ul class="navigation">
                <li><a href="MPOS.aspx">Dashboard</a></li>
                <li><a href="LoginPage.aspx">Sign Out</a></li>
            </ul>
        </header>
        <div class="user-container">

            <h2>User Credentials</h2>

            <div class="form-group">
                <label>User Name</label>
                <asp:TextBox ID="txtID" runat="server" CssClass="input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtID" CssClass="error" ErrorMessage="Please Enter a valid User ID" />
            </div>

            <div class="form-group">
                <label>First Name</label>
                <asp:TextBox ID="Fname" runat="server" CssClass="input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="Fname" CssClass="error" ErrorMessage="Please Fill out field" />
            </div>

            <div class="form-group">
                <label>Last Name</label>
                <asp:TextBox ID="Lname" runat="server" CssClass="input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="Lname" CssClass="error" ErrorMessage="Please Fill out Field" />
            </div>

            <div class="form-group">
                <label>Password</label>
                <asp:TextBox ID="Passw" runat="server" TextMode="Password" CssClass="input"></asp:TextBox>
            </div>

            <div class="form-group">
                <label>Confirm Password</label>
                <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="input"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server"
                    ControlToCompare="Passw" ControlToValidate="password"
                    CssClass="error" ErrorMessage="Passwords Do Not Match" />
            </div>

            <div class="form-group">
                <label>User Type</label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input">
                    <asp:ListItem>User</asp:ListItem>
                    <asp:ListItem>Admin</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="btn-row">
                <asp:Button ID="UpdateUser" runat="server" CssClass="btnsubmit" Text="Save Changes" OnClick="UpdateUser_Click1"  />
                <asp:Button ID="DeleteUser" runat="server" CssClass="btnsubmit danger" Text="Delete User" OnClick="DeleteUser_Click1"  />
                <asp:Button ID="Cancel" runat="server" CssClass="btnsubmit" Text="Cancel" OnClick="Cancel_Click"  />
            </div>

            <asp:Label ID="LblMess" runat="server" CssClass="message"></asp:Label>

        </div>
    </form>
</body>

</html>
