using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            SqlString userName = SqlString.Null;
            SqlString password = SqlString.Null;
            SqlString email = SqlString.Null;
            SqlString mobileNo = SqlString.Null;
            String errMessage = "";

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (txtUserName.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter Username <br/>";
                }
                else
                {
                    userName = txtUserName.Text.ToString().Trim();
                }

                if (txtPassword.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter Password <br/>";
                }
                else
                {
                    password = txtPassword.Text.ToString().Trim();
                }

                if (txtEmail.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter Email <br/>";
                }
                else
                {
                    email = txtEmail.Text.ToString().Trim();
                }

                if (txtMobileNo.Text.ToString().Trim() == "")
                {
                    errMessage += " - Please Enter Mobile number <br/>";
                }
                else
                {
                    mobileNo = txtMobileNo.Text.ToString().Trim();
                }

                if (errMessage.Trim() != "")
                {
                    lblMessage.Text = "Kindly solve Following error(s) <br/>" + errMessage;
                }

                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_User_Register]";

                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MobileNo", mobileNo);

                cmd.ExecuteNonQuery();

                Response.Redirect("~/Pages/Login.aspx");
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