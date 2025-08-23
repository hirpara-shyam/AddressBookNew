using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.ContactCategory
{
    public partial class ContactCategoryAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ContactCategoryID"] != null)
                {
                    lblAddEdit.Text = "Edit Contact Category";
                    FillData(Request.QueryString["ContactCategoryID"].ToString().Trim());
                }
                else
                    lblAddEdit.Text = "Add Contact Category";
            }
        }

        private void FillData(SqlString ContactCategoryID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_ContactCategory_SelectByPK]";

                cmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.Read())
                {
                    txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString();
                }
                else
                {
                    lblMessage.Text = "No data found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text += ex.Message;
            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlString countryName = SqlString.Null;
            String errMessage = "";

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (txtContactCategoryName.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter Contact Category name <br/>";
                }
                else
                {
                    countryName = txtContactCategoryName.Text.ToString().Trim();
                }

                if (errMessage.Trim() != "")
                {
                    lblMessage.Text = "Kindly solve Following error(s) <br/>" + errMessage;
                }

                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ContactCategoryName", countryName);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                if (Request.QueryString["ContactCategoryID"] != null)
                {
                    cmd.Parameters.AddWithValue("@ContactCategoryID", Request.QueryString["ContactCategoryID"].ToString().Trim());
                    cmd.CommandText = "[PR_ContactCategory_Update]";

                    cmd.ExecuteNonQuery();

                    Response.Redirect("~/Pages/ContactCategory/ContactCategoryList.aspx");
                }
                else
                {
                    cmd.CommandText = "[PR_ContactCategory_Insert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Contact Category Inserted Successfully.";

                    txtContactCategoryName.Text = "";
                    txtContactCategoryName.Focus();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text += ex.Message;
            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }
    }
}