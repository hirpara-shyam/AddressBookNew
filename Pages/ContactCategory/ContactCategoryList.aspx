<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="ContactCategoryList.aspx.cs" Inherits="AddressBookNew.Pages.ContactCategory.ContactCategoryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="row">
        <div class="col-12">
            <h1>ContactCategory List</h1>
        </div>
    </div>

    <div class="col-12">
        <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink ID="hlAddContactCategory" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Pages/ContactCategory/ContactCategoryAddEdit.aspx">Add new ContactCategory</asp:HyperLink>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvContactCategory" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" OnRowCommand="gvContactCategory_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ContactCategoryID" HeaderText="ContactCategory ID" />
                    <asp:BoundField DataField="ContactCategoryName" HeaderText="ContactCategory Name" />

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink ID="btnEdit" CssClass="btn btn-primary" runat="server" Text="Edit" NavigateUrl='<%# "~/Pages/ContactCategory/ContactCategoryAddEdit.aspx?ContactCategoryID=" + Eval("ContactCategoryID").ToString().Trim() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" CommandName="DeleteContactCategory" CommandArgument='<%# Eval("ContactCategoryID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="col-12">
        <asp:Label ID="lblDataMessage" runat="server" EnableViewState="false"></asp:Label>
    </div>
</div>
</asp:Content>
