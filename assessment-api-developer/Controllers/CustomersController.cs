using assessment_platform_developer.Models;
using assessment_platform_developer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;

namespace assessment_platform_developer.Controllers
{
    // Use a RoutePrefix so that all routes in this controller are based at /api/customers
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;

        // Dependency injection is used to obtain an instance of ICustomerService.
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET /api/customers
        [HttpGet, Route("")]
        public IHttpActionResult GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        // GET /api/customers/{id}
        [HttpGet, Route("{id:int}", Name = "GetCustomerById")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST /api/customers
        [HttpPost, Route("")]
        public IHttpActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _customerService.AddCustomer(customer);

            // Return a 201 created response along with the new resource URI.
            return CreatedAtRoute("GetCustomerById", new { id = customer.ID }, customer);
        }

        // PUT /api/customers/{id}
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.ID != id)
            {
                return BadRequest("Invalid customer data or ID mismatch");
            }

            var existing = _customerService.GetCustomer(id);
            if (existing == null)
            {
                return NotFound();
            }

            _customerService.UpdateCustomer(customer);
            return Ok(customer);
        }

        // DELETE /api/customers/{id}
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var existing = _customerService.GetCustomer(id);
            if (existing == null)
            {
                return NotFound();
            }

            _customerService.DeleteCustomer(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}