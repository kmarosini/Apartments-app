using DataLayer.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{
    public partial class RegisteredUsers : System.Web.UI.Page
    {
        private IList<DataLayer.Model.AspNetUsers> _listOfAllUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllUsers = ((DBRepo)Application["database"]).LoadAspNetUsers();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptRegisteredUsers.DataSource = _listOfAllUsers;
            rptRegisteredUsers.DataBind();
        }

    }
}
