using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.Sports;

namespace WebAPI.Controllers.Sports
{
    [EnableQuery()]
    [Route("api/Clubs")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly ClubsContext _context;

        public ClubsController(ClubsContext context)
        {
            _context = context;
        }

        // GET: api/Clubs
        [HttpGet]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<Club>>> GetClubs()
        {
            return await _context.Clubs.Include(c=>c.Terrains).Include(c=>c.ClubAdmin).ToListAsync();
        }

        // GET: api/Clubs/GetClubsByClubAdmin
        [HttpGet("[action]/{id}")]
        [Authorize(Roles = "ClubAdmin, SuperAdmin,Client")]
        public IEnumerable<Club> GetClubsByClubAdmin(Guid id)
        {
            return _context.Clubs.Include(c => c.Terrains).Where(c => c.ClubAdminId == id);
        }


        // GET: api/algolia
        [HttpGet]
        [Route("[action]")]
        public List<ClubAlgolia> Algolia()
        {
            // Add the data to Algolia
            SearchClient client = new SearchClient("ZJ5YQA6729", "ef857c06f1ebf56ed75841fc6c2df18b");
            SearchIndex index = client.InitIndex("ClubFoot");
            List<ClubAlgolia> clubs = new List<ClubAlgolia>();
            foreach (Club club in _context.Clubs.ToList())
            {
                var clubNew = new ClubAlgolia()
                {
                    ObjectID = club.IdClub.ToString(),
                    Name = club.Name,
                    Address = club.Address,
                    Phone = club.Phone,
                    Email = club.Email,
                    OpeningTime = club.OpeningTime,
                    ClosingTime = club.ClosingTime
                };
                clubs.Add(clubNew);
            }
            index.ClearObjects();
            // Fetch from DB or a Json file
            index.SaveObjects(clubs);
            return clubs;
        }

        // GET: api/Clubs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(Guid id)
        {
            var club = await _context.Clubs.Include(c => c.Terrains).SingleOrDefaultAsync(c=>c.IdClub==id);

            if (club == null)
            {
                return NotFound();
            }

            return club;
        }



        //Put : /api/Club/ToggleStatus
        [HttpPut("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var club = await _context.Clubs.SingleOrDefaultAsync(c => c.IdClub == id);
            if (club == null)
            {
                return BadRequest();
            }

            try
            {
                club.IsActive = !club.IsActive;
                await _context.SaveChangesAsync();
                return Ok(club);

            }
            catch
            {
                return NoContent();
            }
        }

        // PUT: api/Clubs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(Guid id, Club club)
        {
            if (id != club.IdClub)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
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


        // POST: api/Clubs
        [HttpPost]
        public async Task<ActionResult<Club>> PostClub(Club club)
        {
            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.IdClub }, club);
        }

        // DELETE: api/Clubs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Club>> DeleteClub(Guid id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();

            return club;
        }

        private bool ClubExists(Guid id)
        {
            return _context.Clubs.Any(e => e.IdClub == id);
        }
    }
}
