using DataLayer.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{
    public partial class Tags : System.Web.UI.Page
    {
        private IList<DataLayer.Model.ApartmentTags> _listOfAllTags;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllTags = ((DBRepo)Application["database"]).LoadApartmentTags();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptTags.DataSource = _listOfAllTags;
            rptTags.DataBind();
        }

        public bool DeleteButtonVisibility(string totalCount)
        {
            if (Convert.ToInt32(totalCount) == 0)
            {
                return true;
            }
            return false;
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string ID = e.CommandArgument.ToString();

                RepoFactory.GetRepo().DeleteTag(Convert.ToInt32(ID));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateTag.aspx");
        }
    }
}