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
    public partial class CreateTag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RepoFactory.GetRepo().CreateTag(new DataLayer.Model.Tags{
                Name = txtName.Text,
                NameEng = txtNameEng.Text,
                TypeId = Convert.ToInt32(txtTypeId.Text),
                Guid = System.Guid.NewGuid(),
                CreatedAt = DateTime.Now
            });

            Response.Write("<script>alert('Tag added successful');</script>");


        }
    }
}