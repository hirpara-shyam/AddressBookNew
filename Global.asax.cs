using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AddressBookNew
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        }

        public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            #region Country Routes
            routes.MapPageRoute("PagesCountryList",
                                "Pages/Country/List",
                                "~/Pages/Country/CountryList.aspx");

            routes.MapPageRoute("PagesCountryAdd",
                                "Pages/Country/{OperationName}",
                                "~/Pages/Country/CountryAddEdit.aspx");

            routes.MapPageRoute("PagesCountryEdit",
                                "Pages/Country/{OperationName}/{CountryID}",
                                "~/Pages/Country/CountryAddEdit.aspx");
            #endregion

            #region State Routes
            routes.MapPageRoute("PagesStateList",
                                "Pages/State/List",
                                "~/Pages/State/StateList.aspx");

            routes.MapPageRoute("PagesStateAdd",
                                "Pages/State/{OperationName}",
                                "~/Pages/State/StateAddEdit.aspx");

            routes.MapPageRoute("PagesStateEdit",
                                "Pages/State/{OperationName}/{StateID}",
                                "~/Pages/State/StateAddEdit.aspx");
            #endregion

            #region City Routes
            routes.MapPageRoute("PagesCityList",
                                "Pages/City/List",
                                "~/Pages/City/CityList.aspx");

            routes.MapPageRoute("PagesCityAdd",
                                "Pages/City/{OperationName}",
                                "~/Pages/City/CityAddEdit.aspx");

            routes.MapPageRoute("PagesCityEdit",
                                "Pages/City/{OperationName}/{CityID}",
                                "~/Pages/City/CityAddEdit.aspx");
            #endregion

            #region ContactCategory Routes
            routes.MapPageRoute("PagesContactCategoryList",
                                "Pages/ContactCategory/List",
                                "~/Pages/ContactCategory/ContactCategoryList.aspx");

            routes.MapPageRoute("PagesContactCategoryAdd",
                                "Pages/ContactCategory/{OperationName}",
                                "~/Pages/ContactCategory/ContactCategoryAddEdit.aspx");

            routes.MapPageRoute("PagesContactCategoryEdit",
                                "Pages/ContactCategory/{OperationName}/{ContactCategoryID}",
                                "~/Pages/ContactCategory/ContactCategoryAddEdit.aspx");
            #endregion

            #region Contact Routes
            routes.MapPageRoute("PagesContactList",
                                "Pages/Contact/List",
                                "~/Pages/Contact/ContactList.aspx");

            routes.MapPageRoute("PagesContactAdd",
                                "Pages/Contact/{OperationName}",
                                "~/Pages/Contact/ContactAddEdit.aspx");

            routes.MapPageRoute("PagesContactEdit",
                                "Pages/Contact/{OperationName}/{ContactID}",
                                "~/Pages/Contact/ContactAddEdit.aspx");
            #endregion

            #region Profile Route
            routes.MapPageRoute("PagesProfile",
                                "Pages/Profile",
                                "~/Pages/Profile.aspx");
            #endregion

            #region Login Route
            routes.MapPageRoute("PagesLogin",
                                "Pages/Login",
                                "~/Pages/Login.aspx");
            #endregion

            #region Register Route
            routes.MapPageRoute("PagesRegister",
                                "Pages/Register",
                                "~/Pages/Register.aspx");
            #endregion
        }
    }
}