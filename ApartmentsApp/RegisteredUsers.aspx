<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisteredUsers.aspx.cs" Inherits="ApartmentsApp.RegisteredUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rptRegisteredUsers" runat="server">
        <HeaderTemplate></script>
            <table class="table table-striped">
                <tr style="border-bottom:2px solid black;align-content:center;">
                    <th>Id</th>
                    <th>Email</th>
                    <th>Phone Number</th>
                    <th>Username</th>
                    <th>Adress</th>
                </tr>   
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="justify-content:center;"><%# Eval("Id") %></td>
                <td style="justify-content:center;"><%# Eval("Email") %></td>
                <td style="justify-content:center;"><%# Eval("PhoneNumber") %></td>
                <td><%# Eval("UserName") %></td>
                <td><%# Eval("Address") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
