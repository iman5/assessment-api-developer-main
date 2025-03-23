using assessment_platform_developer.Helper;
using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using System;
using System.Collections.Generic;

namespace assessment_platform_developer.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            return _customerRepository.Get(id);
        }

        public void AddCustomer(Customer customer)
        {
            // Validate customer data
            if (!ValidationHelper.ValidateCustomer(customer, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            _customerRepository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            // Validate customer data
            if (!ValidationHelper.ValidateCustomer(customer, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.Delete(id);
        }
    }
}
