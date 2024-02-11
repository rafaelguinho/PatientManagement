<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="UpsertPatient.aspx.cs" Inherits="UpsertPatient" %>

<asp:Content ID="ListPatientsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create/Updated new patient</h2>
    <div class="form-group">
        <label for="txtFirstName">First Name:</label>

        <asp:TextBox class="form-control" ID="txtFirstName" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredtxtFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required" class="invalid-field"></asp:RequiredFieldValidator>

    </div>
    <div class="form-group">
        <label for="txtLastName">Last Name:</label>

        <asp:TextBox class="form-control" ID="txtLastName" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredtxtLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required" class="invalid-field"></asp:RequiredFieldValidator>
    </div>


    <div class="form-group">
        <label for="txtLastName">Phone: <small class="form-text text-muted">Ex. (123) 456-7890, 123-456-7890, 123.456.7890, 123 456 7890</small></label>

        <asp:TextBox class="form-control" ID="txtPhone" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone is required" class="invalid-field"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" ControlToValidate="txtPhone"
            ErrorMessage="Invalid phone number" ValidationExpression="^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$" class="invalid-field">
        </asp:RegularExpressionValidator>
    </div>

    <div class="form-group">
        <label for="txtLastName">Email:</label>

        <asp:TextBox class="form-control" ID="txtEmail" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" class="invalid-field"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" class="invalid-field"></asp:RegularExpressionValidator>
    </div>

    <div class="form-group">
        <asp:Label ID="lblGender" runat="server" AssociatedControlID="ddlGender" Text="Gender:"></asp:Label>
        <asp:DropDownList ID="ddlGender" runat="server" class="form-control">
            <asp:ListItem Text="Select Gender" Value=""></asp:ListItem>
            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
            <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator class="invalid-field" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGender" ErrorMessage="Gender is required"></asp:RequiredFieldValidator>
    </div>


    <div class="form-group">
        <asp:Label ID="lblNotes" runat="server" AssociatedControlID="txtNotes" Text="Notes:"></asp:Label>
        <asp:TextBox class="form-control" ID="txtNotes" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
    </div>
    <!-- Add other fields such as Phone, Email, Gender, Notes here -->

    <asp:Button ID="AddButton" class="btn btn-primary" Text="Save" OnClick="FormUpsertPatient_Submit" runat="server" />
</asp:Content>
