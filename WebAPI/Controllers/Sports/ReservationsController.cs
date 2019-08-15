using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers.Sports
{
    [EnableQuery()]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ClubsContext _context;

        public ReservationsController(ClubsContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.Include(r => r.Client).Include(r => r.Terrain).ToListAsync();
            //return await _context.Reservations.Include(r=>r.Client).ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        [EnableQuery()]
        public async Task<ActionResult<Reservation>> GetReservation(Guid id)
        {
            var reservation = await _context.Reservations.Include(r => r.Client).Include(r => r.Terrain).SingleOrDefaultAsync(r=>r.IdReservation==id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Guid id, Reservation reservation)
        {
            if (id != reservation.IdReservation)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.IdReservation }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        // DELETE: api/Reservations/5
        [HttpDelete]
        public async Task<ActionResult<Reservation>> DeleteReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();
            if (reservations == null)
            {
                return NotFound();
            }

            _context.Reservations.RemoveRange(reservations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.IdReservation == id);
        }
    }
}
