using AutoMapper;
using MeetupAPI.DTOs.Account;
using MeetupAPI.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserModel>()
                .ReverseMap();
        }
    }
}
