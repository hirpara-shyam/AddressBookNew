using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages
{
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUserData();
            }
        }

        private void FillUserData()
        {
            string userID = "";
            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString().Trim();

            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_Users_SelectByPK]";

                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lblUserName.Text = dr["UserName"].ToString();
                        lblName.Text = dr["UserName"].ToString();
                        txtUserName.Text = dr["UserName"].ToString();
                        lblMobileNo.Text = dr["MobileNo"].ToString();
                        txtMobileNo.Text = dr["MobileNo"].ToString();
                        lblEmail.Text = dr["Email"].ToString();
                        txtEmail.Text = dr["Email"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if(con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string userID = "";
            string userName = "";
            string email = "";
            string mobileNo = "";
            string errorMessage = "";

            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString().Trim();

            if (txtUserName.Text.ToString() != "")
            {
                userName = txtUserName.Text.ToString().Trim();
            }
            else
                errorMessage += " - Enter User name <br />";

            if (txtEmail.Text.ToString() != "")
            {
                email = txtEmail.Text.ToString().Trim();
            }
            else
                errorMessage += " - Enter Email <br />";

            if (txtMobileNo.Text.ToString() != "")
            {
                mobileNo = txtMobileNo.Text.ToString().Trim();
            }
            else
                errorMessage += " - Enter mobile number <br />";

            if (errorMessage != "")
            {
                lblMessage.Text = "Kindly solve following errors <br/>" + errorMessage;
                return;
            }


            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_Users_UpdateProfile]";

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MobileNo", mobileNo);

                cmd.ExecuteNonQuery();

                Session["UserName"] = userName;
                lblMessage.Text = "Data updated successfully";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                Response.Redirect("~/Pages/Profile.aspx");
            }
        }

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            string userID = "";
            string oldPassword = "";
            string newPassword = "";
            string retypePassword = "";
            string errorMessage = "";


            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString().Trim();

            if (txtOldPassword.Text.ToString() != "")
            {
                oldPassword = txtOldPassword.Text.ToString().Trim();
            }
            else
                errorMessage += " - Enter Old Password <br />";

            if (txtNewPassword.Text.ToString() != "")
            {
                newPassword = txtNewPassword.Text.ToString().Trim();
            }
            else
                errorMessage += " - Enter New Password <br />";

            if (txtRetypePassword.Text.ToString() != "")
            {
                retypePassword = txtRetypePassword.Text.ToString().Trim();
            }
            else
                errorMessage += " - Enter Retype Password <br />";
            
            if (newPassword != retypePassword)
            {
                errorMessage += " - New Password and Re-Type Password does not match. <br/>";
            }

            if (errorMessage != "")
            {
                lblMessage.Text = "Kindly solve following errors <br/>" + errorMessage;
                return;
            }

            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_Users_ChangePassword]";

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (Convert.ToBoolean(sdr["result"]))
                        {
                            lblMessage.Text = sdr["SuccessMessage"].ToString().Trim();
                        }
                        else
                        {
                            lblMessage.Text = sdr["ErrorMessage"].ToString().Trim();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Profile.aspx");
        }
    }
}