<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateApartment.aspx.cs" Inherits="ApartmentsApp.CreateApartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Apartment Information</h3>

        <p><label for="OwnerId">Owner:</label>
         <asp:DropDownList DataTextField="Name" DataValueField="Id" ID="ddlOwner" runat="server">
        
        </asp:DropDownList>
      </p>

        <p><label for="TypeId">TypeId:</label>
         <asp:TextBox ID="txtTypeId" runat="server"></asp:TextBox>
      </p>

        <p><label for="StatusId">Status:</label>
         <asp:DropDownList DataTextField="Name" DataValueField="Id" ID="ddlStatus" runat="server">
        
        </asp:DropDownList>
      </p>

    <p><label for="CityId">City:</label>
        <asp:DropDownList DataTextField="Name" DataValueField="Id" ID="ddlCity" runat="server">
        
        </asp:DropDownList>
      </p>

    <p><label for="Adress">Adress:</label>
         <asp:TextBox ID="txtAdress" runat="server"></asp:TextBox>
      </p>

    <p><label for="Name">Name:</label>
         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
      </p>

    <p><label for="NameEng">NameEng:</label>
         <asp:TextBox ID="txtNameEng" runat="server"></asp:TextBox>
      </p>

    <p><label for="Price">Price:</label>
         <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
      </p>

     <p><label for="MaxAdults">MaxAdults:</label>
         <asp:TextBox ID="txtMaxAdults" runat="server"></asp:TextBox>
      </p>

     <p><label for="MaxChildren">MaxChildren:</label>
         <asp:TextBox ID="txtMaxChildren" runat="server"></asp:TextBox>
      </p>

     <p><label for="TotalRooms">TotalRooms:</label>
         <asp:TextBox ID="txtTotalRooms" runat="server"></asp:TextBox>
      </p>

    <p><label for="BeachDistance">BeachDistance:</label>
         <asp:TextBox ID="txtBeachDistance" runat="server"></asp:TextBox>
      </p>

    <p>
        Select image: <asp:FileUpload ID="FileImage" runat="server" />
        <asp:Label ID="lblImage" runat="server" Text=""></asp:Label>
        <asp:Image ID="Image" runat="server" />

    </p>
        

    <asp:Button ID="btnAdd" runat="server" Text="Add Apartment" OnClick="btnAdd_Click" />

</asp:Content>
