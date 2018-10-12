using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pindogramApp.Entities;
using Microsoft.AspNetCore.Authorization;


namespace pindogramApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemesController : ControllerBase
    {
        private readonly PindogramDataContext _context;

        public MemesController(PindogramDataContext context)
        {
            _context = context;
        }

        // GET: api/Memes
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Meme> GetMeme()
        {
            return _context.Meme;
        }

        // GET: api/Memes/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meme = await _context.Meme.FindAsync(id);

            if (meme == null)
            {
                return NotFound();
            }

            return Ok(meme);
        }

        // PUT: api/Memes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeme([FromRoute] int id, [FromBody] Meme meme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meme.Id)
            {
                return BadRequest();
            }

            _context.Entry(meme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemeExists(id))
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



        // POST: api/Memes
        [HttpPost]
        public async Task<IActionResult> PostMeme([FromBody] Meme meme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Meme.Add(meme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeme", new { id = meme.Id }, meme);
        }

        // DELETE: api/Memes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meme = await _context.Meme.FindAsync(id);
            if (meme == null)
            {
                return NotFound();
            }

            _context.Meme.Remove(meme);
            await _context.SaveChangesAsync();

            return Ok(meme);
        }

        private bool MemeExists(int id)
        {
            return _context.Meme.Any(e => e.Id == id);
        }
    }
}