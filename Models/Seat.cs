using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Seat_Reservation.Models
{
    public class Seat
    {

        [Key]
        public int SeatId { get; set; }
        public int SeatNumber { get; set; }
  
       
        
    }
}
