using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApartmentsApp
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "123" && txtUsername.Text == "admin")
            {
                Session["New"] = txtUsername.Text;
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblErrorMessage.Visible = true;
            }
        }
    }
}