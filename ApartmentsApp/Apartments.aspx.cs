using DataLayer.Dal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{
    public partial class Apartments : System.Web.UI.Page
    {
        private IList<DataLayer.Model.Apartment> _listOfAllApartments;
        private IList<DataLayer.Model.City> _listOfAllCities;
        private IList<DataLayer.Model.ApartmentStatus> _listOfAllStatus;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllApartments = ((DBRepo)Application["database"]).LoadApartments();
            _listOfAllCities = ((DBRepo)Application["database"]).LoadCities();
            _listOfAllStatus = ((DBRepo)Application["database"]).LoadApartmentStatus();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptApartmentTable.DataSource = _listOfAllApartments;
            rptApartmentTable.DataBind();

            ddlCity.DataSource = _listOfAllCities;
            ddlCity.DataBind();

            ddlStatus.DataSource = _listOfAllStatus;
            ddlStatus.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateApartment.aspx");
        }

        protected void btnOpen_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Open")
            {
                Session["SelectedApartment"] = e.CommandArgument.ToString(); 
                Response.Redirect("EditApartment.aspx");
            }
            
        }

        protected void btnSort_Click(object sender, EventArgs e)
        {
            List<DataLayer.Model.Apartment> list = ((DBRepo)Application["database"]).LoadApartments().ToList();
            if (rbAsc.Checked == true)
            {
                switch (ddlSorting.SelectedValue)
                {
                    case "Broj Soba":
                        list.Sort((x,y) => x.TotalRooms.CompareTo(y.TotalRooms));
                        rptApartmentTable.DataSource = list;
                        rptApartmentTable.DataBind();
                        break;
                    case "Broj Mjesta":
                        list.Sort((x, y) => x.MaxAdults.CompareTo(y.MaxAdults));
                        rptApartmentTable.DataSource = list;
                        rptApartmentTable.DataBind();
                        break;
                    case "Cijena":
                        list.Sort((x,y) => x.Price.CompareTo(y.Price));
                        rptApartmentTable.DataSource = list;
                        rptApartmentTable.DataBind();
                        break;
                    default:
                        break;
                }     
            }

            if (rbDesc.Checked == true)
            {
                switch (ddlSorting.SelectedValue)
                {
                    case "Broj Soba":
                        list.Sort((x, y) => -x.TotalRooms.CompareTo(y.TotalRooms));
                        rptApartmentTable.DataSource = list;
                        rptApartmentTable.DataBind();
                        break;
                    case "Broj Mjesta":
                        list.Sort((x, y) => -x.MaxAdults.CompareTo(y.MaxAdults));
                        rptApartmentTable.DataSource = list;
                        rptApartmentTable.DataBind();
                        break;
                    case "Cijena":
                        list.Sort((x, y) => -x.Price.CompareTo(y.Price));
                        rptApartmentTable.DataSource = list;
                        rptApartmentTable.DataBind();
                        break;
                    default:
                        break;
                }
            }


        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<DataLayer.Model.Apartment> list = ((DBRepo)Application["database"]).LoadApartments().ToList();
            switch (ddlStatus.SelectedValue)
            {
                case "1":
                    List<DataLayer.Model.Apartment> apartmani = list.FindAll((x)=> x.StatusId == 1 && x.CityId == Convert.ToInt32(ddlCity.SelectedValue));
                    rptApartmentTable.DataSource= apartmani;
                    rptApartmentTable.DataBind();
                    break;
                case "3":
                    List<DataLayer.Model.Apartment> apartmani_two = list.FindAll((x) =>  x.StatusId == 3 && x.CityId == Convert.ToInt32(ddlCity.SelectedValue));
                    rptApartmentTable.DataSource = apartmani_two;
                    rptApartmentTable.DataBind();
                    break;
                case "2":
                    List<DataLayer.Model.Apartment> apartmani_three = list.FindAll((x) => x.StatusId == 2 && x.CityId == Convert.ToInt32(ddlCity.SelectedValue));
                    rptApartmentTable.DataSource = apartmani_three;
                    rptApartmentTable.DataBind();
                    break;
                default:
                    break;
            }
        }
    }
}