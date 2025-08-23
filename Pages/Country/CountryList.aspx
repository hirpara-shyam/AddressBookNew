<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="CountryList.aspx.cs" Inherits="AddressBookNew.Pages.CountryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <h1>Country List</h1>
            </div>
        </div>

        <div class="col-12">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:HyperLink ID="hlAddCountry" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Pages/Country/CountryAddEdit.aspx">Add new Country</asp:HyperLink>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" OnRowCommand="gvCountry_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="CountryID" HeaderText="Country ID" />
                        <asp:BoundField DataField="CountryName" HeaderText="Country Name" />

                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink ID="btnEdit" CssClass="btn btn-primary" runat="server" Text="Edit" NavigateUrl='<%# "~/Pages/Country/CountryAddEdit.aspx?CountryID=" + Eval("CountryID").ToString().Trim() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" CommandName="DeleteCountry" CommandArgument='<%# Eval("CountryID") %>' />
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
