using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.Contact
{
    public partial class ContactList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillGridView();
            }
        }

        #region Fill Grid View
        private void FillGridView()
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

            objConn.Open();

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.CommandText = "PR_Contact_SelectAll";

            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                gvContact.DataSource = objSDR;
                gvContact.DataBind();
            }
            else
                lblDataMessage.Text = "No data found.";

            objConn.Close();
        }
        #endregion

        #region Contact Row Command
        protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteContact")
            {
                if (e.CommandArgument != "")
                {
                    DeleteContact(e.CommandArgument.ToString().Trim());
                }
            }
        }
        #endregion

        #region Delete Contact
        private void DeleteContact(String contactID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.CommandText = "PR_Contact_DeleteByPK";

            objCmd.Parameters.AddWithValue("@ContactID", contactID);
            if (Session["UserID"] != null)
                objCmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

            objCmd.ExecuteNonQuery();

            objConn.Close();


            Response.Redirect("~/Pages/Contact/List");

            FillGridView();
        }
        #endregion

        #region Export to Excel
        protected void ExportToExcelUsingEPPlus(object sender, EventArgs e)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Contact Data");
                workSheet.Cells[1, 1].LoadFromDataTable(GetGridViewData(), true);

                using (var range = workSheet.Cells["A1:Z1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                }

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=ContactData.xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            foreach (TableCell cell in gvContact.HeaderRow.Cells)
            {
                if (cell.Text != "Edit" && cell.Text != "Delete")
                    dt.Columns.Add(cell.Text);
            }
            foreach (GridViewRow row in gvContact.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Cells.Count - 2; i++)
                {
                    dr[i] = row.Cells[i].Text;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region Multiple Delete
        protected void btnDeleteMultiple_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("<Contacts>");

            foreach (GridViewRow row in gvContact.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("cbDeleteMany");
                if (cb != null && cb.Checked)
                {
                    string ContactID = gvContact.DataKeys[row.RowIndex].Value.ToString();
                    sb.Append("<Contact><ContactID>" + ContactID + "</ContactID></Contact>");
                }

            }

            sb.Append("</Contacts>");
            string xmlCountryIDs = sb.ToString();

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_Contact_MultiDelete]";

                cmd.Parameters.AddWithValue("@xml", xmlCountryIDs);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                cmd.ExecuteNonQuery();

                Response.Redirect("~/Pages/Contact/List");
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
        #endregion

        #region Select All
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectAll = (CheckBox)gvContact.HeaderRow.FindControl("cbSelectAll");

            foreach (GridViewRow row in gvContact.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("cbDeleteMany");

                chk.Checked = selectAll.Checked;
            }
        }
        #endregion

    }
}