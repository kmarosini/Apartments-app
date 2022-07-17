<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reservations.aspx.cs" Inherits="ApartmentsApp.Reservations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="bntNotRegisteredUser" OnClick="bntNotRegisteredUser_Click" runat="server" Text="Not-registered user" />
    <asp:Button ID="btnRegisteredUser" runat="server" Text="Registered user" OnClick="btnRegisteredUser_Click" />

    <asp:Panel ID="pnlNotRegisteredUser" Visible="false" runat="server">
        <h3>Not Registered user</h3>

        <p><label for="Details">Details:</label>
         <asp:TextBox ID="txtDetails" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="DetailsValidator" runat="server" ErrorMessage="Unesite detalje*"  ControlToValidate="txtDetails" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="DetailsDataValidator" runat="server" Operator="DataTypeCheck" Type="string" ForeColor="Red" ControlToValidate="txtDetails" ErrorMessage="Unesite string!*"></asp:CompareValidator>
      </p>

        <p><label for="UserName">Username:</label>
         <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="UserNameValidator" runat="server" ErrorMessage="Unesite username*"  ControlToValidate="txtUserName" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="UserNameDataValidator" runat="server" Operator="DataTypeCheck" Type="string" ForeColor="Red" ControlToValidate="txtUserName" ErrorMessage="Unesite string!*"></asp:CompareValidator>
      </p>

        <p><label for="UserEmail">Email:</label>
         <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="EmailValidator" runat="server" ErrorMessage="Unesite email*"  ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmailExpressionValidator" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" runat="server" ErrorMessage="Unesite email adresu*" ForeColor="Red"></asp:RegularExpressionValidator>
        </p>

        <p><label for="UserPhone">Phone number:</label>
         <asp:TextBox ID="txtUserPhone" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="UserPhoneValidator" runat="server" ErrorMessage="Unesite broj telefona*"  ControlToValidate="txtUserPhone" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="UserPhoneDataValidator" runat="server" Operator="DataTypeCheck" Type="string" ForeColor="Red" ControlToValidate="txtUserPhone" ErrorMessage="Unesite string!*"></asp:CompareValidator>
      </p>

        
        <p><label for="UserAddress">Adresa:</label>
         <asp:TextBox ID="txtUserAddress" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="UserAddressValidator" runat="server" ErrorMessage="Unesite adresu*"  ControlToValidate="txtUserAddress" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="UserAddressDataValidator" runat="server" Operator="DataTypeCheck" Type="string" ForeColor="Red" ControlToValidate="txtUserAddress" ErrorMessage="Unesite string!*"></asp:CompareValidator>
      </p>

        <asp:Button ID="btnAddReservation" runat="server" Text="Add reservation" OnClick="btnAddReservation_Click" />
    </asp:Panel>
    

    <asp:Panel ID="pnlRegisteredUsers" Visible="false" runat="server">
        <h3>Registered user</h3>

        <asp:DropDownList ID="ddlRegisteredUsers" runat="server" AutoPostBack="false" >

        </asp:DropDownList>
        <p><label for="Details">Details:</label>
         <asp:TextBox ID="txtRegisteredUserDetails" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RegisteredUserDetailsValidator" runat="server" ErrorMessage="Unesite detalje*"  ControlToValidate="txtRegisteredUserDetails" ForeColor="Red"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="RegisteredUserDetailsDataValidator" runat="server" Operator="DataTypeCheck" Type="string" ForeColor="Red" ControlToValidate="txtRegisteredUserDetails" ErrorMessage="Unesite string!*"></asp:CompareValidator>
      </p>
        <asp:Button ID="btnAddRegisteredUserReservation" runat="server" Text="Add reservation" OnClick="btnAddRegisteredUserReservation_Click" />
    </asp:Panel>

</asp:Content>
