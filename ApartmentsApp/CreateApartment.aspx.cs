using DataLayer.Dal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{
    public partial class CreateApartment : System.Web.UI.Page
    {
        private IList<DataLayer.Model.City> _listOfAllCities;
        private IList<DataLayer.Model.ApartmentOwner> _listOfAllOwners;
        private IList<DataLayer.Model.ApartmentStatus> _listAllStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllCities = ((DBRepo)Application["database"]).LoadCities();
            _listOfAllOwners = ((DBRepo)Application["database"]).LoadApartmentOwner();
            _listAllStatus = ((DBRepo)Application["database"]).LoadApartmentStatus();

            ddlCity.DataSource = _listOfAllCities;
            ddlCity.DataBind();

            ddlOwner.DataSource = _listOfAllOwners;
            ddlOwner.DataBind();

            ddlStatus.DataSource = _listAllStatus;
            ddlStatus.DataBind();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
           
            int createdApartment = RepoFactory.GetRepo().CreateApartment(new DataLayer.Model.Apartment
            {
                Name = txtName.Text,
                NameEng = txtNameEng.Text,
                Guid = System.Guid.NewGuid(),
                OwnerId = Convert.ToInt32(ddlOwner.SelectedValue),
                CityId = Convert.ToInt32(ddlCity.SelectedValue),
                BeachDistance = Convert.ToInt32(txtBeachDistance.Text),
                MaxAdults = Convert.ToInt32(txtMaxAdults.Text),
                MaxChildren = Convert.ToInt32(txtMaxChildren.Text),
                DeletedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                Adress = txtAdress.Text,
                Price = Convert.ToInt32(txtPrice.Text),
                TotalRooms = Convert.ToInt32(txtTotalRooms.Text),
                StatusId = Convert.ToInt32(ddlStatus.SelectedValue),
                TypeId = Convert.ToInt32(txtTypeId.Text)

            });

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
                    ApartmentId = createdApartment,
                    Base64Content = bytes,
                    Name = fileName
                });
            }
        }

    }
}