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
	public class CountryController : ControllerBase
	{
		private readonly sakilaContext _context;

		public CountryController(sakilaContext context)
		{
			_context = context;
		}

		// GET: api/Country
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
		{
			return await _context.Country.ToListAsync();
		}

		// GET: api/Country/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Country>> GetCountry(short id)
		{
			var country = await _context.Country.FindAsync(id);

			if (country == null)
			{
				return NotFound();
			}

			return country;
		}

		// PUT: api/Country/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCountry(short id, Country country)
		{
			if (id != country.CountryId)
			{
				return BadRequest();
			}

			_context.Entry(country).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CountryExists(id))
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

		// POST: api/Country
		[HttpPost]
		public async Task<ActionResult<Country>> PostCountry(Country country)
		{
			_context.Country.Add(country);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
		}

		// DELETE: api/Country/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Country>> DeleteCountry(short id)
		{
			var country = await _context.Country.FindAsync(id);
			if (country == null)
			{
				return NotFound();
			}

			_context.Country.Remove(country);
			await _context.SaveChangesAsync();

			return country;
		}

		private bool CountryExists(short id)
		{
			return _context.Country.Any(e => e.CountryId == id);
		}
	}
}
