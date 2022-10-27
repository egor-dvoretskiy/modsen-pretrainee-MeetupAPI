using MeetupAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DTOs
{
    public class MeetupDTO
    {
        public string Topic { get; set; }

        public string Description { get; set; }

        public string Plan { get; set; }

        public string Sponsor { get; set; }

        public List<SpeakerDTO> Speakers { get; set; }

        public DateTime EventDateTime { get; set; }

        public string EventLocation { get; set; }
    }
}
