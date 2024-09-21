

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;


namespace Seat_Reservation.Models

{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
       
        public int SeatNumber { get; set; }
        [ForeignKey("SeatNumber")]
        public Seat ? Seat { get; set; }
        public int User_Id { get; set; }
        public string EmployeeName { get; set; }

        
        public string  ReservationDate { get; set; }
        
}

    }

