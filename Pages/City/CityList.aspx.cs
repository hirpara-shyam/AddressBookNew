using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBookNew.Pages.City
{
    public partial class CityList : System.Web.UI.Page
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
            SqlString userID = SqlString.Null;

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (Session["UserID"] != null)
                {
                    userID = Session["UserID"].ToString().Trim();
                }

                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_City_SelectAll]";

                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    gvCity.DataSource = objSDR;
                    gvCity.DataBind();
                }
                else
                {
                    lblDataMessage.Text = "No data found.";
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

        #region City GridView Row Command
        protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteCity")
            {
                DeleteCity(e.CommandArgument.ToString());
            }
        }
        #endregion

        #region Delete City
        private void DeleteCity(String cityID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_City_DeleteByPK]";

                cmd.Parameters.AddWithValue("@CityID", cityID);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                cmd.ExecuteNonQuery();

                Response.Redirect("~/Pages/City/List");
                lblMessage.Text = "City Deleted Successfully.";
                lblMessage.Attributes.Add("class", "text-success");
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

        #region Export to Excel
        protected void ExportToExcelUsingEPPlus(object sender, EventArgs e)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("City Data");
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
                    Response.AddHeader("content-disposition", "attachment; filename=CityData.xlsx");
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
            foreach (TableCell cell in gvCity.HeaderRow.Cells)
            {
                if(cell.Text != "Edit" && cell.Text != "Delete")
                    dt.Columns.Add(cell.Text);
            }
            foreach (GridViewRow row in gvCity.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Cells.Count-2; i++)
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
            sb.Append("<Cities>");

            foreach (GridViewRow row in gvCity.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("cbDeleteMany");
                if (cb != null && cb.Checked)
                {
                    string CityID = gvCity.DataKeys[row.RowIndex].Value.ToString();
                    sb.Append("<City><CityID>" + CityID + "</CityID></City>");
                }

            }

            sb.Append("</Cities>");
            string xmlCountryIDs = sb.ToString();

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_City_MultiDelete]";

                cmd.Parameters.AddWithValue("@xml", xmlCountryIDs);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                cmd.ExecuteNonQuery();

                Response.Redirect("~/Pages/City/List");
                lblMessage.Text = "All Selected Cities Deleted Successfully.";
                lblMessage.Attributes.Add("class", "text-success");
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

        #region Select All
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectAll = (CheckBox)gvCity.HeaderRow.FindControl("cbSelectAll");

            foreach (GridViewRow row in gvCity.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("cbDeleteMany");

                chk.Checked = selectAll.Checked;
            }
        }
        #endregion
    }
}