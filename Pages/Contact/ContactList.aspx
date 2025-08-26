<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="AddressBookNew.Pages.Contact.ContactList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container card p-4">
        <div class="row">
            <div class="col-md-12">
                <h2>Contact List</h2>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" ID="lblMessage" EnableViewState="false"></asp:Label>
                    </div>
                </div>
                <p>
                    <asp:HyperLink ID="hlContactAdd" runat="server" CssClass="btn btn-primary" Text="Add New Contact" NavigateUrl="~/Pages/Contact/Add" />
                    <asp:Button ID="hlExportToExcel" runat="server" CssClass="btn btn-primary" OnClick="ExportToExcelUsingEPPlus" Text="Export to Excel"></asp:Button>
                    <asp:Button ID="btnDeleteMultiple" runat="server" CssClass="btn btn-primary" OnClick="btnDeleteMultiple_Click" Text="Delete Selected Rows"></asp:Button>
                </p>
            </div>
        </div>
        <hr />
        
        <div class="row">
            <div class="col-md-12">
                <div>
                    <asp:GridView ID="gvContact" runat="server" DataKeyNames="ContactID" OnRowCommand="gvContact_RowCommand" AutoGenerateColumns="false" CssClass="table table-hover">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="cbSelectAll" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbDeleteMany" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:BoundField DataField="ContactID" HeaderText="ID" />--%>
                            <asp:BoundField DataField="ContactName" HeaderText="Contact" />

                            <asp:BoundField DataField="Gender" HeaderText="Gender" />
                            <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="CityName" HeaderText="City" />

                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="hlEdit" Text="Edit" CssClass="btn btn-primary btn-sm" NavigateUrl='<%# "~/Pages/Contact/Edit/" + AddressBookNew.EncryptDecrypt.Encrypt(Eval("ContactID").ToString().Trim()) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteContact" CommandArgument='<%# Eval("ContactID").ToString() %>' />
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
    </div>
</asp:Content>
