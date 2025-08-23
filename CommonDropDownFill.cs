using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AddressBookNew
{
    public class CommonDropDownFill
    {
        public static void FillCountryDropDown(DropDownList ddl, String userID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_Country_SelectForDropDownList]";

                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "CountryID";
                    ddl.DataTextField = "CountryName";
                    ddl.DataBind();
                }

                ddl.Items.Insert(0, new ListItem("Select Country", "-1"));
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }

        public static void FillStateDropDown(DropDownList ddl, String userID, String countryID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_State_SelectForDropDownList]";

                cmd.Parameters.AddWithValue("@CountryID", countryID);
                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "StateID";
                    ddl.DataTextField = "StateName";
                    ddl.DataBind();
                }

                ddl.Items.Insert(0, new ListItem("Select State", "-1"));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }

        public static void FillCityDropDown(DropDownList ddl, String userID, String stateID)
        {
            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
            try
            {
                if (objConn.State != System.Data.ConnectionState.Open)
                    objConn.Open();

                SqlCommand cmd = objConn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[PR_City_SelectForDropDownList]";

                cmd.Parameters.AddWithValue("@StateID", stateID);
                cmd.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader objSDR = cmd.ExecuteReader();

                if (objSDR.HasRows)
                {
                    ddl.DataSource = objSDR;
                    ddl.DataValueField = "CityID";
                    ddl.DataTextField = "CityName";
                    ddl.DataBind();
                }

                ddl.Items.Insert(0, new ListItem("Select City", "-1"));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (objConn.State == System.Data.ConnectionState.Open)
                    objConn.Close();
            }
        }
    }
}