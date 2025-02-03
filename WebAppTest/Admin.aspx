<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="WebAppTest.Admin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin - Edit Menu</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            padding: 20px;
        }
        h1 {
            color: #333;
        }
        .grid-view {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .grid-view th, .grid-view td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .grid-view th {
            background-color: #007bff;
            color: white;
        }
        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .grid-view tr:hover {
            background-color: #f1f1f1;
        }
        .btn-update {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 4px;
        }
        .btn-update:hover {
            background-color: #218838;
        }
        .section {
            margin-bottom: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Edit Menu Items (retter) -->
            <a href="Default.aspx" style="margin-left: 20px; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px;">Hjem</a>
            <div class="section">
                <h1>Edit Menu Items</h1>
                <asp:GridView ID="GridViewRetter" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowEditing="GridViewRetter_RowEditing" OnRowCancelingEdit="GridViewRetter_RowCancelingEdit" OnRowUpdating="GridViewRetter_RowUpdating" CssClass="grid-view">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="rett" HeaderText="Item Name" />
                        <asp:BoundField DataField="pris" HeaderText="Price" DataFormatString="{0:C}" />
                        <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Rediger" UpdateText="Oppdater" CancelText="Avbryt" ControlStyle-CssClass="btn-update" />
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Edit Weekly Menu (ukesmeny) -->
            <div class="section">
                <h1>Edit Weekly Menu</h1>
                <asp:GridView ID="GridViewUkesmeny" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowEditing="GridViewUkesmeny_RowEditing" OnRowCancelingEdit="GridViewUkesmeny_RowCancelingEdit" OnRowUpdating="GridViewUkesmeny_RowUpdating" CssClass="grid-view">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="mandag" HeaderText="Monday" />
                        <asp:BoundField DataField="tirsdag" HeaderText="Tuesday" />
                        <asp:BoundField DataField="onsdag" HeaderText="Wednesday" />
                        <asp:BoundField DataField="torsdag" HeaderText="Thursday" />
                        <asp:BoundField DataField="fredag" HeaderText="Friday" />
                        <asp:BoundField DataField="uke" HeaderText="Week" />
                        <asp:CheckBoxField DataField="denne_uken" HeaderText="Current Week" />
                        <asp:CommandField ShowEditButton="True" ButtonType="Button" EditText="Rediger" UpdateText="Oppdater" CancelText="Avbryt" ControlStyle-CssClass="btn-update" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>