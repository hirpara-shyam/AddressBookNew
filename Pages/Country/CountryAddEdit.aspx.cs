using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.Country
{
    public partial class CountryAddEdit : System.Web.UI.Page
    {
        #region XML data
        private void xmlData()
        {
            ViewState["CountryRecordsXml"] = "";
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                xmlData();

                if (Page.RouteData.Values["CountryID"] != null)
                {
                    lblAddEdit.Text = "Edit Country";
                    btnAddMore.Visible = false;
                    FillData(EncryptDecrypt.Decrypt(Page.RouteData.Values["CountryID"].ToString().Trim()));
                }
                else
                    lblAddEdit.Text = "Add Country";
            }
        }

        #region Fill Data for Edit
        private void FillData(SqlString CountryID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_Country_SelectByPK]";

                cmd.Parameters.AddWithValue("@CountryID", CountryID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.Read())
                {
                    txtCountryName.Text = objSDR["CountryName"].ToString();
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

        #region Save Button Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlString countryName = SqlString.Null;
            String errMessage = "";

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                #region Server Side Validation
                if (txtCountryName.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter Country name <br/>";
                }
                else
                {
                    countryName = txtCountryName.Text.ToString().Trim();
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
                if (ViewState["CountryRecordsXml"] != "")
                {
                    ViewState["CountryRecordsXml"] = "<Countries>" + ViewState["CountryRecordsXml"].ToString();
                    ViewState["CountryRecordsXml"] += "<CountryNode><CountryName>" + countryName.ToString() + "</CountryName></CountryNode>";
                    ViewState["CountryRecordsXml"] += "</Countries>";

                    cmd.Parameters.AddWithValue("@xml", ViewState["CountryRecordsXml"].ToString());
                    cmd.CommandText = "[PR_Country_MultiInsert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Countries Inserted Successfully.";
                    lblMessage.Attributes.Add("class", "text-success");

                    ViewState["CountryRecordsXml"] = "";
                    txtCountryName.Text = "";
                    txtCountryName.Focus();
                }
                #endregion
                else
                {
                    cmd.Parameters.AddWithValue("@CountryName", countryName);

                    if (Page.RouteData.Values["CountryID"] != null)
                    {
                        cmd.Parameters.AddWithValue("@CountryID", EncryptDecrypt.Decrypt(Page.RouteData.Values["CountryID"].ToString().Trim()));
                        cmd.CommandText = "[PR_Country_Update]";

                        cmd.ExecuteNonQuery();

                        Response.Redirect("~/Pages/Country/List");
                    }
                    else
                    {
                        cmd.CommandText = "[PR_Country_Insert]";

                        cmd.ExecuteNonQuery();

                        lblMessage.Text = "Country Inserted Successfully.";
                        lblMessage.Attributes.Add("class", "text-success");

                        txtCountryName.Text = "";
                        txtCountryName.Focus();
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
            SqlString countryName = SqlString.Null;
            String errMessage = "";

            if (txtCountryName.Text.ToString().Trim() == "")
            {
                errMessage += " - Please Enter Country name <br/>";
            }
            else
            {
                countryName = txtCountryName.Text.ToString().Trim();
            }

            if (errMessage.Trim() != "")
            {
                lblMessage.Text = "Kindly solve Following error(s) <br/>" + errMessage;
                lblMessage.Attributes.Add("class", "text-danger");
                return;
            }
            #endregion

            ViewState["CountryRecordsXml"] += "<CountryNode><CountryName>" + countryName.ToString() + "</CountryName></CountryNode>";
            txtCountryName.Text = "";
            txtCountryName.Focus();
        }
        #endregion
    }
}