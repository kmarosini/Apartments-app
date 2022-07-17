<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditApartment.aspx.cs" Inherits="ApartmentsApp.EditApartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <link href="Styles/ConfirmationPanel.css" rel="stylesheet" type="text/css" />
    
    <h3>Edit Apartment information</h3>

    <p><label for="Adress">Status:</label>
          <asp:DropDownList ID="ddlStatus" DataTextField="Name" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" DataValueField="Id" runat="server" AutoPostBack="true">

        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="StatusValidator" runat="server" ErrorMessage="Odaberite status apartmana*" ControlToValidate="ddlStatus"></asp:RequiredFieldValidator>
      </p>

     <p><label for="MaxAdults">MaxAdults:</label>
         <asp:TextBox ID="txtMaxAdults" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="MaxAdultsValidator" runat="server" ErrorMessage="Unesite broj MaxAdults*"  ControlToValidate="txtMaxAdults" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="MaxAdultsDataValidator" runat="server" Operator="DataTypeCheck" Type="integer" ForeColor="Red" ControlToValidate="txtMaxAdults" ErrorMessage="Unesite broj!*"></asp:CompareValidator>
      </p>

     <p><label for="MaxChildren">MaxChildren:</label>
         <asp:TextBox ID="txtMaxChildren" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="MaxChildrenValidator" runat="server" ControlToValidate="txtMaxChildren" ErrorMessage="Unesite broj MaxChildren*" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="MaxChildrenDataValidator" runat="server" Operator="DataTypeCheck" Type="integer" ForeColor="Red" ControlToValidate="txtMaxChildren" ErrorMessage="Unesite broj!*"></asp:CompareValidator>
      </p>

     <p><label for="TotalRooms">TotalRooms:</label>
         <asp:TextBox ID="txtTotalRooms" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="TotalRoomsValidator" runat="server" ControlToValidate="txtTotalRooms" ErrorMessage="Unesite broj TotalRooms*" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="TotalRoomsDataValidator" runat="server" Operator="DataTypeCheck" Type="integer" ForeColor="Red" ControlToValidate="txtTotalRooms" ErrorMessage="Unesite broj!*"></asp:CompareValidator>
      </p>

    <p><label for="BeachDistance">BeachDistance:</label>
         <asp:TextBox ID="txtBeachDistance" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="BeachDistanceValidator" runat="server" ControlToValidate="txtMaxChildren" ErrorMessage="Unesite BeachDistance*" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="BeachDistanceDataValidator" runat="server" Operator="DataTypeCheck" Type="integer" ForeColor="Red" ControlToValidate="txtBeachDistance" ErrorMessage="Unesite broj!*"></asp:CompareValidator>
      </p>

    <p><label for="Tags">Currently used tags:</label>
          <asp:DropDownList ID="ddlUsedTags" runat="server" AutoPostBack="true">

        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Odaberite status apartmana*" ControlToValidate="ddlStatus"></asp:RequiredFieldValidator>
      </p>

     <p><label for="MoreTags">Add more tags:</label>
          <asp:DropDownList ID="ddlMoreTags" runat="server" AutoPostBack="true">

        </asp:DropDownList>
         <asp:Button ID="btnAddNewTag" runat="server" Text="Add new Tag" OnClick="btnAddNewTag_Click" />
      </p>
       
    <asp:Button ID="btnSave" runat="server" Text="Update Apartment" onClick="btnSave_Click"/>
    <asp:Button ID="btnDeleteApartment" runat="server" Text="Delete Apartment" OnClick="btnDeleteApartment_Click"/>
    <asp:Button ID="btnImages" runat="server" Text="Upload image" OnClick="btnImages_Click" />
    <asp:Button ID="btnViewImages" runat="server" Text="View image" OnClick="btnViewImages_Click"/>
    
    <asp:Button ID="btnAddReservation" runat="server" Visible="false" Text="Add reservation" OnClick="btnAddReservation_Click"/>


    <asp:Panel ID="pnlAlert" CssClass="AlertModal" runat="server" Visible="false">
        <asp:Label ID="lblPotvrda" runat="server" Text="Želite li obrisati apartman?"></asp:Label>
                <asp:Button CssClass="YesButton" ForeColor="Red" ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" />
                <asp:Button CssClass="NoButton" ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click" />
    </asp:Panel>
    <asp:Panel Style="padding:20px" ID="pnlImage" runat="server" Visible="false">
        Select image: <asp:FileUpload ID="FileImage" runat="server" />
        <asp:Button Style="margin:20px" ID="btnAddImage" runat="server" Text="Add image" OnClick="btnAddImage_Click" /><asp:Label ID="lblImage" runat="server" Text=""></asp:Label>
        <asp:Image ID="Image" runat="server" />
    </asp:Panel>
    <asp:Panel Style="padding:20px" ID="pnlViewImages" runat="server" Visible="false">
        <asp:DropDownList ID="ddlImages" runat="server" AutoPostBack="true"  DataTextField=""></asp:DropDownList>
        <asp:Button ID="bntView" runat="server" Text="View" OnClick="bntView_Click" />
        <asp:Button ID="btnRepresentative" runat="server" Text="Set as Representative" OnClick="btnRepresentative_Click" />
    </asp:Panel>
</asp:Content>
