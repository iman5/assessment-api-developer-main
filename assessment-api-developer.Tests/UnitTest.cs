using Microsoft.VisualStudio.TestTools.UnitTesting;
using assessment_platform_developer.Models;
using assessment_platform_developer.Helper;

namespace assessment_platform_developer.Tests
{
    [TestClass]
    public class CustomerValidationTests
    {
        [TestMethod]
        public void ValidateCustomer_WithValidFields_ReturnsTrue()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Park Street",
                City = "Toronto",
                State = ((int)CanadianProvinces.Ontario).ToString(), 
                Zip = "M5V3G5",
                Country = ((int)Countries.Canada).ToString(),
                Email = "john.doe@example.com",
                Phone = "4165551234",
                ContactName = "Jane Doe",
                ContactEmail = "jane.doe@example.com",
                ContactPhone = "4165555678"
            };
            string errorMessage;

            // Act
            bool isValid = ValidationHelper.ValidateCustomer(customer, out errorMessage);

            // Assert
            Assert.IsTrue(isValid, "A valid customer should pass validation.");
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage));
        }

        [TestMethod]
        public void ValidateCustomer_WithEmptyName_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "",  // Invalid: Empty name.
                Address = "123 Park Street",
                City = "Toronto",
                State = ((int)CanadianProvinces.Ontario).ToString(),
                Zip = "M5V3G5",
                Country = ((int)Countries.Canada).ToString(),
                Email = "john.doe@example.com",
                Phone = "4165551234"
            };

            string errorMessage;

            // Act
            bool isValid = ValidationHelper.ValidateCustomer(customer, out errorMessage);

            // Assert
            Assert.IsFalse(isValid, "Customer with an empty name should fail validation.");
            Assert.IsTrue(errorMessage.Contains("Name"), "Error message should mention the 'Name' field.");
        }

        [TestMethod]
        public void ValidateCustomer_WithNonNumericPhone_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Park Street",
                City = "Toronto",
                State = ((int)CanadianProvinces.Ontario).ToString(),
                Zip = "M5V3G5",
                Country = ((int)Countries.Canada).ToString(),
                Email = "john.doe@example.com",
                Phone = "ABCDEF",   // Non-numeric value.
            };

            string errorMessage;

            // Act
            bool isValid = ValidationHelper.ValidateCustomer(customer, out errorMessage);

            // Assert
            Assert.IsFalse(isValid, "Customer with a non-numeric phone number should fail validation.");
            Assert.IsTrue(errorMessage.Contains("Phone"), "Error message should mention the 'Phone' field.");
        }

        [TestMethod]
        public void ValidateCustomer_WithInvalidEmail_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Park Street",
                City = "Toronto",
                State = ((int)CanadianProvinces.Ontario).ToString(),
                Zip = "M5V3G5",
                Country = ((int)Countries.Canada).ToString(),
                Email = "not-an-email",  // Invalid email address.
                Phone = "4165551234"
            };

            string errorMessage;

            // Act
            bool isValid = ValidationHelper.ValidateCustomer(customer, out errorMessage);

            // Assert
            Assert.IsFalse(isValid, "Customer with an invalid email should fail validation.");
        }


    }
}
