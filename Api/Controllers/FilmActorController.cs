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
    public class FilmActorController : ControllerBase
    {
        private readonly sakilaContext _context;

        public FilmActorController(sakilaContext context)
        {
            _context = context;
        }

        // GET: api/FilmActor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmActor>>> GetFilmActor()
        {
            return await _context.FilmActor.ToListAsync();
        }

        // GET: api/FilmActor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmActor>> GetFilmActor(short id)
        {
            var filmActor = await _context.FilmActor.FindAsync(id);

            if (filmActor == null)
            {
                return NotFound();
            }

            return filmActor;
        }

        // PUT: api/FilmActor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmActor(short id, FilmActor filmActor)
        {
            if (id != filmActor.ActorId)
            {
                return BadRequest();
            }

            _context.Entry(filmActor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmActorExists(id))
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

        // POST: api/FilmActor
        [HttpPost]
        public async Task<ActionResult<FilmActor>> PostFilmActor(FilmActor filmActor)
        {
            _context.FilmActor.Add(filmActor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FilmActorExists(filmActor.ActorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFilmActor", new { id = filmActor.ActorId }, filmActor);
        }

        // DELETE: api/FilmActor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmActor>> DeleteFilmActor(short id)
        {
            var filmActor = await _context.FilmActor.FindAsync(id);
            if (filmActor == null)
            {
                return NotFound();
            }

            _context.FilmActor.Remove(filmActor);
            await _context.SaveChangesAsync();

            return filmActor;
        }

        private bool FilmActorExists(short id)
        {
            return _context.FilmActor.Any(e => e.ActorId == id);
        }
    }
}
