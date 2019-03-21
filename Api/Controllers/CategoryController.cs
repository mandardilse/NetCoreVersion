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
	public class CategoryController : ControllerBase
	{
		private readonly sakilaContext _context;

		public CategoryController(sakilaContext context)
		{
			_context = context;
		}

		// GET: api/Category
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
		{
			return await _context.Category.ToListAsync();
		}

		// GET: api/Category/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(byte id)
		{
			var category = await _context.Category.FindAsync(id);

			if (category == null)
			{
				return NotFound();
			}

			return category;
		}

		// PUT: api/Category/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(byte id, Category category)
		{
			if (id != category.CategoryId)
			{
				return BadRequest();
			}

			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
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

		// POST: api/Category
		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(Category category)
		{
			_context.Category.Add(category);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
		}

		// DELETE: api/Category/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Category>> DeleteCategory(byte id)
		{
			var category = await _context.Category.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}

			_context.Category.Remove(category);
			await _context.SaveChangesAsync();

			return category;
		}

		private bool CategoryExists(byte id)
		{
			return _context.Category.Any(e => e.CategoryId == id);
		}
	}
}
