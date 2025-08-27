using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.State
{
    public partial class StateAddEdit : System.Web.UI.Page
    {
        #region Xml for data Records
        private void xmlData()
        {
            ViewState["StateRecordsXml"] = "";
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                xmlData();

                String userID = "";
                if (Session["UserID"] != null)
                    userID = Session["UserID"].ToString().Trim();

                CommonDropDownFill.FillCountryDropDown(ddlCountry, userID);

                if (Page.RouteData.Values["StateID"] != null)
                {
                    lblAddEdit.Text = "Edit State";
                    btnAddMore.Visible = false;
                    FillData(EncryptDecrypt.Decrypt(Page.RouteData.Values["StateID"].ToString().Trim()));
                }
                else
                    lblAddEdit.Text = "Add State";
            }
        }
        #endregion

        #region Fill Data for Edit
        private void FillData(SqlString StateID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_State_SelectByPK]";

                cmd.Parameters.AddWithValue("@StateID", StateID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString();
                        txtStateName.Text = objSDR["StateName"].ToString();
                    }
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
            SqlString countryID = SqlString.Null;
            SqlString stateName = SqlString.Null;
            String errMessage = "";

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                #region Server Side Validation
                if (ddlCountry.SelectedValue.ToString().Trim() == "-1")
                {
                    errMessage += " - Please Select Country <br/>";
                }
                else
                {
                    countryID = ddlCountry.SelectedValue.ToString().Trim();
                }

                if (txtStateName.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter State name <br/>";
                }
                else
                {
                    stateName = txtStateName.Text.ToString().Trim();
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
                if (ViewState["StateRecordsXml"] != "")
                {
                    ViewState["StateRecordsXml"] = "<States>" + ViewState["StateRecordsXml"].ToString();
                    ViewState["StateRecordsXml"] += "<StateNode><StateName>" + stateName.ToString() + "</StateName><CountryID>" + countryID.ToString() + "</CountryID></StateNode>";
                    ViewState["StateRecordsXml"] += "</States>";

                    cmd.Parameters.AddWithValue("@xml", ViewState["StateRecordsXml"].ToString());
                    cmd.CommandText = "[PR_State_MultiInsert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "States Inserted Successfully.";
                    lblMessage.Attributes.Add("class", "text-success");

                    ViewState["StateRecordsXml"] = "";
                    ddlCountry.SelectedIndex = 0;
                    txtStateName.Text = "";
                    ddlCountry.Focus();
                }
                #endregion
                else
                {
                    cmd.Parameters.AddWithValue("@StateName", stateName);
                    cmd.Parameters.AddWithValue("@CountryID", countryID);

                    if (Page.RouteData.Values["StateID"] != null)
                    {
                        cmd.Parameters.AddWithValue("@StateID", EncryptDecrypt.Decrypt(Page.RouteData.Values["StateID"].ToString().Trim()));
                        cmd.CommandText = "[PR_State_Update]";

                        cmd.ExecuteNonQuery();

                        Response.Redirect("~/Pages/State/List");
                    }
                    else
                    {
                        cmd.CommandText = "[PR_State_Insert]";

                        cmd.ExecuteNonQuery();

                        lblMessage.Text = "State Inserted Successfully.";
                        lblMessage.Attributes.Add("class", "text-success");

                        ddlCountry.SelectedValue = "-1";
                        txtStateName.Text = "";
                        txtStateName.Focus();
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
            SqlString stateName = SqlString.Null;
            SqlString countryID = SqlString.Null;
            String errMessage = "";

            if (ddlCountry.SelectedValue.ToString().Trim() == "-1")
            {
                errMessage += " - Please Select Country <br/>";
            }
            else
            {
                countryID = ddlCountry.SelectedValue.ToString().Trim();
            }

            if (txtStateName.Text.ToString().Trim() == "")
            {
                errMessage += " - Please Enter State name <br/>";
            }
            else
            {
                stateName = txtStateName.Text.ToString().Trim();
            }

            if (errMessage.Trim() != "")
            {
                lblMessage.Text = "Kindly solve Following error(s) <br/>" + errMessage;
                lblMessage.Attributes.Add("class", "text-danger");
                return;
            }
            #endregion

            ViewState["StateRecordsXml"] += "<StateNode><StateName>" + stateName.ToString() + "</StateName><CountryID>" + countryID.ToString() + "</CountryID></StateNode>";
            ddlCountry.SelectedIndex = 0;
            txtStateName.Text = "";
            ddlCountry.Focus();
        }
        #endregion
    }
}