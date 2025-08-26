using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Content
{
    public partial class NewAddressBook : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                lblUserName.Text = Session["UserName"].ToString().Trim();
                imgProfile.ImageUrl = Session["ProfilePath"].ToString().Trim();
            }
            else
            {
                Response.Redirect("~/Pages/Login", true);
            }
        }

        protected void hlLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("~/Pages/Login", true);
        }

        protected void lbtnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Profile", true);
        }
    }
}