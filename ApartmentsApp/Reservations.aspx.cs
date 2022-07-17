using DataLayer.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{


    public partial class Reservations : System.Web.UI.Page
    {

        private IList<DataLayer.Model.AspNetUsers> _listOfAllRegisteredUsers;
        private IList<DataLayer.Model.AspNetUsers> _listOfAllUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllRegisteredUsers = ((DBRepo)Application["database"]).LoadRegisteredUsers();
            _listOfAllUsers = ((DBRepo)Application["database"]).LoadAspNetUsers();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
           ddlRegisteredUsers.DataSource = _listOfAllRegisteredUsers;
           ddlRegisteredUsers.DataBind();
        }

        protected void bntNotRegisteredUser_Click(object sender, EventArgs e)
        {
            pnlNotRegisteredUser.Visible = true;
        }

        protected void btnAddReservation_Click(object sender, EventArgs e)
        {
            RepoFactory.GetRepo().InsertNotRegisteredResevation(new DataLayer.Model.ApartmentReservation
            {
                ApartmentId = int.Parse(Session["SelectedApartment"].ToString()),
                Details = txtDetails.Text,
                UserId = 0,
                UserName = txtUserName.Text,
                UserEmail = txtUserAddress.Text,
                UserPhone = txtUserPhone.Text,
                UserAdress = txtUserAddress.Text
            });

            Response.Write("<script>alert('Reservation added successful');</script>");
        }

        protected void btnRegisteredUser_Click(object sender, EventArgs e)
        {
            pnlRegisteredUsers.Visible = true;
        }

        protected void btnAddRegisteredUserReservation_Click(object sender, EventArgs e)
        {
            RepoFactory.GetRepo().InsertNotRegisteredResevation(new DataLayer.Model.ApartmentReservation
            {
                ApartmentId = int.Parse(Session["SelectedApartment"].ToString()),
                Details = txtRegisteredUserDetails.Text,
                UserId = GetUserId(),
                UserName = ddlRegisteredUsers.SelectedValue,
                UserEmail = GetEmail(),
                UserPhone = GetPhoneNumber(),
                UserAdress = GetAdrress()
            });

            //GetEmail();
            //GetPhoneNumber();
            //GetAdrress();
            //GetUserId();
            //GetUserName();
        }

        private int GetUserId()
        {
            int id = 0;
            foreach (var item in _listOfAllUsers)
            {
                if (item.UserName == ddlRegisteredUsers.SelectedValue)
                {
                    id += item.Id;
                    break;
                }
            }
            return id;
        }

        private string GetAdrress()
        {
            string adresa = "";
            foreach (var item in _listOfAllUsers)
            {
                if (item.UserName == ddlRegisteredUsers.SelectedValue)
                {
                    adresa += item.Address;
                }
            }
            return adresa;
        }

        private string GetPhoneNumber()
        {
            string PhoneNumber = "";
            foreach (var item in _listOfAllUsers)
            {
                if (item.UserName == ddlRegisteredUsers.SelectedValue)
                {
                    PhoneNumber += item.PhoneNumber;
                }
            }
            return PhoneNumber;
        }

        private string GetEmail()
        {
            string email = "";
            foreach (var item in _listOfAllUsers)
            {
                if (item.UserName == ddlRegisteredUsers.SelectedValue)
                {
                    email += item.Email;
                }
            }
            return email;
        }
    }
}