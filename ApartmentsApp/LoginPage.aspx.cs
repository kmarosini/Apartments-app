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
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-958MSQ8\SQLEXPRESS2;Initial Catalog=RwaApartmani;Integrated Security=True");
                conn.Open();

                string checkUser = "select * from LoginDB where username='" + txtUsername.Text + "'";
                SqlCommand cmd = new SqlCommand(checkUser, conn);
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                conn.Close();
                if (temp == 1)
                {
                    conn.Open();
                    string checkPassword = "select pass from LoginDB where  username='" + txtUsername.Text + "'";
                    SqlCommand passCmd = new SqlCommand(checkPassword, conn);
                    string password = passCmd.ExecuteScalar().ToString();
                    if (password == txtPassword.Text)
                    {
                        Session["New"] = txtUsername.Text;
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {

                        lblErrorMessage.Visible = true;
                    }
                }
                else
                {
                    lblErrorMessage.Visible = true;
                }
            }
            catch (Exception)
            {

                lblErrorMessage.Visible = true;
            }
        }
    }
}