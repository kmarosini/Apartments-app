<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Apartments.aspx.cs" Inherits="ApartmentsApp.Apartments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <label>Status: </label>
    <asp:DropDownList ID="ddlStatus" DataTextField="Name" DataValueField="Id" runat="server">

        </asp:DropDownList>
    <label>City: </label>
    <asp:DropDownList ID="ddlCity" DataTextField="Name" DataValueField="Id" runat="server">

        </asp:DropDownList>
    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />
    <label>Sort by: </label>
    <asp:DropDownList ID="ddlSorting" runat="server">
        <asp:ListItem>Broj Soba</asp:ListItem>
        <asp:ListItem>Broj Mjesta</asp:ListItem>
        <asp:ListItem>Cijena</asp:ListItem>

        </asp:DropDownList>
    <asp:RadioButton GroupName="sorting" Text="ASC"  ID="rbAsc" runat="server" />
    <asp:RadioButton GroupName="sorting" ID="rbDesc" Text="DESC" runat="server" />
    <asp:Button ID="btnSort" runat="server" Text="Sort" OnClick="btnSort_Click" />
    <asp:Repeater ID="rptApartmentTable" runat="server">
        <HeaderTemplate></script>
            <table id="mySortingTable" class="table table-striped">
                <tr style="border-bottom:2px solid black;align-content:center;">
                    <th>Id</th>
                    <th>ApartmentName</th>
                    <th>City</th>
                    <th>Max Adults</th>
                    <th>Max Children</th>
                    <th>Total Rooms</th>
                    <th>Pictures</th>
                    <th>Beach Distance</th>
                    <th>Price</th>
                    <th>Status</th>
                </tr>   
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="justify-content:center;"><%# Eval("Id") %></td>
                <td style="justify-content:center;"><%# Eval("ApartmentName") %></td>
                <td style="justify-content:center;"><%# Eval("Name") %></td>
                <td><%# Eval("MaxAdults") %></td>
                <td><%# Eval("MaxChildren") %></td>
                <td><%# Eval("TotalRooms") %></td>
                <td><%# Eval("Ukupno") %></td>
                <td><%# Eval("BeachDistance") %>m</td>
                <td><%# Eval("Price") %>€</td>
                <td><%# Eval("StatusName") %></td>
                <td>
                    <asp:Button ID="btnOpen" runat="server" Text="Open" OnCommand="btnOpen_Command" CommandName="Open" CommandArgument='<%# Eval("Id")%>' />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
</asp:Content>
