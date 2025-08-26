<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="CountryList.aspx.cs" Inherits="AddressBookNew.Pages.CountryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card p-4">
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
                <asp:HyperLink ID="hlAddCountry" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Pages/Country/Add">Add new Country</asp:HyperLink>
                <asp:Button ID="hlExportToExcel" runat="server" CssClass="btn btn-primary" OnClick="ExportToExcelUsingEPPlus" Text="Export to Excel"></asp:Button>
                <asp:Button ID="btnDeleteMultiple" runat="server" CssClass="btn btn-primary" OnClick="btnDeleteMultiple_Click" Text="Delete Selected Rows"></asp:Button>
            </div>
        </div>
        <hr />

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvCountry" runat="server" DataKeyNames="CountryID" AutoGenerateColumns="false" CssClass="table table-hover" OnRowCommand="gvCountry_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="cbSelectAll" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbDeleteMany" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:BoundField DataField="CountryID" HeaderText="Country ID" />--%>
                        <asp:BoundField DataField="CountryName" HeaderText="Country Name" />

                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink ID="btnEdit" CssClass="btn btn-primary" runat="server" Text="Edit" NavigateUrl='<%# "~/Pages/Country/Edit/" + AddressBookNew.EncryptDecrypt.Encrypt(Eval("CountryID").ToString().Trim()) %>' />
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
