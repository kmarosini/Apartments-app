<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateTag.aspx.cs" Inherits="ApartmentsApp.CreateTag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    //[Id], [Guid], [CreatedAt], [TypeId], [Name], [NameEng]
  
      <h3>Tag Information</h3>

        <p><label for="TypeId">TypeId:</label>
         <asp:TextBox ID="txtTypeId" runat="server"></asp:TextBox>
      </p>

        <p><label for="Name">Name:</label>
         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
      </p>

        <p><label for="NameEng">NameEng:</label>
         <asp:TextBox ID="txtNameEng" runat="server"></asp:TextBox>
      </p>

      <p><asp:Button ID="btnAdd" runat="server" Text="Add Tag" OnClick="btnAdd_Click" /></p>
  

</asp:Content>
