<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="CityAddEdit.aspx.cs" Inherits="AddressBookNew.Pages.City.CityAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card p-5">
        <div class="row">
            <div class="col-md-12">
                <h1>City Add/Edit</h1>
            </div>
        </div>
        <div class="row">
            <asp:Label ID="lblAddEdit" runat="server" EnableViewState="false"></asp:Label>
        </div>
        <hr />
        <div class="row">
            <asp:Label ID="lblMessage" CssClass="text-danger" runat="server" EnableViewState="false"></asp:Label>
        </div>

        <div class="row">
            <div class="col-4">
                <asp:Label ID="lblCountryName" runat="server" Text="Country Name"></asp:Label>
            </div>
            <div class="col-8">
                <div class="input-group has-validation">
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                    <div class="invalid-feedback">Please select Country Name.</div>
                </div>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-4">
                <asp:Label ID="lblStateName" runat="server" Text="State Name"></asp:Label>
            </div>
            <div class="col-8">
                <div class="input-group has-validation">
                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    <div class="invalid-feedback">Please select State Name.</div>
                </div>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-4">
                <asp:Label ID="lblCityName" runat="server" Text="City Name"></asp:Label>
            </div>
            <div class="col-8">
                <div class="input-group has-validation">
                    <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" placeholder="Enter City Name..."></asp:TextBox>
                    <div class="invalid-feedback">Please enter City Name.</div>
                </div>
            </div>
        </div>

        <hr />


        <div class="row">
            <div class="col-md-4">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary w-100" Text="Save" OnClick="btnSave_Click" />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnAddMore" runat="server" CssClass="btn btn-primary w-100" Text="Add More" OnClick="btnAddMore_Click" />
            </div>
            <div class="col-md-4">
                <asp:HyperLink ID="btnCancel" runat="server" CssClass="btn btn-light w-100" Text="Cancel" NavigateUrl="~/Pages/City/List" />
            </div>
        </div>

    </div>
</asp:Content>
