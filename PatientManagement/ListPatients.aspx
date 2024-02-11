<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ListPatients.aspx.cs" Inherits="ListPatients" %>

<asp:Content ID="ListPatientsContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h2>List of Patients</h2>

        <a class="nav-link" runat="server" href="~/UpsertPatient">Create new patient</a>

        <asp:GridView ID="GridViewPatients"
            runat="server"
            CssClass="table table-striped"
            AutoGenerateColumns="False"
            OnRowCommand="GridViewPatients_RowCommand"
            OnRowDeleting="GridViewPatients_RowDeleting"
            >
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="Email" HeaderText="Email" />

                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                <asp:BoundField DataField="LastUpdatedDate" HeaderText="Last Updated Date" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Edit" CssClass="btn btn-primary" CommandName="Edit" CommandArgument='<%# Eval("Email") %>' />
                        <asp:Button runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("Email") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
