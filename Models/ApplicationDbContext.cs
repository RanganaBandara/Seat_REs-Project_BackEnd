
using Microsoft.EntityFrameworkCore;
namespace Seat_Reservation.Models;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {


    }
    public DbSet<User> Users { get; set; }
    public DbSet<LoginDto> LoginDtos { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<PasswordChangeDto> PasswordChangeDtos { get; set; }
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
    .HasKey(b => b.User_Id);

    modelBuilder.Entity<User>()
    .Property(b => b.User_Id)
     .ValueGeneratedNever();

     
    modelBuilder.Entity<LoginDto>()
    .HasKey(b => b.User_Id);

     modelBuilder.Entity<LoginDto>()
    .Property(b => b.User_Id)
    .ValueGeneratedNever();    

}

}