using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.Contact
{
    public partial class ContactAddEdit : System.Web.UI.Page
    {
        #region XML for Storing Records
        public void xmlData()
        {
            ViewState["ContactRecordsXml"] = "";
            ViewState["idForContactNodes"] = 0;
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                xmlData();

                FillDropDown();
                FillCBLContactCategoryID();

                if (Page.RouteData.Values["ContactID"] != null)
                {
                    lblMessage.Text = "Edit Contact ";
                    btnAddMore.Visible = false;
                    FillControls(Convert.ToInt32(EncryptDecrypt.Decrypt(Page.RouteData.Values["ContactID"].ToString())));
                    FillContactCategoryIDByContactID(Convert.ToInt32(EncryptDecrypt.Decrypt(Page.RouteData.Values["ContactID"].ToString().Trim())));
                }
                else
                    lblMessage.Text = "Add Contact";
            }
            
        }
        #endregion

        #region FillCommonDropDown
        private void FillDropDown()
        {
            String UserID = "";
            if (Session["UserID"] != null)
            {
                UserID = Session["UserID"].ToString();
            }
            CommonDropDownFill.FillCountryDropDown(ddlCountryID, UserID);
        }
        #endregion

        #region ContactSave
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Local Variables
            SqlString strContactName = SqlString.Null;

            SqlString strGender = SqlString.Null;
            SqlString strMobileNo = SqlString.Null;
            SqlString strEmail = SqlString.Null;
            SqlString str = SqlString.Null;
            SqlString strCountryID = SqlString.Null;
            SqlString strStateID = SqlString.Null;
            SqlString strCityID = SqlString.Null;

            String errorMessage = "";
            #endregion

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString;
            try
            {
                #region Server Side Validations
                if (txtContactName.Text.Trim() == "")
                    errorMessage += "Enter ContactName<br/>";

                if (txtMobileNo.Text.Trim() == "")
                    errorMessage += "Enter Mobile No <br/>";

                if (txtEmail.Text.Trim() == "")
                    errorMessage += "Enter Email<br/>";

                if (rbtnlGender.SelectedValue == "")
                    errorMessage += "Select Gender<br/>";

                if (ddlCountryID.SelectedIndex == 0)
                    errorMessage += "Select Country<br/>";

                if (ddlStateID.SelectedIndex == 0)
                    errorMessage += "Select State<br/>";

                if (ddlCityID.SelectedIndex == 0)
                    errorMessage += "Select City<br/>";

                if (errorMessage != "")
                {
                    lblMessage.Text = errorMessage;
                    lblMessage.Attributes.Add("class", "text-danger");
                    return;
                }
                #endregion

                if (txtContactName.Text.Trim() != "")
                    strContactName = txtContactName.Text.Trim();

                if (txtMobileNo.Text.Trim() != "")
                    strMobileNo = txtMobileNo.Text.Trim();

                if (rbtnlGender.SelectedValue != "")
                    strGender = rbtnlGender.SelectedValue.Trim();

                if (txtEmail.Text.Trim() != "")
                    strEmail = txtEmail.Text.Trim();

                if (ddlCountryID.SelectedIndex > 0)
                    strCountryID = ddlCountryID.SelectedValue;
                if (ddlStateID.SelectedIndex > 0)
                    strStateID = ddlStateID.SelectedValue;
                if (ddlCityID.SelectedIndex > 0)
                    strCityID = ddlCityID.SelectedValue;

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                #region Multiple Records Insert
                if (ViewState["ContactRecordsXml"] != "")
                {
                    ViewState["ContactRecordsXml"] = "<Contacts>" + ViewState["ContactRecordsXml"].ToString();

                    ViewState["ContactRecordsXml"] += $"<ContactNode id=\"{ViewState["idForContactNodes"].ToString()}\" ><ContactName>" + strContactName.ToString() +
                                                "</ContactName><Gender>" + strGender.ToString() +
                                                "</Gender><MobileNo>" + strMobileNo.ToString() +
                                                "</MobileNo><Email>" + strEmail.ToString() +
                                                "</Email><CityID>" + strCityID.ToString() +
                                                "</CityID><StateID>" + strStateID.ToString() +
                                                "</StateID><CountryID>" + strCountryID.ToString() +
                                                "</CountryID><ContactCategories>";

                    foreach (ListItem li in cblContactCategoryID.Items)
                    {
                        if (li.Selected)
                        {

                            ViewState["ContactRecordsXml"] += "<ContactCategory><ContactCategoryID>" + li.Value.ToString() + "</ContactCategoryID></ContactCategory>";
                        }
                    }
                    ViewState["ContactRecordsXml"] += "</ContactCategories></ContactNode>";

                    ViewState["ContactRecordsXml"] += "</Contacts>";

                    cmd.Parameters.AddWithValue("@xml", ViewState["ContactRecordsXml"].ToString());
                    cmd.CommandText = "[PR_Contact_MultiInsert]";

                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Contacts Inserted Successfully.";

                    ViewState["idForContactNodes"] = 0;
                    ViewState["ContactRecordsXml"] = "";
                    txtContactName.Text = "";
                    rbtnlGender.ClearSelection();
                    txtMobileNo.Text = "";
                    txtEmail.Text = "";
                    cblContactCategoryID.ClearSelection();
                    ddlCountryID.ClearSelection();
                    ddlStateID.ClearSelection();
                    ddlCityID.ClearSelection();
                    txtContactName.Focus();
                }
                #endregion
                else
                {
                    cmd.Parameters.AddWithValue("@ContactName", strContactName);
                    cmd.Parameters.AddWithValue("@Gender", strGender);
                    cmd.Parameters.AddWithValue("@MobileNo", strMobileNo);
                    cmd.Parameters.AddWithValue("@Email", strEmail);
                    cmd.Parameters.AddWithValue("@CountryID", strCountryID);
                    cmd.Parameters.AddWithValue("@StateID", strStateID);
                    cmd.Parameters.AddWithValue("@CityID", strCityID);

                    if (Page.RouteData.Values["ContactID"] != null)
                    {
                        cmd.Parameters.AddWithValue("@ContactID", EncryptDecrypt.Decrypt(Page.RouteData.Values["ContactID"].ToString().Trim()));
                        cmd.CommandText = "[PR_Contact_Update]";
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.Parameters.Add("@ContactID", System.Data.SqlDbType.Int, 4).Direction = System.Data.ParameterDirection.Output;
                        cmd.CommandText = "[PR_Contact_Insert]";
                        cmd.ExecuteNonQuery();
                        txtContactName.Text = "";
                        rbtnlGender.ClearSelection();
                        txtMobileNo.Text = "";
                        txtEmail.Text = "";
                        cblContactCategoryID.ClearSelection();
                        ddlCountryID.ClearSelection();
                        ddlStateID.ClearSelection();
                        ddlCityID.ClearSelection();
                        txtContactName.Focus();
                    }

                    SqlInt32 ContactID = 0;
                    if (Page.RouteData.Values["ContactID"] != null)
                        ContactID = Convert.ToInt32(EncryptDecrypt.Decrypt(Page.RouteData.Values["ContactID"].ToString()));
                    else
                        ContactID = Convert.ToInt32(cmd.Parameters["@ContactID"].Value);

                    String xml = "<ContactWiseContactCategory>";
                    SqlCommand objCmdContactCategory = conn.CreateCommand();
                    objCmdContactCategory.CommandType = System.Data.CommandType.StoredProcedure;
                    objCmdContactCategory.CommandText = "[PR_ContactWiseContactCategory_Insert]";
                    objCmdContactCategory.Parameters.Add("@ContactID", System.Data.SqlDbType.Int).Value = ContactID;
                    foreach (ListItem li in cblContactCategoryID.Items)
                    {
                        if (li.Selected)
                        {

                            xml += "<ContactNode><ContactCategoryID>" + li.Value.ToString() + "</ContactCategoryID><ContactID>" + ContactID.ToString() + "</ContactID></ContactNode>";
                        }
                    }
                    xml += "</ContactWiseContactCategory>";
                    objCmdContactCategory.Parameters.AddWithValue("@xml", xml);
                    objCmdContactCategory.ExecuteNonQuery();



                    lblMessage.Text = "Data inserted Successfully. ";
                    lblMessage.Attributes.Add("class", "text-success");
                    Response.Redirect("~/Pages/Contact/List", false);
                }
                
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Attributes.Add("class", "text-danger");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }
        #endregion

        #region Fill Control for Edit Mode
        private void FillControls(Int32 ContactID)
        {
            String imgPath = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_Contact_SelectByPK]";
                cmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    String UserID = "";
                    if (Session["UserID"] != null)
                    {
                        UserID = Session["UserID"].ToString();
                    }

                    while (sdr.Read())
                    {
                        if (sdr["ContactName"].Equals(DBNull.Value) != true)
                        {
                            txtContactName.Text = sdr["ContactName"].ToString().Trim();
                        }
                        rbtnlGender.SelectedValue = sdr["Gender"].ToString();
                        ddlCountryID.SelectedValue = sdr["CountryID"].ToString();
                        ddlStateID.SelectedValue = sdr["StateID"].ToString();
                        ddlCityID.SelectedValue = sdr["CityID"].ToString();

                        CommonDropDownFill.FillStateDropDown(ddlStateID, UserID, sdr["CountryID"].ToString());
                        CommonDropDownFill.FillCityDropDown(ddlCityID, UserID, sdr["StateID"].ToString());

                        txtMobileNo.Text = sdr["MobileNo"].ToString();
                        txtEmail.Text = sdr["Email"].ToString();

                        break;
                    }
                }
                else
                {
                    lblMessage.Text = "No Data";
                    lblMessage.Attributes.Add("class", "text-info");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text += ex.Message;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();

            }
        }
        #endregion

        #region FillContactCategoryID By ContactID
        private void FillContactCategoryIDByContactID(Int32 ContactID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_ContactCategory_ContactWiseCategory_SelectByContactID]";
                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim() );

                SqlDataReader sdr = cmd.ExecuteReader();

                cblContactCategoryID.Items.Clear();
                while (sdr.Read())
                {
                    ListItem item = new ListItem
                    {
                        Text = sdr["ContactCategoryName"].ToString(),
                        Value = sdr["ContactCategoryID"].ToString(),
                        Selected = Convert.ToBoolean(sdr["IsSelected"])
                    };
                    cblContactCategoryID.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text += ex.Message;
                lblMessage.Attributes.Add("class", "text-danger");
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

        }
        #endregion

        #region ddlCountryID SelectedIndexChanged
        protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountryID.SelectedIndex > 0)
            {
                String UserID = "";
                if (Session["UserID"] != null)
                {
                    UserID = Session["UserID"].ToString();
                }
                ddlStateID.Enabled = true;
                ddlStateID.Items.Clear();
                CommonDropDownFill.FillStateDropDown(ddlStateID, UserID, ddlCountryID.SelectedValue);
            }
            else
            {
                ddlStateID.Enabled = false;
            }
        }
        #endregion

        #region ddlStateID_SelectedIndexChanged
        protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStateID.SelectedIndex > 0)
            {
                String UserID = "";
                if (Session["UserID"] != null)
                {
                    UserID = Session["UserID"].ToString();
                }
                ddlCityID.Enabled = true;
                ddlCityID.Items.Clear();
                CommonDropDownFill.FillCityDropDown(ddlCityID, UserID, ddlStateID.SelectedValue);
            }
            else
            {
                ddlCityID.Enabled = false;
            }
        }

        #endregion

        #region Fill Contact Category Checkbox List
        private void FillCBLContactCategoryID()
        {
            try
            {
                SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

                objConn.Open();

                SqlCommand objCmd = new SqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.CommandText = "[PR_ContactCategory_SelectForDropDownList]";

                if (Session["UserID"] != null)
                    objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    cblContactCategoryID.DataTextField = "ContactCategoryName";
                    cblContactCategoryID.DataValueField = "ContactCategoryID";
                    cblContactCategoryID.DataSource = objSDR;
                    cblContactCategoryID.DataBind();
                }
                else
                    lblMessage.Text = "No data found.";

                objConn.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Attributes.Add("class", "text-danger");
            }
            finally
            {

            }

        }
        #endregion

        #region Button Add More Click event for Multiple Insert
        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            #region Local Variables
            SqlString strContactName = SqlString.Null;

            SqlString strGender = SqlString.Null;
            SqlString strMobileNo = SqlString.Null;
            SqlString strEmail = SqlString.Null;
            SqlString str = SqlString.Null;
            SqlString strCountryID = SqlString.Null;
            SqlString strStateID = SqlString.Null;
            SqlString strCityID = SqlString.Null;

            String errorMessage = "";
            #endregion

            #region Server Side Validations
            if (txtContactName.Text.Trim() == "")
                errorMessage += "Enter ContactName<br/>";
            else
                strContactName = txtContactName.Text.Trim();

            if (txtMobileNo.Text.Trim() == "")
                errorMessage += "Enter Mobile No <br/>";
            else
                strMobileNo = txtMobileNo.Text.Trim();

            if (txtEmail.Text.Trim() == "")
                errorMessage += "Enter Email<br/>";
            else
                strEmail = txtEmail.Text.Trim();

            if (rbtnlGender.SelectedValue == "")
                errorMessage += "Select Gender<br/>";
            else
                strGender = rbtnlGender.Text.Trim();

            if (ddlCountryID.SelectedIndex == 0)
                errorMessage += "Select Country<br/>";
            else
                strCountryID = ddlCountryID.Text.Trim();

            if (ddlStateID.SelectedIndex == 0)
                errorMessage += "Select State<br/>";
            else
                strStateID = ddlStateID.Text.Trim();

            if (ddlCityID.SelectedIndex == 0)
                errorMessage += "Select City<br/>";
            else
                strCityID = ddlCityID.Text.Trim();

            if (errorMessage != "")
            {
                lblMessage.Text = errorMessage;
                lblMessage.Attributes.Add("class", "text-danger");
                return;
            }
            #endregion

            ViewState["ContactRecordsXml"] += $"<ContactNode id=\"{ViewState["idForContactNodes"].ToString()}\"><ContactName>" + strContactName.ToString() + 
                                                "</ContactName><Gender>" + strGender.ToString() + 
                                                "</Gender><MobileNo>" + strMobileNo.ToString() + 
                                                "</MobileNo><Email>" + strEmail.ToString() + 
                                                "</Email><CityID>" + strCityID.ToString() + 
                                                "</CityID><StateID>" + strStateID.ToString() +  
                                                "</StateID><CountryID>" + strCountryID.ToString() +
                                                "</CountryID><ContactCategories>";

            foreach (ListItem li in cblContactCategoryID.Items)
            {
                if (li.Selected)
                {

                    ViewState["ContactRecordsXml"] += "<ContactCategory><ContactCategoryID>" + li.Value.ToString() + "</ContactCategoryID></ContactCategory>";
                }
            }
            ViewState["ContactRecordsXml"] += "</ContactCategories></ContactNode>";

            ViewState["idForContactNodes"] = (Convert.ToInt32(ViewState["idForContactNodes"]) + 1).ToString();

            txtContactName.Text = "";
            rbtnlGender.ClearSelection();
            txtMobileNo.Text = "";
            txtEmail.Text = "";
            cblContactCategoryID.ClearSelection();
            ddlCountryID.ClearSelection();
            ddlStateID.ClearSelection();
            ddlCityID.ClearSelection();
            txtContactName.Focus();
        }
        #endregion
    }
}