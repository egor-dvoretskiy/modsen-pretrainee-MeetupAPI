using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Models
{
    public class SpeakerModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int MeetupModelId { get; set; }

        public MeetupModel MeetupModel { get; set; }
    }
}
