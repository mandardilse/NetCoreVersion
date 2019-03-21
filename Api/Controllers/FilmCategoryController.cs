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
    public class FilmCategoryController : ControllerBase
    {
        private readonly sakilaContext _context;

        public FilmCategoryController(sakilaContext context)
        {
            _context = context;
        }

        // GET: api/FilmCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmCategory>>> GetFilmCategory()
        {
            return await _context.FilmCategory.ToListAsync();
        }

        // GET: api/FilmCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmCategory>> GetFilmCategory(short id)
        {
            var filmCategory = await _context.FilmCategory.FindAsync(id);

            if (filmCategory == null)
            {
                return NotFound();
            }

            return filmCategory;
        }

        // PUT: api/FilmCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmCategory(short id, FilmCategory filmCategory)
        {
            if (id != filmCategory.FilmId)
            {
                return BadRequest();
            }

            _context.Entry(filmCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmCategoryExists(id))
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

        // POST: api/FilmCategory
        [HttpPost]
        public async Task<ActionResult<FilmCategory>> PostFilmCategory(FilmCategory filmCategory)
        {
            _context.FilmCategory.Add(filmCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FilmCategoryExists(filmCategory.FilmId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFilmCategory", new { id = filmCategory.FilmId }, filmCategory);
        }

        // DELETE: api/FilmCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmCategory>> DeleteFilmCategory(short id)
        {
            var filmCategory = await _context.FilmCategory.FindAsync(id);
            if (filmCategory == null)
            {
                return NotFound();
            }

            _context.FilmCategory.Remove(filmCategory);
            await _context.SaveChangesAsync();

            return filmCategory;
        }

        private bool FilmCategoryExists(short id)
        {
            return _context.FilmCategory.Any(e => e.FilmId == id);
        }
    }
}
