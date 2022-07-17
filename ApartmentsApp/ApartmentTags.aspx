<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentTags.aspx.cs" Inherits="ApartmentsApp.Tags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Repeater ID="rptTags" runat="server">
                    <HeaderTemplate>
                                <h1>Tags</h1>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <ul>
                            <li><%# Eval(nameof(DataLayer.Model.ApartmentTags.name)) %> <%# $"({Eval(nameof(DataLayer.Model.ApartmentTags.ukupno))})"%> <asp:Button ID="btnDelete" OnCommand="btnDelete_Command" CommandName="Delete" Visible='<%# DeleteButtonVisibility(Eval(nameof(DataLayer.Model.ApartmentTags.ukupno)).ToString()) %>' CommandArgument='<%# Eval("Id") %>' runat="server" Text="Delete"/></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
