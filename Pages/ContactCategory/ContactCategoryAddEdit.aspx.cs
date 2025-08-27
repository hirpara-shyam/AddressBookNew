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
        #region xml for Data Records
        private void xmlData()
        {
            ViewState["ContactCategoryRecordsXml"] = "";
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                xmlData();

                if (Page.RouteData.Values["ContactCategoryID"] != null)
                {
                    lblAddEdit.Text = "Edit Contact Category";
                    btnAddMore.Visible = false;
                    FillData(EncryptDecrypt.Decrypt(Page.RouteData.Values["ContactCategoryID"].ToString().Trim()));
                }
                else
                    lblAddEdit.Text = "Add Contact Category";
            }
        }
        #endregion

        #region Fill Data for Edit
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
                    lblMessage.Attributes.Add("class", "text-info");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text += ex.Message;
                lblMessage.Attributes.Add("class", "text-danger");
            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }
        #endregion

        #region Save button Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlString contactCategoryName = SqlString.Null;
            String errMessage = "";

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                #region Server Side Validation
                if (txtContactCategoryName.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter Contact Category name <br/>";
                }
                else
                {
                    contactCategoryName = txtContactCategoryName.Text.ToString().Trim();
                }

                if (errMessage.Trim() != "")
                {
                    lblMessage.Text = "Kindly solve Following error(s) <br/>" + errMessage;
                    lblMessage.Attributes.Add("class", "text-danger");
                    return;
                }
                #endregion

                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                #region Multiple Records Insert
                if (ViewState["ContactCategoryRecordsXml"] != "")
                {
                    ViewState["ContactCategoryRecordsXml"] = "<ContactCategories>" + ViewState["ContactCategoryRecordsXml"].ToString();
                    ViewState["ContactCategoryRecordsXml"] += "<ContactCategoryNode><ContactCategoryName>" + contactCategoryName.ToString() + "</ContactCategoryName></ContactCategoryNode>";
                    ViewState["ContactCategoryRecordsXml"] += "</ContactCategories>";

                    cmd.Parameters.AddWithValue("@xml", ViewState["ContactCategoryRecordsXml"].ToString());
                    cmd.CommandText = "[PR_ContactCategory_MultiInsert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Contact Categories Inserted Successfully.";
                    lblMessage.Attributes.Add("class", "text-success");

                    ViewState["ContactCategoryRecordsXml"] = "";
                    txtContactCategoryName.Text = "";
                    txtContactCategoryName.Focus();
                }
                #endregion
                else
                {
                    cmd.Parameters.AddWithValue("@ContactCategoryName", contactCategoryName);

                    if (Page.RouteData.Values["ContactCategoryID"] != null)
                    {
                        cmd.Parameters.AddWithValue("@ContactCategoryID", EncryptDecrypt.Decrypt(Page.RouteData.Values["ContactCategoryID"].ToString().Trim()));
                        cmd.CommandText = "[PR_ContactCategory_Update]";

                        cmd.ExecuteNonQuery();

                        Response.Redirect("~/Pages/ContactCategory/List");
                    }
                    else
                    {
                        cmd.CommandText = "[PR_ContactCategory_Insert]";

                        cmd.ExecuteNonQuery();

                        lblMessage.Text = "Contact Category Inserted Successfully.";
                        lblMessage.Attributes.Add("class", "text-success");

                        txtContactCategoryName.Text = "";
                        txtContactCategoryName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text += ex.Message;
                lblMessage.Attributes.Add("class", "text-danger");
            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }
        #endregion

        #region Button Add More Click event for Multiple Insert
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            #region Local Variables + Server Side Validation
            SqlString contactCategoryName = SqlString.Null;
            String errMessage = "";

            if (txtContactCategoryName.Text.ToString().Trim() == "")
            {
                errMessage += " - Please Enter Contact Category name <br/>";
            }
            else
            {
                contactCategoryName = txtContactCategoryName.Text.ToString().Trim();
            }

            if (errMessage.Trim() != "")
            {
                lblMessage.Text = "Kindly solve Following error(s) <br/>" + errMessage;
                lblMessage.Attributes.Add("class", "text-danger");
                return;
            }
            #endregion

            ViewState["ContactCategoryRecordsXml"] += "<ContactCategoryNode><ContactCategoryName>" + contactCategoryName.ToString() + "</ContactCategoryName></ContactCategoryNode>";
            txtContactCategoryName.Text = "";
            txtContactCategoryName.Focus();
        }
        #endregion
    }
}