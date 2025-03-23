using assessment_platform_developer.Models;
using System;
using System.Text.RegularExpressions;

namespace assessment_platform_developer.Helper
{
    public static class ValidationHelper
    {
        public static bool ValidateCustomer(Customer customer, out string errorMessage)
        {
            errorMessage = string.Empty;

            // Convert customer.Country: if it’s numeric, get the enum name; otherwise, trim the value.
            string countryText;
            if (string.IsNullOrWhiteSpace(customer.Country))
            {
                countryText = string.Empty;
            }
            else if (int.TryParse(customer.Country, out int countryNumeric))
            {
                countryText = Enum.GetName(typeof(Countries), countryNumeric);
            }
            else
            {
                countryText = customer.Country.Trim();
            }

            // Validate Name
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                errorMessage = "Name is required.";
                return false;
            }

            // Validate Address
            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                errorMessage = "Address is required.";
                return false;
            }

            // Validate Email
            if (string.IsNullOrWhiteSpace(customer.Email) ||
                !Regex.IsMatch(customer.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = "Invalid email address.";
                return false;
            }

            // Validate Phone
            if (!string.IsNullOrWhiteSpace(customer.Phone) &&
                !Regex.IsMatch(customer.Phone, @"^\d+$"))
            {
                errorMessage = "Phone number must contain digits only.";
                return false;
            }

            // Validate City
            if (string.IsNullOrWhiteSpace(customer.City))
            {
                errorMessage = "City is required.";
                return false;
            }

            // Validate State (for US customers only)
            if (!string.IsNullOrWhiteSpace(customer.State) &&
                countryText.Equals("UnitedStates", StringComparison.OrdinalIgnoreCase) &&
                !Enum.TryParse<USStates>(customer.State, true, out USStates parsedState))
            {
                errorMessage = "Invalid State for United States.";
                return false;
            }

            // Validate ZIP Code
            if (!string.IsNullOrWhiteSpace(customer.Zip))
            {
                // For US customers: enforce US ZIP format (##### or #####-####).
                if (countryText.Equals("UnitedStates", StringComparison.OrdinalIgnoreCase))
                {
                    if (!Regex.IsMatch(customer.Zip, @"^\d{5}(-\d{4})?$"))
                    {
                        errorMessage = "ZIP code must match ##### or #####-#### for US customers.";
                        return false;
                    }
                }

                // For Canadian customers: explicitly reject a US ZIP code, and require Canadian postal format.
                if (countryText.Equals("Canada", StringComparison.OrdinalIgnoreCase))
                {
                    if (Regex.IsMatch(customer.Zip, @"^\d{5}(-\d{4})?$"))
                    {
                        errorMessage = "US ZIP code format is not allowed for Canadian customers.";
                        return false;
                    }
                    if (!Regex.IsMatch(customer.Zip, @"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$"))
                    {
                        errorMessage = "Postal code must match A1A 1A1 or A1A1A1 for Canadian customers.";
                        return false;
                    }
                }
            }

            // Validate Country
            if (string.IsNullOrWhiteSpace(countryText))
            {
                errorMessage = "Country is required.";
                return false;
            }

            // Validate Notes (optional length validation)
            if (!string.IsNullOrWhiteSpace(customer.Notes) && customer.Notes.Length > 500)
            {
                errorMessage = "Notes cannot exceed 500 characters.";
                return false;
            }

            // Validate Contact Name
            if (!string.IsNullOrWhiteSpace(customer.ContactName) && customer.ContactName.Length > 100)
            {
                errorMessage = "Contact Name cannot exceed 100 characters.";
                return false;
            }

            // Validate Contact Phone
            if (!string.IsNullOrWhiteSpace(customer.ContactPhone) &&
                !Regex.IsMatch(customer.ContactPhone, @"^\d+$"))
            {
                errorMessage = "Contact Phone must contain digits only.";
                return false;
            }

            // Validate Contact Email
            if (!string.IsNullOrWhiteSpace(customer.ContactEmail) &&
                !Regex.IsMatch(customer.ContactEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = "Invalid Contact Email address.";
                return false;
            }

            // Validate Contact Title
            if (!string.IsNullOrWhiteSpace(customer.ContactTitle) && customer.ContactTitle.Length > 50)
            {
                errorMessage = "Contact Title cannot exceed 50 characters.";
                return false;
            }

            // Validate Contact Notes
            if (!string.IsNullOrWhiteSpace(customer.ContactNotes) && customer.ContactNotes.Length > 500)
            {
                errorMessage = "Contact Notes cannot exceed 500 characters.";
                return false;
            }

            // All validations passed
            return true;
        }
    }
}
