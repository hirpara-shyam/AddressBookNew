using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.City
{
    public partial class CityAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String userID = "";
                if (Session["UserID"] != null)
                    userID = Session["UserID"].ToString().Trim();

                CommonDropDownFill.FillCountryDropDown(ddlCountry, userID);

                if (Request.QueryString["CityID"] != null)
                {
                    lblAddEdit.Text = "Edit City";
                    FillData(Request.QueryString["CityID"].ToString().Trim());
                }
                else
                    lblAddEdit.Text = "Add City";
            }
        }

        private void FillData(String CityID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_City_SelectByPK]";

                cmd.Parameters.AddWithValue("@CityID", CityID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        ddlCountry.SelectedValue = objSDR["CountryID"].ToString();
                        ddlState.SelectedValue = objSDR["StateID"].ToString();
                        txtCityName.Text = objSDR["CityName"].ToString();
                        CommonDropDownFill.FillStateDropDown(ddlState, Session["UserID"].ToString().Trim(), ddlCountry.SelectedValue);
                    }
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
            SqlString countryID = SqlString.Null;
            SqlString stateID = SqlString.Null;
            SqlString cityName = SqlString.Null;
            String errMessage = "";

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (ddlCountry.SelectedValue.ToString().Trim() == "-1")
                {
                    errMessage += " - Please Select Country <br/>";
                }
                else
                {
                    countryID = ddlCountry.SelectedValue.ToString().Trim();
                }
                if (ddlState.SelectedValue.ToString().Trim() == "-1")
                {
                    errMessage += " - Please Select State <br/>";
                }
                else
                {
                    stateID = ddlState.SelectedValue.ToString().Trim();
                }

                if (txtCityName.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter City name <br/>";
                }
                else
                {
                    cityName = txtCityName.Text.ToString().Trim();
                }

                if (errMessage.Trim() != "")
                {
                    lblMessage.Text = "Kindly solve Following error(s) <br/>" + errMessage;
                }

                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@StateID", stateID);
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                if (Request.QueryString["CityID"] != null)
                {
                    cmd.Parameters.AddWithValue("@CityID", Request.QueryString["CityID"].ToString().Trim());
                    cmd.CommandText = "[PR_City_Update]";

                    cmd.ExecuteNonQuery();

                    Response.Redirect("~/Pages/City/CityList.aspx");
                }
                else
                {
                    cmd.CommandText = "[PR_City_Insert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "City Inserted Successfully.";

                    ddlCountry.SelectedValue = "-1";
                    ddlState.SelectedValue = "-1";
                    txtCityName.Text = "";
                    txtCityName.Focus();
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

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCountry.SelectedIndex > 0)
            {
                ddlState.Items.Clear();
                CommonDropDownFill.FillStateDropDown(ddlState, Session["UserID"].ToString().Trim(), ddlCountry.SelectedValue);
            }
            else
            {
                ddlState.Items.Clear();
            }
        }
    }
}