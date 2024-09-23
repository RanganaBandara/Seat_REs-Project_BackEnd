using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seat_Reservation.Models;
using System.Configuration;
using System.Security.Claims;
using System.IO;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Seat_Reservation.Controllers;




[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{

    private readonly ApplicationDbContext _context;     //db context

        private readonly IConfiguration configuration;      //IConfiguration injecting
private readonly IMapper _mapper;       //auto mapper injecting



public UserController(ApplicationDbContext context ,IConfiguration configuration,IMapper mapper){
    _context=context;
    this.configuration=configuration;
    _mapper=mapper;
}

//Login Api
[HttpPost]
[Route("Login")]

public IActionResult Login(LoginDto loginDto){
var user=_context.Users.FirstOrDefault(x => x.User_Id==loginDto.User_Id && x.Password==loginDto.Password);
if(user!=null){
    var claims=new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        new Claim("UserId",user.User_Id.ToString()),
        new Claim("Password",user.Password.ToString())    
};

        var key=new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var signIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var Token=new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims, 
                expires : DateTime.UtcNow.AddMinutes(05),
                signingCredentials:signIn
        );

        string tokenValue=new JwtSecurityTokenHandler().WriteToken(Token);
        return Ok(new {Token =tokenValue});

}

return NoContent();


}

//Registration Api
  [HttpPost()]
  [Route("Register")]


public IActionResult RegisterUser([FromBody]User Model){

    var usr=_context.Users.Find(Model.User_Id);
    if(usr==null){
     _context.Add(Model);
     _context.SaveChanges();
      var response = new
            {
                Message = "Request was successful!",
                StatusCode = 200,
                Data = new { ExampleData = "This is some example data." }
            };
    return Ok(response);
    }
    return BadRequest("Email already inserted");
}

[HttpGet("{Email}")]

public IActionResult Password_Change([FromRoute] string Email){
    var user=_context.Users.FirstOrDefault(x => x.Email==Email);
    if(user!=null){
      
       return Ok(user);

    }

    return Ok();

    
}


[HttpGet("User_Id/{Id}")]
public async Task<ActionResult<User>> GetName(int Id)
{
    var user = await _context.Users
      .Where(x=>x.User_Id==Id)
        .Select(x=>x.Name) // Include User_Id if needed
        .FirstOrDefaultAsync();

    if (user != null)
    {
        var usr=new{
            name=user
        };
        return Ok(usr); // Return the user object
    }

    return NotFound(); // Use NotFound() if the user doesn't exist
}


[HttpGet]
public IActionResult getall(){
    var all=_context.Users.ToList();
    return Ok(all);
}
}

//forget password
/*/
[HttpPost("forgot-password")]

public IActionResult ForgetPassword(string email){
 var user=_context.Users.FirstOrDefault(u=>u.Email==email);
 if(user==null){
    return NotFound();
 }
 user. PasswordResetToken=CreateRandomToken();
 _context.SaveChanges();

 return Ok("User Verified");

}

private  string CreateRandomToken(){
    return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    */