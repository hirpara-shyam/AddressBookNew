<%@ Page Title="" Language="C#" MasterPageFile="~/Content/NewAddressBook.Master" AutoEventWireup="true" CodeBehind="StateList.aspx.cs" Inherits="AddressBookNew.Pages.State.StateList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="row">
        <div class="col-12">
            <h1>State List</h1>
        </div>
    </div>

    <div class="col-12">
        <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink ID="hlAddState" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Pages/State/StateAddEdit.aspx">Add new State</asp:HyperLink>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" OnRowCommand="gvState_RowCommand">
                <Columns>
                    <asp:BoundField DataField="StateID" HeaderText="State ID" />
                    <asp:BoundField DataField="CountryName" HeaderText="Country Name" />
                    <asp:BoundField DataField="StateName" HeaderText="State Name" />

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink ID="btnEdit" CssClass="btn btn-primary" runat="server" Text="Edit" NavigateUrl='<%# "~/Pages/State/StateAddEdit.aspx?StateID=" + Eval("StateID").ToString().Trim() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" CommandName="DeleteState" CommandArgument='<%# Eval("StateID") %>' />
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
