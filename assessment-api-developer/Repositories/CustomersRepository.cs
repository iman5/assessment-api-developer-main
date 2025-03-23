using assessment_platform_developer.Models;
using assessment_platform_developer.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace assessment_platform_developer.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDBContext _context;

        public CustomerRepository(CustomerDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public void Add(Customer customer)
        {
            // Validate customer data before adding
            if (!ValidationHelper.ValidateCustomer(customer, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            // Validate customer data before updating
            if (!ValidationHelper.ValidateCustomer(customer, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            var existingCustomer = _context.Customers.Find(customer.ID);
            if (existingCustomer != null)
            {
                // Update all fields from the incoming customer object.
                existingCustomer.Name = customer.Name;
                existingCustomer.Address = customer.Address;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.City = customer.City;
                existingCustomer.State = customer.State;
                existingCustomer.Zip = customer.Zip;
                existingCustomer.Country = customer.Country;
                existingCustomer.Notes = customer.Notes;
                existingCustomer.ContactName = customer.ContactName;
                existingCustomer.ContactPhone = customer.ContactPhone;
                existingCustomer.ContactEmail = customer.ContactEmail;
                existingCustomer.ContactTitle = customer.ContactTitle;
                existingCustomer.ContactNotes = customer.ContactNotes;

                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Customer with ID {customer.ID} not found.");
            }
        }

        public void Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
        }
    }
}
