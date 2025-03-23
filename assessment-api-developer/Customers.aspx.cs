using assessment_platform_developer.Helper;
using assessment_platform_developer.Models;
using assessment_platform_developer.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace assessment_platform_developer
{
    public partial class Customers : Page
    {
        // Local customer list used for UI binding.
        private static List<Customer> customers = new List<Customer>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Hide the error label on every load.
            lblError.Visible = false;

            if (!IsPostBack)
            {
                // Retrieve the DI container and load customers.
                var container = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = container.GetInstance<ICustomerService>();

                customers = customerService.GetAllCustomers().ToList();
                ViewState["Customers"] = customers;

                // Bind the customers drop-down and other dropdowns on initial load.
                PopulateCustomerListBox();
                PopulateCustomerDropDownLists();
            }
            else
            {
                // Retrieve customers from ViewState on postbacks.
                customers = (List<Customer>)ViewState["Customers"];
            }
        }

        private void PopulateCustomerDropDownLists()
        {
            // Bind Country drop-down from Countries enum.
            var countryList = Enum.GetValues(typeof(Countries))
                .Cast<Countries>()
                .Select(c => new ListItem
                {
                    Text = c.ToString(),
                    Value = ((int)c).ToString()
                })
                .ToArray();

            CountryDropDownList.Items.Clear();
            CountryDropDownList.Items.AddRange(countryList);

            // Set default selection on first load only.
            if (!IsPostBack)
                CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();

            // Bind State/Province drop-down from CanadianProvinces enum.
            var provinceList = Enum.GetValues(typeof(CanadianProvinces))
                .Cast<CanadianProvinces>()
                .Select(p => new ListItem
                {
                    Text = p.ToString(),
                    Value = ((int)p).ToString()
                })
                .ToArray();

            StateDropDownList.Items.Clear();
            StateDropDownList.Items.Add(new ListItem("")); // Option for no selection.
            StateDropDownList.Items.AddRange(provinceList);
        }

        protected void PopulateCustomerListBox()
        {
            // Preserve the user's currently selected value (if any).
            string selectedValue = CustomersDDL.SelectedValue;

            CustomersDDL.Items.Clear();
            var listItems = customers.Select(c => new ListItem(c.Name, c.ID.ToString())).ToArray();

            if (listItems.Length > 0)
            {
                CustomersDDL.Items.AddRange(listItems);
                // If a previous selection was preserved, try to restore it.
                if (!string.IsNullOrEmpty(selectedValue) && CustomersDDL.Items.FindByValue(selectedValue) != null)
                    CustomersDDL.SelectedValue = selectedValue;
                else
                    CustomersDDL.SelectedIndex = 0;
            }
            else
            {
                CustomersDDL.Items.Add(new ListItem("Add new customer"));
            }
        }

        // Fired when the customer drop-down selection is changed.
        protected void CustomersDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedID;
            if (int.TryParse(CustomersDDL.SelectedValue, out selectedID))
            {
                var selectedCustomer = customers.FirstOrDefault(c => c.ID == selectedID);
                if (selectedCustomer != null)
                {
                    // Load the selected customer's details into the form fields.
                    CustomerName.Text = selectedCustomer.Name;
                    CustomerAddress.Text = selectedCustomer.Address;
                    CustomerEmail.Text = selectedCustomer.Email;
                    CustomerPhone.Text = selectedCustomer.Phone;
                    CustomerCity.Text = selectedCustomer.City;
                    StateDropDownList.SelectedValue = selectedCustomer.State;
                    CustomerZip.Text = selectedCustomer.Zip;
                    CountryDropDownList.SelectedValue = selectedCustomer.Country;
                    CustomerNotes.Text = selectedCustomer.Notes;
                    ContactName.Text = selectedCustomer.ContactName;
                    ContactEmail.Text = selectedCustomer.ContactEmail;
                    ContactPhone.Text = selectedCustomer.ContactPhone;
                }
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new customer object from the form fields.
                var customer = new Customer
                {
                    Name = CustomerName.Text.Trim(),
                    Address = CustomerAddress.Text.Trim(),
                    City = CustomerCity.Text.Trim(),
                    State = StateDropDownList.SelectedValue,
                    Zip = CustomerZip.Text.Trim(),
                    Country = CountryDropDownList.SelectedValue,
                    Email = CustomerEmail.Text.Trim(),
                    Phone = CustomerPhone.Text.Trim(),
                    Notes = CustomerNotes.Text.Trim(),
                    ContactName = ContactName.Text.Trim(),
                    ContactEmail = ContactEmail.Text.Trim(),
                    ContactPhone = ContactPhone.Text.Trim()
                };

                // Validate with ValidationHelper.
                string errorMessage;
                if (!ValidationHelper.ValidateCustomer(customer, out errorMessage))
                {
                    lblError.Text = errorMessage;
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Visible = true;
                    return;
                }

                var container = (Container)HttpContext.Current.Application["DIContainer"];
                var customerService = container.GetInstance<ICustomerService>();
                customerService.AddCustomer(customer);

                // Update local list and ViewState.
                customers.Add(customer);
                ViewState["Customers"] = customers;
                PopulateCustomerListBox();
                ClearFormFields();

                lblError.Text = "Customer added successfully!";
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred: " + ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int selectedID;
            if (!int.TryParse(CustomersDDL.SelectedValue, out selectedID))
            {
                lblError.Text = "Please select a valid customer to update.";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            var customerToUpdate = customers.FirstOrDefault(c => c.ID == selectedID);
            if (customerToUpdate == null)
            {
                lblError.Text = "Customer not found.";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            // Update the customer properties from the form.
            customerToUpdate.Name = CustomerName.Text.Trim();
            customerToUpdate.Address = CustomerAddress.Text.Trim();
            customerToUpdate.Email = CustomerEmail.Text.Trim();
            customerToUpdate.Phone = CustomerPhone.Text.Trim();
            customerToUpdate.City = CustomerCity.Text.Trim();
            customerToUpdate.State = StateDropDownList.SelectedValue;
            customerToUpdate.Zip = CustomerZip.Text.Trim();
            customerToUpdate.Country = CountryDropDownList.SelectedValue;
            customerToUpdate.Notes = CustomerNotes.Text.Trim();
            customerToUpdate.ContactName = ContactName.Text.Trim();
            customerToUpdate.ContactEmail = ContactEmail.Text.Trim();
            customerToUpdate.ContactPhone = ContactPhone.Text.Trim();

            string errorMessage;
            if (!ValidationHelper.ValidateCustomer(customerToUpdate, out errorMessage))
            {
                lblError.Text = errorMessage;
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            var container = (Container)HttpContext.Current.Application["DIContainer"];
            var customerService = container.GetInstance<ICustomerService>();
            customerService.UpdateCustomer(customerToUpdate);

            ViewState["Customers"] = customers;
            PopulateCustomerListBox();

            lblError.Text = "Customer updated successfully!";
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Visible = true;
            ClearFormFields();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int selectedID;
            if (!int.TryParse(CustomersDDL.SelectedValue, out selectedID))
            {
                lblError.Text = "Please select a valid customer to delete.";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            var container = (Container)HttpContext.Current.Application["DIContainer"];
            var customerService = container.GetInstance<ICustomerService>();

            try
            {
                customerService.DeleteCustomer(selectedID);
                customers.RemoveAll(c => c.ID == selectedID);
                ViewState["Customers"] = customers;
                PopulateCustomerListBox();

                lblError.Text = "Customer deleted successfully!";
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Visible = true;
                ClearFormFields();
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred while deleting the customer: " + ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
            }
        }

        private void ClearFormFields()
        {
            CustomerName.Text = string.Empty;
            CustomerAddress.Text = string.Empty;
            CustomerEmail.Text = string.Empty;
            CustomerPhone.Text = string.Empty;
            CustomerCity.Text = string.Empty;
            StateDropDownList.SelectedIndex = 0;
            CustomerZip.Text = string.Empty;
            CountryDropDownList.SelectedIndex = 0;
            CustomerNotes.Text = string.Empty;
            ContactName.Text = string.Empty;
            ContactEmail.Text = string.Empty;
            ContactPhone.Text = string.Empty;
        }
    }
}
