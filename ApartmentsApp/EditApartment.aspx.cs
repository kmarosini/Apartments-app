using DataLayer.Dal;
using DataLayer.Model;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{
    public partial class EditApartment : System.Web.UI.Page
    {
        private IList<DataLayer.Model.ApartmentStatus> _listOfAllStatus;
        private IList<DataLayer.Model.Tags> _listOfUsedTags;
        private IList<DataLayer.Model.Tags> _listOfUnusedTags;
        private IList<DataLayer.Model.ApartmentTags> _listOfAllTags;
        private IList<DataLayer.Model.ApartmentPicture> _listOfAllPictures;


        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllStatus = ((DBRepo)Application["database"]).LoadApartmentStatus();
            _listOfUsedTags = ((DBRepo)Application["database"]).GetUsedTags(int.Parse(Session["SelectedApartment"].ToString()));
            _listOfUnusedTags = ((DBRepo)Application["database"]).GetUnusedTags(int.Parse(Session["SelectedApartment"].ToString()));
            _listOfAllTags = ((DBRepo)Application["database"]).LoadApartmentTags();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            ddlStatus.DataSource = _listOfAllStatus;
            ddlStatus.DataBind();
            LoadApartmentInfo();

            ddlUsedTags.DataSource = _listOfUsedTags;
            ddlUsedTags.DataBind();

            ddlMoreTags.DataSource = _listOfUnusedTags;
            ddlMoreTags.DataBind();
        }

        private void LoadApartmentInfo()
        {
            var data = RepoFactory.GetRepo().getApartmentById(int.Parse(Session["SelectedApartment"].ToString()));
 
            txtMaxAdults.Text = data.MaxAdults.ToString();
            txtMaxChildren.Text = data.MaxChildren.ToString();
            txtTotalRooms.Text = data.TotalRooms.ToString();
            txtBeachDistance.Text = data.BeachDistance.ToString();
            ddlStatus.SelectedValue = data.StatusId.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            RepoFactory.GetRepo().UpdateApartment(new DataLayer.Model.Apartment
            {
                Id = int.Parse(Session["SelectedApartment"].ToString()),
                BeachDistance = Convert.ToInt32(txtBeachDistance.Text),
                MaxAdults = Convert.ToInt32(txtMaxAdults.Text),
                MaxChildren = Convert.ToInt32(txtMaxChildren.Text),
                StatusId = Convert.ToInt32(ddlStatus.SelectedValue),
                TotalRooms = Convert.ToInt32(txtTotalRooms.Text)
            }); 
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 2 || Convert.ToInt32(ddlStatus.SelectedValue) == 1)
            {
                btnAddReservation.Visible = true;
            }
            else
            {
                btnAddReservation.Visible = false;
            }
        }

        protected void btnAddReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reservations.aspx");
        }

        protected void btnDeleteApartment_Click(object sender, EventArgs e)
        {
            pnlAlert.Visible = true;  
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            RepoFactory.GetRepo().SoftDelete(int.Parse(Session["SelectedApartment"].ToString()));
            Response.Redirect("Apartments.aspx");

        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            pnlAlert.Visible = false;
        }

        protected void btnAddNewTag_Click(object sender, EventArgs e)
        {
            RepoFactory.GetRepo().InsertIntoTags(int.Parse(Session["SelectedApartment"].ToString()), CheckTagId());
            Response.Redirect("EditApartment.aspx");
        }

        private int CheckTagId()
        {
            int id = 0;
            foreach (var item in _listOfAllTags)
            {
                if (item.name == ddlMoreTags.SelectedValue)
                {
                    id += item.Id;
                }
            }
            return id;
        }

        protected void btnImages_Click(object sender, EventArgs e)
        {
            pnlImage.Visible = true;
        }

        protected void btnAddImage_Click(object sender, EventArgs e)
        {

            HttpPostedFile postedFile = FileImage.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(postedFile.FileName);
            int fileSize = postedFile.ContentLength;


            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

                RepoFactory.GetRepo().SavePicture(new DataLayer.Model.ApartmentPicture
                {
                    ApartmentId = int.Parse(Session["SelectedApartment"].ToString()),
                    Base64Content = bytes,
                    Name = fileName
                });
            }
        }

        protected void btnViewImages_Click(object sender, EventArgs e)
        {
            pnlViewImages.Visible = true;
            _listOfAllPictures = ((DBRepo)Application["database"]).GetApartmentPictures(int.Parse(Session["SelectedApartment"].ToString()));

            ddlImages.DataSource = _listOfAllPictures;
            ddlImages.DataBind();
        }


        protected void bntView_Click(object sender, EventArgs e)
        {
            Session["SelectedValue"] = ddlImages.SelectedValue;
            Response.Redirect("Picture.aspx");
        }

        protected void btnRepresentative_Click(object sender, EventArgs e)
        {
            _listOfAllPictures = ((DBRepo)Application["database"]).GetApartmentPictures(int.Parse(Session["SelectedApartment"].ToString()));

            RepoFactory.GetRepo().SetAsRepresentative(int.Parse(Session["SelectedApartment"].ToString()), GetPictureId());

        }

        private int GetPictureId()
        {
            int id = 0;
            foreach (var item in _listOfAllPictures)
            {
                if (item.Name == ddlImages.SelectedValue)
                {
                    id += item.Id;
                }
            }
            return id;
        }
    }
}
 