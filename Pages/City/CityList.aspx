<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="CityList.aspx.cs" Inherits="AddressBookNew.Pages.City.CityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card p-4">
        <div class="row">
            <div class="col-12">
                <h1>City List</h1>
            </div>
        </div>

        <div class="col-12">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:HyperLink ID="hlAddCity" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Pages/City/Add">Add new City</asp:HyperLink>
                <asp:Button ID="hlExportToExcel" runat="server" CssClass="btn btn-primary" OnClick="ExportToExcelUsingEPPlus" Text="Export to Excel"></asp:Button>
                <asp:Button ID="btnDeleteMultiple" runat="server" CssClass="btn btn-primary" OnClick="btnDeleteMultiple_Click" Text="Delete Selected Rows"></asp:Button>
            </div>
        </div>
        <hr />

        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvCity" runat="server" DataKeyNames="CityID" AutoGenerateColumns="false" CssClass="table table-hover" OnRowCommand="gvCity_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="cbSelectAll" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbDeleteMany" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:BoundField DataField="CityID" HeaderText="City ID" />--%>
                        <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                        <asp:BoundField DataField="StateName" HeaderText="State Name" />
                        <asp:BoundField DataField="CityName" HeaderText="City Name" />

                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink ID="btnEdit" CssClass="btn btn-primary" runat="server" Text="Edit" NavigateUrl='<%# "~/Pages/City/Edit/" + AddressBookNew.EncryptDecrypt.Encrypt(Eval("CityID").ToString().Trim()) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" CommandName="DeleteCity" CommandArgument='<%# Eval("CityID") %>' />
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
