<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="ContactAddEdit.aspx.cs" Inherits="AddressBookNew.Pages.Contact.ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h2>Contact Add Edit</h2>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:Label runat="server" ID="lblMessage" EnableViewState="false" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-8">
                <div class="row">
                    <div class="col-md-4">
                        Contact Name<span class="text-danger">*</span>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control" />
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-4">
                        <label class="form-label" asp-for="Gender">Gender<span class="text-danger">*</span></label><br />
                    </div>
                    <div class="col-md-8">
                        <asp:RadioButtonList ID="rbtnlGender" runat="server">
                            <asp:ListItem Value="Male">Male</asp:ListItem>
                            <asp:ListItem Value="Female">Female</asp:ListItem>
                            <asp:ListItem Value="Other">Other</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-4">
                        <label class="form-label" asp-for="MobileNo">Mobile No<span class="text-danger">*</span></label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-4">
                        <label class="form-label" asp-for="Email">Email<span class="text-danger">*</span></label>
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtEmail" runat="server" Placeholder="example@gmail.com" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col">
                        <label class="form-label" asp-for="CountryID">Country <span class="text-danger">*</span></label><br />
                        <asp:DropDownList ID="ddlCountryID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col">
                        <label class="form-label" asp-for="StateID">State<span class="text-danger">*</span></label><br />
                        <asp:DropDownList ID="ddlStateID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStateID_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col">
                        <label class="form-label" asp-for="CityID">City<span class="text-danger">*</span></label><br />
                        <asp:DropDownList ID="ddlCityID" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <br />
            </div>

            <div class="col-md-4">
                <div class="row">
                    <h5>Contact Category</h5>
                    <asp:CheckBoxList ID="cblContactCategoryID" runat="server" CssClass="ms-3"></asp:CheckBoxList>
                </div>
                <br />

            </div>
        </div>

        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-8">
                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-dark btn-sm" OnClick="btnSave_Click" />
                <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click"></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>
