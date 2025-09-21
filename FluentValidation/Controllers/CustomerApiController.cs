using AutoMapper;
using FluentValidation;
using FluentValidationApp.DTOs;
using FluentValidationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluentValidationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Customer> _validator;
        private readonly IMapper _mapper;

        public CustomerApiController(AppDbContext context, IValidator<Customer> validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        [Route("MappingSample")]
        [HttpGet]
        public ActionResult MappingSample()
        {
            Customer customer = new Customer { Id = 1,
                                               Name="Kadir Batlar",
                                               Email="kadir@gmail.com",
                                               Age=22,
                                               CreditCard = new CreditCard {Number = "123", ValidDate=DateTime.Now}};

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        // GET: api/customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                                         .Include(c => c.Addresses)
                                         .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return customer;
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var vresult = await _validator.ValidateAsync(customer);
            if (!vresult.IsValid)
            {
                return BadRequest(vresult.Errors.Select(x=> new {property = x.PropertyName, error = x.ErrorMessage}));
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Customers.Any(c => c.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}