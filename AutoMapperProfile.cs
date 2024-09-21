using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Seat_Reservation.Models;
namespace AutoMapperProfile;

public class AutoMapperProfile:Profile{


        public AutoMapperProfile()
        {
            CreateMap<User,LoginDto>();
            CreateMap<User,PasswordChangeDto>();
        }

}