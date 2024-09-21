using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seat_Reservation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seat_Reservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Seats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetSeats()
        {
            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Seats/5
        [HttpGet("Filtering/{dt}")]
        public async Task<ActionResult<Reservation>> GetSeat(string  dt)
        {
            var seat = await _context.Reservations.FirstOrDefaultAsync(s => s.ReservationDate== dt);

            if (seat == null)
            {
                return NotFound();
            }

            return seat;
        }
      

        // POST: api/Seats/Reserve
        [HttpPost()]
        [Route("Reserve")]
        public async Task<ActionResult<Seat>> ReserveSeat([FromBody] Reservation reservation)
        {
            var seat = await _context.Seats.FindAsync(reservation.SeatNumber);
        

            if (seat == null)            {
                return BadRequest("Seat is not available.");
            }

           
           

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Seats/CancelReservation/5
        [HttpDelete("CancelReservation/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var reservation = await _context.Reservations.Include(r => r.Seat).FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return NotFound();
            }

           
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
