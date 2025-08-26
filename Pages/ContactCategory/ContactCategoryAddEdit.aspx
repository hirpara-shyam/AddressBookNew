<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="ContactCategoryAddEdit.aspx.cs" Inherits="AddressBookNew.Pages.ContactCategory.ContactCategoryAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card p-5">
    <div class="row">
        <div class="col-md-12">
            <h1>ContactCategory Add/Edit</h1>
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
        <div class="col-4">
            <asp:Label ID="lblContactCategoryName" runat="server" Text="Contact Category Name"></asp:Label>
        </div>
        <div class="col-8">
            <div class="input-group has-validation">
                <asp:TextBox ID="txtContactCategoryName" runat="server" CssClass="form-control" placeholder="Enter Contact Category Name..."></asp:TextBox>
                <div class="invalid-feedback">Please enter Contact Category Name.</div>
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
            <asp:HyperLink ID="btnCancel" runat="server" CssClass="btn btn-light w-100" Text="Cancel" NavigateUrl="~/Pages/ContactCategory/List" />
        </div>
    </div>

</div>
</asp:Content>
