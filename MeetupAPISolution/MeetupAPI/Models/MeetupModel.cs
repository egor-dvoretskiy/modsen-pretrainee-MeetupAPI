using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Models
{
    public class MeetupModel
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public string Plan { get; set; }

        public string Sponsor { get; set; }

        public List<SpeakerModel> Speakers { get; set; }

        public DateTime EventDateTime { get; set; }

        public string EventLocation { get; set; }

        public decimal Budget { get; set; }
    }
}
