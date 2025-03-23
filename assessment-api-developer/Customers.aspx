<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="assessment_platform_developer.Customers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title><%: Page.Title %> RPM API Developer Assessment</title>
  <asp:PlaceHolder runat="server">
    <%: Scripts.Render("~/bundles/modernizr") %>
  </asp:PlaceHolder>
  <webopt:bundlereference runat="server" path="~/Content/css" />
  <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
      <div class="container body-content">
        <a class="navbar-brand" runat="server" href="~/">RPM API Developer Assessment</a>
        <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse"
                title="Toggle navigation" aria-controls="navbarSupportedContent" aria-expanded="false"
                aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
          <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
              <a class="nav-link" runat="server" href="~/">Home</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" runat="server" href="~/Customers">Customers</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    
    <!-- Customer Registry drop-down -->
    <div class="container body-content">
      <h2>Customer Registry</h2>
      <asp:DropDownList ID="CustomersDDL" runat="server" CssClass="form-control" AutoPostBack="true"
          OnSelectedIndexChanged="CustomersDDL_SelectedIndexChanged" />
    </div>
    
    <!-- Customer Add / Edit Form -->
    <div class="container body-content">
      <div class="card">
        <div class="card-body">
          <div class="row justify-content-center">
            <div class="col-md-6">
              <h1>Add / Edit Customer</h1>
              
              <!-- Validation Summary for field-level errors -->
              <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="alert alert-danger"
                  HeaderText="Please correct the following errors:" DisplayMode="BulletList" />
              <br />
              <!-- lblError displays additional status messages -->
              <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger" Visible="false" />
              <br />
              <!-- CUSTOMER FIELDS -->
              <div class="form-group">
                <asp:Label ID="CustomerNameLabel" runat="server" Text="Name" CssClass="form-label" />
                <asp:TextBox ID="CustomerName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="ReqCustomerName" runat="server" ControlToValidate="CustomerName"
                    ErrorMessage="Name is required." Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerAddressLabel" runat="server" Text="Address" CssClass="form-label" />
                <asp:TextBox ID="CustomerAddress" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="ReqCustomerAddress" runat="server" ControlToValidate="CustomerAddress"
                    ErrorMessage="Address is required." Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerEmailLabel" runat="server" Text="Email" CssClass="form-label" />
                <asp:TextBox ID="CustomerEmail" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="ReqCustomerEmail" runat="server" ControlToValidate="CustomerEmail"
                    ErrorMessage="Email is required." Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="RegexCustomerEmail" runat="server" ControlToValidate="CustomerEmail"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Invalid email format." 
                    Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerPhoneLabel" runat="server" Text="Phone" CssClass="form-label" />
                <asp:TextBox ID="CustomerPhone" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="RegexCustomerPhone" runat="server" ControlToValidate="CustomerPhone"
                    ValidationExpression="^\d+$" ErrorMessage="Phone must contain digits only." 
                    Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerCityLabel" runat="server" Text="City" CssClass="form-label" />
                <asp:TextBox ID="CustomerCity" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="ReqCustomerCity" runat="server" ControlToValidate="CustomerCity"
                    ErrorMessage="City is required." Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerStateLabel" runat="server" Text="Province/State" CssClass="form-label" />
                <asp:DropDownList ID="StateDropDownList" runat="server" CssClass="form-control" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerZipLabel" runat="server" Text="Postal/Zip Code" CssClass="form-label" />
                <asp:TextBox ID="CustomerZip" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="ReqCustomerZip" runat="server" ControlToValidate="CustomerZip"
                    ErrorMessage="Postal/Zip Code is required." Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="RegexCustomerZip" runat="server" ControlToValidate="CustomerZip"
                    ValidationExpression="^\d{5}(-\d{4})?$" ErrorMessage="Invalid Postal/Zip Code format." 
                    Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerCountryLabel" runat="server" Text="Country" CssClass="form-label" />
                <asp:DropDownList ID="CountryDropDownList" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="ReqCustomerCountry" runat="server" ControlToValidate="CountryDropDownList"
                    InitialValue="" ErrorMessage="Country is required." Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="CustomerNotesLabel" runat="server" Text="Notes" CssClass="form-label" />
                <asp:TextBox ID="CustomerNotes" runat="server" CssClass="form-control" TextMode="MultiLine" />
              </div>
              
              <!-- CUSTOMER CONTACT DETAILS -->
              <h1>Customer Contact Details</h1>
              
              <div class="form-group">
                <asp:Label ID="ContactNameLabel" runat="server" Text="Name" CssClass="form-label" />
                <asp:TextBox ID="ContactName" runat="server" CssClass="form-control" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="ContactEmailLabel" runat="server" Text="Email" CssClass="form-label" />
                <asp:TextBox ID="ContactEmail" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="RegexContactEmail" runat="server" ControlToValidate="ContactEmail"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="Invalid contact email format." 
                    Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <div class="form-group">
                <asp:Label ID="ContactPhoneLabel" runat="server" Text="Phone" CssClass="form-label" />
                <asp:TextBox ID="ContactPhone" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="RegexContactPhone" runat="server" ControlToValidate="ContactPhone"
                    ValidationExpression="^\d+$" ErrorMessage="Contact phone must contain digits only." 
                    Display="Dynamic" CssClass="text-danger" />
              </div>
              
              <!-- ACTION BUTTONS -->
              <div class="form-group">
                <asp:Button ID="AddButton" runat="server" CssClass="btn btn-primary btn-md" Text="Add"
                    OnClick="AddButton_Click" />
                <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-secondary btn-md" Text="Update"
                    OnClick="UpdateButton_Click" />
                <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-danger btn-md" Text="Delete"
                    OnClick="DeleteButton_Click" />
              </div>
              
            </div>
          </div>
        </div>
      </div>
    </div>
    
  </form>
</body>
</html>
