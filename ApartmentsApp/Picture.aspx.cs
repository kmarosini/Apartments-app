using DataLayer.Dal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{
    public partial class Picture : System.Web.UI.Page
    {
        private IList<DataLayer.Model.ApartmentPicture> _listOfAllPictures;

        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllPictures = ((DBRepo)Application["database"]).GetApartmentPictures(int.Parse(Session["SelectedApartment"].ToString()));


            foreach (var item in _listOfAllPictures)
            {
                if (item.Name == Session["SelectedValue"].ToString())
                {
                    Image.ImageUrl = "data:Image/png;base64," + Convert.ToBase64String(item.Base64Content);
                }
            }
        }
    }
}