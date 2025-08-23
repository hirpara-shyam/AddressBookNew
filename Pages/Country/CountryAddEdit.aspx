<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="CountryAddEdit.aspx.cs" Inherits="AddressBookNew.Pages.Country.CountryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Country Add/Edit</h1>
            </div>
        </div>
        <div class="row">
            <asp:Label ID="lblAddEdit" runat="server" EnableViewState="false"></asp:Label>
        </div>
        <hr />
        <div class="row">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
        </div>

        <div class="row">
            <div class="col-12">
                <asp:Label ID="lblCountryName" runat="server" Text="Country Name"></asp:Label>
                <div class="input-group has-validation">
                    <asp:TextBox ID="txtCountryName" runat="server" CssClass="form-control" placeholder="Enter Country Name..."></asp:TextBox>
                    <div class="invalid-feedback">Please enter Country Name.</div>
                </div>
            </div>
        </div>

        <hr />

        <div class="row">
            <div class="col-md-6">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary w-100" Text="Save" OnClick="btnSave_Click" />
            </div>
            <div class="col-md-6">
                <asp:HyperLink ID="btnCancel" runat="server" CssClass="btn btn-light w-100" Text="Cancel" NavigateUrl="~/Pages/Country/CountryList.aspx" />
            </div>
        </div>

    </div>
</asp:Content>
