<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppTest.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
    <style>
    p {
        text-align: center;
    }

    h1 {
        font-size: 2.5rem;
        background-color: #0094ff;
        color: white;
        margin: 0 auto;
        padding: 1rem;
        width: 100%;
        max-width: 1200px;
        text-align: center;
        border-radius: 8px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    }

    h2 {
        text-align: center;
        font-size: 25px;
    }

    h3 {
        text-align: center;
        font-size: 20px;
    }

    .gray-box {
        background-color: gray;
        width: 100%;
    }

    body {
        text-align: center;
        background-color: lightgrey;
    }

    table {
        border-collapse: collapse;
        min-width: 350px;
        font-family: Arial, sans-serif;
        margin-left: auto;
        margin-right: auto;
        max-width: 500px;
    }

    th, td {
        text-align: left;
        padding: 8px;
    }

    tr:nth-child(even) {
        background-color: #f2f2f2
    }

    th {
        background-color: #0094ff;
        color: white;
    }
</style>
<body>
    
    <div style="display: flex; align-items: center;">
    <h1>Glemmen Kantine</h1>
    <a href="Login.aspx" style="margin-left: 20px; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px;">Logg inn</a>
</div>
    

    <h2>Ukesmeny</h2>
    <form id="form1" runat="server">
        <h3>Mandag</h3>
        <asp:Label ID="LabelMan" runat="server" Text=""></asp:Label>
        <br />
        <h3>Tirsdag</h3>
        <asp:Label ID="LabelTir" runat="server" Text=""></asp:Label>
        <br />
        <h3>Onsdag</h3>
        <asp:Label ID="LabelOns" runat="server" Text=""></asp:Label>
        <br />
        <h3>Torsdag</h3>
        <asp:Label ID="LabelTor" runat="server" Text=""></asp:Label>
        <br />
        <h3>Fredag</h3>
        <asp:Label ID="LabelFre" runat="server" Text=""></asp:Label>

        <h2 style="margin-top: 80px;">Faste varer</h2>

        <p>
            <asp:ListView ID="lvFastevarer" runat="server" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                <LayoutTemplate>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <th>Vare</th>
                            <th>Pris</th>
                            <th></th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td><%# Eval("Vare") %></td>
                    <td><%# Eval("Pris") %></td>
                    <td>Kr</td>
                </ItemTemplate>
            </asp:ListView>

        </p>


    </form>

    <p>
        &nbsp;
    </p>

    <p>
        &nbsp;
    </p>

</body>

</html>