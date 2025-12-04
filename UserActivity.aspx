<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserActivity.aspx.cs" Inherits="Aquasol.UserActivity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Activity Report</title>
    <link href="UserActivity.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <ul class="navigation">
                <li><a href="MPOS.aspx">Dashboard</a></li>
                <li><a href="Report.aspx">Reports</a></li>
                <li><a href="LoginPage.aspx">Sign Out</a></li>
            </ul>
</header>
        <div class="main">
            <h1>User Activity Report</h1>

            <div class="filter-section">
                <asp:Label ID="lblUser" runat="server" Text="User: "></asp:Label>
                <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged"></asp:DropDownList>

                <asp:Label ID="lblStart" runat="server" Text="Start Date: "></asp:Label>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="date-input"></asp:TextBox>

                <asp:Label ID="lblEnd" runat="server" Text="End Date: "></asp:Label>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="date-input"></asp:TextBox>

                <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btnsubmit" OnClick="btnFilter_Click" />
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btnsubmit" OnClick="btnRefresh_Click" />
            </div>

            <asp:GridView ID="gvUserActivity" runat="server" AutoGenerateColumns="False" CssClass="grid-items">
                <Columns>
                    <asp:BoundField DataField="ActivityTime" HeaderText="Time" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                    <asp:BoundField DataField="UserName" HeaderText="User" />
                    <asp:BoundField DataField="ActivityType" HeaderText="Activity Type" />
                    <asp:BoundField DataField="ActivityDescription" HeaderText="Description" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>