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
        [Authorize(Roles = "SuperAdmin,Client,ClubAdmin")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.Include(r => r.Client).Include(r => r.Terrain).ToListAsync();
        }

        // GET: api/Reservations/GetReservationsByClubAdmin/5
        [HttpGet("[action]/{id}")]
        [Authorize(Roles = "ClubAdmin, SuperAdmin,Client")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationsByClubAdmin(Guid id)
        {
            return await _context.Reservations.Include(r => r.Client).Include(r => r.Terrain).ThenInclude(t => t.club).Where(t => t.Terrain.club.ClubAdminId == id).ToListAsync();
        }


        // GET: api/Reservations/terrains/5
        [HttpGet("terrains/{id}")]
        [EnableQuery()]
        public async Task<ActionResult> GetReservationByTerrain(Guid id)
        {
            var terrain = await _context.Terrains.Include(t=>t.Reservations).SingleOrDefaultAsync(r => r.IdTerrain == id);


            if (terrain == null)
            {
                return NotFound();
            }

            return Ok(terrain.Reservations);
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
            var resu = await _context.Reservations.FindAsync(id);
            if (resu == null)
            {
                return BadRequest();
            }

            DateTime NewreservationStart = Convert.ToDateTime(reservation.StartReservation);
            DateTime NewreservationEnd = Convert.ToDateTime(reservation.EndReservation);


            if (resu.StartReservation!= reservation.StartReservation || resu.EndReservation != reservation.EndReservation)
            {
               
                    var terrain = await _context.Terrains.Include(t => t.Reservations).SingleOrDefaultAsync(r => r.IdTerrain == resu.IdTerrain);

                    if (terrain == null)
                    {
                        return BadRequest(new { message = "Stadium not found" });
                    }
                    //var club = await _context.Terrains.SingleOrDefaultAsync(t => t.IdClub == terrain.IdClub);

                    if (DateTime.Compare(NewreservationStart, DateTime.Now) < 0)
                    {
                        return BadRequest(new { message = "This Date has been passed" });
                    }

                    if (DateTime.Compare(NewreservationStart, NewreservationEnd) == 0 || DateTime.Compare(NewreservationStart, NewreservationEnd) == 1)
                    {
                        return BadRequest(new { message = "The given Date is invalid" });
                    }
                if (DateTime.Compare(NewreservationStart, Convert.ToDateTime(resu.StartReservation)) == -1 || DateTime.Compare(NewreservationStart, Convert.ToDateTime(resu.EndReservation)) == 1)
                {
                    var reservations = terrain.Reservations;
                    foreach (var res in reservations)
                    {
                
                            if (DateTime.Compare(NewreservationStart, Convert.ToDateTime(res.StartReservation)) == 0)
                            {
                                return BadRequest(new { message = "This ReservationDate is invalid" });

                            }
                            else
                            {
                                if (DateTime.Compare(NewreservationStart, Convert.ToDateTime(res.EndReservation)) == -1 && DateTime.Compare(NewreservationStart, Convert.ToDateTime(res.StartReservation)) == 1)
                                {
                                    return BadRequest(new { message = "This Reservation StartDate is invalid" });
                                }
                            }
                        
                    }

                }
                if (DateTime.Compare(NewreservationEnd, Convert.ToDateTime(resu.EndReservation)) == 1 || DateTime.Compare(NewreservationEnd, Convert.ToDateTime(resu.StartReservation)) == -1)
                {
                    var reservations = terrain.Reservations;
                    foreach (var res in reservations)
                    {

                        if (DateTime.Compare(NewreservationEnd, Convert.ToDateTime(res.EndReservation)) == 0)
                        {
                            return BadRequest(new { message = "This ReservationDate is invalid" });

                        }
                        else
                        {
                            if (DateTime.Compare(NewreservationEnd, Convert.ToDateTime(res.EndReservation)) == -1 && DateTime.Compare(NewreservationEnd, Convert.ToDateTime(res.StartReservation)) == 1)
                            {
                                return BadRequest(new { message = "This Reservation EndDate is invalid" });
                            }

                        }

                    }

                }
            }

            var terr = await _context.Terrains.Include(t => t.Reservations).SingleOrDefaultAsync(r => r.IdTerrain == resu.IdTerrain);
            resu.StartReservation = reservation.StartReservation;
            resu.EndReservation = reservation.EndReservation;
            resu.Duration = ((NewreservationEnd - NewreservationStart).TotalHours).ToString();
            resu.Price = Double.Parse(resu.Duration) * terr.Price;
            resu.status = reservation.status;

            _context.Entry(resu).State = EntityState.Modified;

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

            DateTime NewreservationStart= Convert.ToDateTime(reservation.StartReservation);
            DateTime NewreservationEnd= Convert.ToDateTime(reservation.EndReservation);
            
            var terrain = await _context.Terrains.Include(t=>t.Reservations).SingleOrDefaultAsync(r => r.IdTerrain == reservation.IdTerrain);
            if (terrain == null)
            {
                return BadRequest(new { message = "Stadium not found" });
            }
            //var club = await _context.Terrains.SingleOrDefaultAsync(t => t.IdClub == terrain.IdClub);
          
            if (DateTime.Compare(NewreservationStart, DateTime.Now) < 0 )
            {
                return BadRequest(new { message = "This Date has been passed" });
            }
            if (DateTime.Compare(NewreservationStart, NewreservationEnd) == 0 || DateTime.Compare(NewreservationStart, NewreservationEnd)==1)
            {
                return BadRequest(new { message = "The given Date is invalid" });
            }

            var reservations = terrain.Reservations;
            foreach (var res in reservations)
            {
                if (DateTime.Compare(NewreservationStart, Convert.ToDateTime(res.StartReservation)) == 0 || DateTime.Compare(NewreservationEnd, Convert.ToDateTime(res.EndReservation)) == 0)
                {
                    return BadRequest(new { message = "This ReservationDate is invalid" });

                }
                else
                {
                     if (DateTime.Compare(NewreservationStart, Convert.ToDateTime(res.EndReservation)) == -1 && DateTime.Compare(NewreservationStart, Convert.ToDateTime(res.StartReservation)) == 1)
                        {
                        return BadRequest(new { message = "This Reservation StartDate is invalid" });
                        }
                    if (DateTime.Compare(NewreservationEnd, Convert.ToDateTime(res.EndReservation)) == -1 && DateTime.Compare(NewreservationEnd, Convert.ToDateTime(res.StartReservation)) == 1)
                    {
                        return BadRequest(new { message = "This Reservation EndDate is invalid" });
                    }

                }
            }
            try
            {


                if (reservation.Duration == null)
                {
                    var Duration = (NewreservationEnd - NewreservationStart).TotalHours;
                    reservation.Price = Duration * terrain.Price;
                    reservation.Duration = Duration.ToString();
                }
                else
                {
                    double Duration = Double.Parse(reservation.Duration);
                    reservation.Duration= Duration.ToString();
                    NewreservationEnd = NewreservationStart.AddHours(Duration);
                    reservation.Price = Duration * terrain.Price;
                }
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetReservation", new { id = reservation.IdReservation }, reservation);

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
