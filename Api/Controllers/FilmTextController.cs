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
	[ApiVersion("1.0")]
	[ApiVersion("2.0")]
	public class FilmTextController : ControllerBase
	{
		private readonly sakilaContext _context;

		public FilmTextController(sakilaContext context)
		{
			_context = context;
		}

		// GET: api/FilmText
		[HttpGet, MapToApiVersion("1.0")]
		public async Task<ActionResult<IEnumerable<FilmText>>> GetFilmText()
		{
			return await _context.FilmText.ToListAsync();
		}

		[HttpGet, MapToApiVersion("2.0")]
		public async Task<ActionResult<IEnumerable<FilmText>>> GetAction()
		{
			return await _context.FilmText.Take(3).ToListAsync();
		}
		// GET: api/FilmText/5
		[HttpGet("{id}")]
		public async Task<ActionResult<FilmText>> GetFilmText(short id)
		{
			var filmText = await _context.FilmText.FindAsync(id);

			if (filmText == null)
			{
				return NotFound();
			}

			return filmText;
		}

		// PUT: api/FilmText/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFilmText(short id, FilmText filmText)
		{
			if (id != filmText.FilmId)
			{
				return BadRequest();
			}

			_context.Entry(filmText).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FilmTextExists(id))
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

		// POST: api/FilmText
		[HttpPost]
		public async Task<ActionResult<FilmText>> PostFilmText(FilmText filmText)
		{
			_context.FilmText.Add(filmText);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (FilmTextExists(filmText.FilmId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetFilmText", new { id = filmText.FilmId }, filmText);
		}

		// DELETE: api/FilmText/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<FilmText>> DeleteFilmText(short id)
		{
			var filmText = await _context.FilmText.FindAsync(id);
			if (filmText == null)
			{
				return NotFound();
			}

			_context.FilmText.Remove(filmText);
			await _context.SaveChangesAsync();

			return filmText;
		}

		private bool FilmTextExists(short id)
		{
			return _context.FilmText.Any(e => e.FilmId == id);
		}
	}
}
