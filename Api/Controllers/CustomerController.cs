using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models.Db;
using Api.Models.Db.Entities;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly sakilaContext _context;

		public CustomerController(sakilaContext context)
		{
			_context = context;
		}

		// GET: api/Customer
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
		{
			return await _context.Customer.ToListAsync();
		}

		// GET: api/Customer/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Customer>> GetCustomer(short id)
		{
			var customer = await _context.Customer.FindAsync(id);

			if (customer == null)
			{
				return NotFound();
			}

			return customer;
		}

		// PUT: api/Customer/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCustomer(short id, Customer customer)
		{
			if (id != customer.CustomerId)
			{
				return BadRequest();
			}

			_context.Entry(customer).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CustomerExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Customer
		[HttpPost]
		public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
		{
			_context.Customer.Add(customer);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
		}

		// DELETE: api/Customer/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Customer>> DeleteCustomer(short id)
		{
			var customer = await _context.Customer.FindAsync(id);
			if (customer == null)
			{
				return NotFound();
			}

			_context.Customer.Remove(customer);
			await _context.SaveChangesAsync();

			return customer;
		}

		private bool CustomerExists(short id)
		{
			return _context.Customer.Any(e => e.CustomerId == id);
		}
	}
}
