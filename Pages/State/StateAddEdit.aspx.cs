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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String userID = "";
                if (Session["UserID"] != null)
                    userID = Session["UserID"].ToString().Trim();

                CommonDropDownFill.FillCountryDropDown(ddlCountry, userID);

                if (Request.QueryString["StateID"] != null)
                {
                    lblAddEdit.Text = "Edit State";
                    FillData(Request.QueryString["StateID"].ToString().Trim());
                }
                else
                    lblAddEdit.Text = "Add State";
            }
        }

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
            SqlString stateName = SqlString.Null;
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
                }

                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StateName", stateName);
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                if (Request.QueryString["StateID"] != null)
                {
                    cmd.Parameters.AddWithValue("@StateID", Request.QueryString["StateID"].ToString().Trim());
                    cmd.CommandText = "[PR_State_Update]";

                    cmd.ExecuteNonQuery();

                    Response.Redirect("~/Pages/State/StateList.aspx");
                }
                else
                {
                    cmd.CommandText = "[PR_State_Insert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "State Inserted Successfully.";

                    ddlCountry.SelectedValue = "-1";
                    txtStateName.Text = "";
                    txtStateName.Focus();
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