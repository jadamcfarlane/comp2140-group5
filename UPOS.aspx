<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UPOS.aspx.cs" Inherits="Aquasol.UPOS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Cashier POS Dashboard</title>
    <link href="UPOS.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

        <div class="layout">

            <!-- SIDEBAR -->
            <div class="sidebar">
                <img src="logo.jpg" style="height: 173px; width: 221px" />&nbsp;
                <div class="logo">MSOL JAMAICA LTD</div>

                <asp:Button ID="btnExit" runat="server" Text="Logout" CssClass="sidebar-btn logout-btn" OnClick="btnExit_Click" />
                
                <asp:Button ID="btnNewOrder" runat="server" Text="+ New Order" CssClass="sidebar-btn" OnClick="btnNewOrder_Click" />

                <div class="user-box">
                    <div class="user-pic"></div>
                    <div class="user-info">
                        <strong>-</strong><br />
                        Cashier
                    </div>
                </div>
            </div>

                                <!-- ORDER SECTION -->
                <div class="order-section">
                    <div class="order-box">
                        <h2>New Order</h2>
                        <div class="search-box">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" Placeholder="Search items..."></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="search-btn" OnClick="btnSearch_Click" />
                        </div>

                        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemID"
                        OnRowCommand="gvItems_RowCommand" CssClass="grid-items">
                        <Columns>
                            <asp:BoundField DataField="ItemName" HeaderText="Item" />
                            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                            <asp:ButtonField Text="Add" CommandName="SelectItem" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>

                    <!-- CART GRID -->
                    <h2>Items to Checkout</h2>
                    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemID" 
                        OnRowCommand="gvCart_RowCommand" CssClass="gv-container">
                        <Columns>
                            <asp:BoundField DataField="ItemName" HeaderText="Item" />
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Qty") %>' CssClass="qty-input"
                                        AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="RemoveItem"
                                        CommandArgument='<%# Eval("ItemID") %>' CssClass="remove-btn" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                    <div>
                    <!-- TOTALS -->
                    <div class="totals-box">
                        <h3>Summary</h3>
                        <div class="total-line">
                            <span>Subtotal:</span>
                            <asp:Label ID="lblSubtotal" runat="server" Text="$0.00"></asp:Label>
                        </div>
                        <div class="total-line">
                            <span>Tax (5%):</span>
                            <asp:Label ID="lblTax" runat="server" Text="$0.00"></asp:Label>
                        </div>
                        <div class="grand-total">
                            <span>Total:</span>
                            <asp:Label ID="lblTotal" runat="server" Text="$0.00"></asp:Label>
                        </div>

                        <asp:Button ID="btnCheckout" runat="server" Text="Check Out" CssClass="checkout-btn" OnClick="btnCheckout_Click" />
                        <asp:Button ID="btnPrintReceipt" runat="server" Text="Print Receipt" CssClass="print-btn" OnClick="btnPrintReceipt_Click" />
                    </div>

                    <div id="receiptBox" runat="server" style="display:none;">
                        <asp:Literal ID="ltReceipt" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
