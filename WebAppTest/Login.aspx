<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAppTest.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
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

    #btnLogin {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 4px;
            cursor: pointer;
            width: 10%;
            margin-top: 10px;
        }

</style>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Logg in</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <div>
                <asp:Label ID="lblUsername" runat="server" Text="Brukernavn:"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblPassword" runat="server" Text="Passord:"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
                
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Logg inn" OnClick="btnLogin_Click" />
            </div>
        </div><br />
            <a href="default.aspx" style="margin-left: 20px; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px;">Hjem</a>
    </form>
</body>
</html>