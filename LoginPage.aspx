<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Aquasol.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>LOGIN</title>
        <link href="LoginPage.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">

    <header>
        <img src="logo.jpg" style="height: 194px; width: 285px" /><br />
        <br />
    </header>

    <div class="loginbox"> 
        <h1>MSOL JAMAICA LTD</h1>
        <h3>AQUASOL</h3>
        <h2>Login Here</h2>
        <asp:Label Text ="Username" CssClass="lblemail" runat="server"  />
        <asp:TextBox  CssClass="txtemail" runat="server" ID="usern" placeholder="Please enter your email address."/>
        <asp:Label Text ="Password" CssClass="lblpassword" runat="server"  />
        <asp:TextBox CssClass="txtpassword" runat="server" ID="pass" placeholder="Please enter your password." />
        <asp:Button ID="loginButton1" runat="server" Text="LOGIN" CssClass="btnsubmit" OnClick="loginButton1_Click" />
        <br />
        <br />
        <asp:LinkButton Text="Forget Password" CssClass="btnforget" runat="server" />
        <br />
                
    </div>
    </form>
</body>
</html>
