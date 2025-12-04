<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="Aquasol.Inventory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Overview</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="Inventory.css" />
</head>

<body>
    <form id="form1" runat="server">
        <header>
            <ul class="navigation">
                <li><a href="MPOS.aspx">Dashboard</a></li>
                <li><a href="Sales.aspx">Sales</a></li>
                <li><a href="LoginPage.aspx">Sign Out</a></li>
            </ul>
        </header>
        <main class="dashboard">

            <header class="dashboard-header">
                <h1 class="dashboard-title">Inventory Overview</h1>
            </header>

            <!-- KPI cards -->
            <section class="kpi-grid kpi-grid-2x2">

                <!-- Total Items -->
                <article class="card kpi-large">
                    <div class="card-title">Total Items in Stock</div>
                    <div class="card-value">
                        <asp:Label ID="lblTotalItems" runat="server" Text="0"></asp:Label>
                    </div>
                    <div class="card-subrow"><span>+0%</span></div>
                    <div class="card-compare">vs previous 30 days</div>

                    <div class ="inventory-summary">
                    <div class="card-title">Inventory Stock Summary</div>

                    <asp:Repeater ID="rptAllItems" runat="server">
                        <ItemTemplate>
                            <div class='stock-card <%# GetStockClass((int)Eval("Stock")) %>'>
                                <div class="stock-title"><%# Eval("ItemName") %></div>
                                <div class="stock-qty">Remaining: <%# Eval("Stock") %></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    </div>
                </article>

                <!-- Unique SKU -->
                <article class="card kpi-large">
                    <div class="card-title">Unique SKUs</div>
                    <div class="card-value">
                        <asp:Label ID="lblUniqueSKU" runat="server" Text="0"></asp:Label>
                    </div>
                    <div class ="inventory-summary">
                    <asp:Repeater ID="rptUniqueSKU" runat="server">
                        <ItemTemplate>
                            <div class="stock-card stock-safe">
                                <div class="stock-title">
                                    SKU: <%# Eval("ItemID") %> - <%# Eval("ItemName") %>
                                </div>
                                 
                               </div>
                    </ItemTemplate>
                    </asp:Repeater>
                        </div>
                </article>

                <!-- Low Stock -->
                <article class="card kpi-large">
                    <div class="card-title">Low Stock Items</div>
                    <div class="card-value">
                        <asp:Label ID="lblLowStock" runat="server" Text="0"></asp:Label>
                    </div>

                    <asp:Repeater ID="rptLowStock" runat="server">
                        <ItemTemplate>
                            <div class='stock-card <%# GetStockClass((int)Eval("Stock")) %>'>
                                <div class="stock-title"><%# Eval("ItemName") %></div>
                                <div class="stock-qty">Remaining: <%# Eval("Stock") %></div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="card-subrow"><span>0%</span></div>
                    <div class="card-compare">vs previous 30 days</div>
                </article>

                <!-- Inventory Value -->
                <article class="card kpi-large">
                    <div class="card-title">Inventory Value</div>
                    <div class="card-value">
                        $<asp:Label ID="lblInventoryValue" runat="server" Text="0"></asp:Label>
                    </div>
                    <div class="card-subrow"><span>+0%</span></div>
                    <div class="card-compare">vs previous 30 days</div>
                </article>

            </section>

        </main>

    </form>
</body>
</html>