using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seat_Reservation.Models;

public class User{

[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int User_Id { get; set; }

   public required string Email{ get; set; }
    
    public required string Password { get; set; }

    public required string Phone_Number { get; set; }

    public required string Name{get;set;}

    public  DateTime Created_Time{get;set;}=DateTime.Now;





}