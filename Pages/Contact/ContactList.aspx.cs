using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.Contact
{
    public partial class ContactList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGridView();
            }
        }

        #region Fill Grid View
        private void FillGridView()
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.CommandText = "PR_Contact_SelectAll";

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                gvContact.DataSource = objSDR;
                gvContact.DataBind();
            }
            else
                lblMessage.Text = "No data found.";

            objConn.Close();
        }
        #endregion

        #region Contact Row Command
        protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteContact")
            {
                if (e.CommandArgument != "")
                {
                    DeleteContact(e.CommandArgument.ToString().Trim());
                }
            }
        }
        #endregion

        #region Delete Contact
        private void DeleteContact(String contactID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.CommandText = "PR_Contact_DeleteByPK";

            objCmd.Parameters.AddWithValue("@ContactID", contactID);
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

            objCmd.ExecuteNonQuery();

            objConn.Close();


            Response.Redirect("~/Pages/Contact/ContactList.aspx");

            FillGridView();
        }
        #endregion
    }
}