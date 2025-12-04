<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="Aquasol.Sales" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Dashboard</title>
    <link href="Sales.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <ul class="navigation">
                <li><a href="MPOS.aspx">Dashboard</a></li>
                <li><a href="Inventory.aspx">Inventory</a></li>
                <li><a href="LoginPage.aspx">Sign Out</a></li>
            </ul>
        </header>
        <main class="dashboard">

            <header class="dashboard-header">
                <h1 class="dashboard-title">Sales Dashboard</h1>

                <div class="filters">
                    <div class="filter">
                        <span class="filter-label">Month</span>
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </header>

            <section class="kpi-grid">
                <article class="card">
                    <div class="card-title">Total Accounts</div>
                    <div class="card-value"><asp:Label ID="lblTotalAccounts" runat="server" Text="0"></asp:Label></div>
                    <div class="card-subrow"><span class="card-change">+20%</span></div>
                    <div class="card-compare">vs previous 30 days</div>
                </article>

                <article class="card">
                    <div class="card-title">Orders per Month</div>
                    <div class="card-value"><asp:Label ID="lblOrders" runat="server" Text="0"></asp:Label></div>
                    <div class="card-subrow"><span class="card-change">+15</span></div>
                    <div class="card-compare">vs previous 30 days</div>
                </article>

                <article class="card">
                    <div class="card-title">Growth Rate</div>
                    <div class="card-value"><asp:Label ID="lblGrowth" runat="server" Text="0%"></asp:Label></div>
                    <div class="card-subrow"><span class="card-change">+1.3%</span></div>
                    <div class="card-compare">vs previous 30 days</div>
                </article>

                <article class="card">
                    <div class="card-title">Revenue</div>
                    <div class="card-value"><asp:Label ID="lblRevenue" runat="server" Text="$0.00"></asp:Label></div>
                    <div class="card-subrow"><span class="card-change">+5%</span></div>
                    <div class="card-compare">vs previous 30 days</div>
                </article>

                <asp:GridView ID="gvSalesReport" runat="server" AutoGenerateColumns="False" CssClass="grid-items">
    <Columns>
        <asp:BoundField DataField="ItemName" HeaderText="Product Type" />
        <asp:BoundField DataField="UserName" HeaderText="Employee" />
        <asp:BoundField DataField="TotalSold" HeaderText="Total Sold" />
        <asp:BoundField DataField="Revenue" HeaderText="Revenue" DataFormatString="{0:C}" />
    </Columns>
</asp:GridView>

            </section>

        </main>
    </form>
</body>
</html>

