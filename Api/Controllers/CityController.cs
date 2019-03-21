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
	public class CityController : ControllerBase
	{
		private readonly sakilaContext _context;

		public CityController(sakilaContext context)
		{
			_context = context;
		}

		// GET: api/City
		[HttpGet]
		public async Task<ActionResult<IEnumerable<City>>> GetCity()
		{
			return await _context.City.ToListAsync();
		}

		// GET: api/City/5
		[HttpGet("{id}")]
		public async Task<ActionResult<City>> GetCity(short id)
		{
			var city = await _context.City.FindAsync(id);

			if (city == null)
			{
				return NotFound();
			}

			return city;
		}

		// PUT: api/City/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCity(short id, City city)
		{
			if (id != city.CityId)
			{
				return BadRequest();
			}

			_context.Entry(city).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CityExists(id))
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

		// POST: api/City
		[HttpPost]
		public async Task<ActionResult<City>> PostCity(City city)
		{
			_context.City.Add(city);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCity", new { id = city.CityId }, city);
		}

		// DELETE: api/City/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<City>> DeleteCity(short id)
		{
			var city = await _context.City.FindAsync(id);
			if (city == null)
			{
				return NotFound();
			}

			_context.City.Remove(city);
			await _context.SaveChangesAsync();

			return city;
		}

		private bool CityExists(short id)
		{
			return _context.City.Any(e => e.CityId == id);
		}
	}
}
