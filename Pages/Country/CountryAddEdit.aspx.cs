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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CountryID"] != null)
                {
                    lblAddEdit.Text = "Edit Country";
                    FillData(Request.QueryString["CountryID"].ToString().Trim());
                }
                else
                    lblAddEdit.Text = "Add Country";
            }
        }

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
                }

                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CountryName", countryName);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                if (Request.QueryString["CountryID"] != null)
                {
                    cmd.Parameters.AddWithValue("@CountryID", Request.QueryString["CountryID"].ToString().Trim());
                    cmd.CommandText = "[PR_Country_Update]";

                    cmd.ExecuteNonQuery();

                    Response.Redirect("~/Pages/Country/CountryList.aspx");
                }
                else
                {
                    cmd.CommandText = "[PR_Country_Insert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Country Inserted Successfully.";

                    txtCountryName.Text = "";
                    txtCountryName.Focus();
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