using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers.Sports
{
    [EnableQuery()]
    [Route("api/Terrains")]
    [ApiController]
    public class TerrainsController : ControllerBase
    {
        private readonly ClubsContext _context;

        public TerrainsController(ClubsContext context)
        {
            _context = context;
        }


        // GET: api/Terrains
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Client,ClubAdmin")]
        public async Task<ActionResult<IEnumerable<Terrain>>> GetTerrains()
        {
            return await _context.Terrains.Include(t => t.Reservations).Include(t => t.club).ToListAsync();
        }

        // GET: api/Terrains
        [HttpGet("[action]/{id}")]
        [Authorize(Roles = "ClubAdmin, SuperAdmin,Client")]
        public async Task<ActionResult<IEnumerable<Terrain>>> GetTerrainsByClubAdmin(Guid id)
        {
            return await _context.Terrains.Include(c => c.club).Where(c => c.club.ClubAdminId == id).ToListAsync();
        }

        // GET: api/Terrains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Terrain>> GetTerrain(Guid id)
        {
            //var terrain = await _context.Terrains.Include(t => t.Images).SingleOrDefaultAsync(t => t.IdTerrain == id);
            var terrain = await _context.Terrains.SingleOrDefaultAsync(t => t.IdTerrain == id);

            if (terrain == null)
            {
                return NotFound();
            }

            return terrain;
        }

        // PUT: api/Terrains/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerrain(Guid id, Terrain terrain)
        {
            if (id != terrain.IdTerrain)
            {
                return BadRequest();
            }

            _context.Entry(terrain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerrainExists(id))
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

        // POST: api/Terrains
        [HttpPost]
        public async Task<ActionResult<Terrain>> PostTerrain(Terrain terrain)
        {
            _context.Terrains.Add(terrain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTerrain", new { id = terrain.IdTerrain }, terrain);
        }

        // DELETE: api/Terrains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Terrain>> DeleteTerrain(Guid id)
        {
            var terrain = await _context.Terrains.FindAsync(id);
            if (terrain == null)
            {
                return NotFound();
            }

            _context.Terrains.Remove(terrain);
            await _context.SaveChangesAsync();

            return terrain;
        }

        private bool TerrainExists(Guid id)
        {
            return _context.Terrains.Any(e => e.IdTerrain == id);
        }
    }
}
