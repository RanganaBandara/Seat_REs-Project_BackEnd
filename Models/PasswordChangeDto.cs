using System.ComponentModel.DataAnnotations;

namespace Seat_Reservation.Models;

public class PasswordChangeDto(){

    

    [Key]
    public int Id{get;set;}
    
    public required string Email{ get; set; }
    public string PasswordResetToken{get;set;}

    public DateTime ResetTokenExpires{get;set;}
    

}