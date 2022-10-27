using AutoMapper;
using MeetupAPI.DTOs;
using MeetupAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.AutomapperProfiles
{
    public class MeetupProfile : Profile
    {
        public MeetupProfile()
        {
            CreateMap<MeetupDTO, MeetupModel>()
                .ReverseMap();

            CreateMap<SpeakerDTO, SpeakerModel>()
                .ReverseMap();
        }
    }
}
