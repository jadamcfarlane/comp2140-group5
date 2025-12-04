<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Aquasol.Report" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory & Sales Report</title>
    <link href="Report.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <ul class="navigation">
                <li><a href="MPOS.aspx">Dashboard</a></li>
                <li><a href="UserActivity.aspx">UserActivity</a></li>
                <li><a href="LoginPage.aspx">Sign Out</a></li>
            </ul>
        </header>
        <div class="dashboard">
            <h1 class="dashboard-title">Inventory & Sales Report</h1>

           <asp:Repeater ID="rptInventorySales" runat="server">
    <HeaderTemplate>
        <table class="report-table">
            <thead>
                <tr>
                    <th>Item Name</th>
                    <th>Remaining Stock</th>
                    <th>Total Sold</th>
                    <th>Revenue ($)</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>

    <ItemTemplate>
        <tr>
            <td><%# Eval("ItemName") %></td>
            <td><%# Eval("RemainingStock") %></td>
            <td><%# Eval("TotalSold") %></td>
            <td><%# Eval("Revenue", "{0:N2}") %></td>
        </tr>
    </ItemTemplate>

    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>
            <button type="button" onclick="window.print()" class="print-btn">
    Print Report
</button>
        </div>
    </form>
</body>
</html>
