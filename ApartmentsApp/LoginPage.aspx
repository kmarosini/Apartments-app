<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="ApartmentsApp.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="mb-3">
               <asp:Label ID="lblUsername" for="txtUsername" class="form-label" meta:resourcekey="lblUsername" runat="server" Text="Username"></asp:Label>
               <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                    <asp:Label ID="lblPassword" for="txtPassword" class="form-label" meta:resourcekey="lblPassword" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
            </div>
          <asp:Button ID="btnLogin" meta:resourcekey="btnLogin" class="btn btn-primary" runat="server" Text="Login" OnClick="btnLogin_Click"/>
            <asp:Label ID="lblErrorMessage" runat="server" Text="Incorrect user credentials" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
