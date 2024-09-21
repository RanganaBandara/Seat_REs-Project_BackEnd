using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seat_Reservation.Models;

public class LoginDto{

[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int User_Id { get; set; }
    public string Password{get;set;}=string.Empty;
}