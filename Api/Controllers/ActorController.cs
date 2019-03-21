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
	public class ActorController : ControllerBase
	{
		private readonly sakilaContext _context;

		public ActorController(sakilaContext context)
		{
			_context = context;
		}

		// GET: api/Actor
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Actor>>> GetActor()
		{
			return await _context.Actor.ToListAsync();
		}

		// GET: api/Actor/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Actor>> GetActor(short id)
		{
			var actor = await _context.Actor.FindAsync(id);

			if (actor == null)
			{
				return NotFound();
			}

			return actor;
		}

		// PUT: api/Actor/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutActor(short id, Actor actor)
		{
			if (id != actor.ActorId)
			{
				return BadRequest();
			}

			_context.Entry(actor).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ActorExists(id))
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

		// POST: api/Actor
		[HttpPost]
		public async Task<ActionResult<Actor>> PostActor(Actor actor)
		{
			_context.Actor.Add(actor);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetActor", new { id = actor.ActorId }, actor);
		}

		// DELETE: api/Actor/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Actor>> DeleteActor(short id)
		{
			var actor = await _context.Actor.FindAsync(id);
			if (actor == null)
			{
				return NotFound();
			}

			_context.Actor.Remove(actor);
			await _context.SaveChangesAsync();

			return actor;
		}

		private bool ActorExists(short id)
		{
			return _context.Actor.Any(e => e.ActorId == id);
		}
	}
}
