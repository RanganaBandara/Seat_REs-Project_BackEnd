using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Seat_Reservation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer("name=ConnectionStrings:defaultConnection"));








builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });


builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(Options=>{
Options.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
    ValidateIssuer=true,
    ValidateAudience=true,
    ValidateLifetime=true,
    ValidateIssuerSigningKey=true,
    ValidIssuer=builder.Configuration["Jwt:Issuer"],
    ValidAudience=builder.Configuration["Jwt:Audience"],
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))




};
});




builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseAuthentication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

        app.UseAuthorization();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
