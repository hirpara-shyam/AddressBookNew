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

namespace AddressBookNew.Pages.State
{
    public partial class StateList : System.Web.UI.Page
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
                cmd.CommandText = "[PR_State_SelectAll]";

                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    gvState.DataSource = objSDR;
                    gvState.DataBind();
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

        #region State row command
        protected void gvState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteState")
            {
                DeleteState(e.CommandArgument.ToString());
            }
        }
        #endregion

        #region Delete State
        private void DeleteState(String stateID)
        {
            String errMessage = "";

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_State_DeleteByPK]";

                cmd.Parameters.AddWithValue("@StateID", stateID);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                cmd.ExecuteNonQuery();

                Response.Redirect("~/Pages/State/List");
                lblMessage.Text = "State Deleted Successfully.";
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
                var workSheet = excel.Workbook.Worksheets.Add("State Data");
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
                    Response.AddHeader("content-disposition", "attachment; filename=StateData.xlsx");
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
            foreach (TableCell cell in gvState.HeaderRow.Cells)
            {
                if (cell.Text != "Edit" && cell.Text != "Delete")
                    dt.Columns.Add(cell.Text);
            }
            foreach (GridViewRow row in gvState.Rows)
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
            sb.Append("<States>");

            foreach (GridViewRow row in gvState.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("cbDeleteMany");
                if (cb != null && cb.Checked)
                {
                    string StateID = gvState.DataKeys[row.RowIndex].Value.ToString();
                    sb.Append("<State><StateID>" + StateID + "</StateID></State>");
                }

            }

            sb.Append("</States>");
            string xmlCountryIDs = sb.ToString();

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_State_MultiDelete]";

                cmd.Parameters.AddWithValue("@xml", xmlCountryIDs);
                if (Session["UserID"] != null)
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString().Trim());

                cmd.ExecuteNonQuery();

                Response.Redirect("~/Pages/State/List");
                lblMessage.Text = "All Selected States Deleted Successfully.";
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
            CheckBox selectAll = (CheckBox)gvState.HeaderRow.FindControl("cbSelectAll");

            foreach (GridViewRow row in gvState.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("cbDeleteMany");

                chk.Checked = selectAll.Checked;
            }
        }
        #endregion

    }
}