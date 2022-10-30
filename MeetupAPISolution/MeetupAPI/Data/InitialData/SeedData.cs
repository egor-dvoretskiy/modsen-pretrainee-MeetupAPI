using MeetupAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Data.InitialData
{
    public class SeedData
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = new MeetupAPIDbContext(serviceProvider.GetRequiredService<DbContextOptions<MeetupAPIDbContext>>());

            if (context == null || context.MeetupModels == null)
            {
                //throw new ArgumentNullException($"{nameof(context)} is null at SeedData.");

                return;
            }

            if (context.MeetupModels.Any())
            {
                return;
            }

            context.MeetupModels.Add(GetMeetupOne());
            context.MeetupModels.Add(GetMeetupTwo());
            context.MeetupModels.Add(GetMeetupThree());
            context.MeetupModels.Add(GetMeetupFour());

            context.SaveChanges();

        }

        private static MeetupModel GetMeetupOne()
        {
            return new MeetupModel
            {
                Budget = 66783.24m,
                Description = "The most fascinating meeting with well-known celebrities.",
                EventDateTime = DateTime.Now.AddDays(16),
                EventLocation = "Drova",
                Plan = "Get satisfied.",
                Topic = "Celebs",
                Sponsor = "Greta Tunberg",
                Speakers = new List<SpeakerModel>()
                { 
                    new SpeakerModel()
                    {
                        FullName = "Doctor Livsey"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Luffy"
                    },
                    new SpeakerModel()
                    {
                        FullName = "John Travolta"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Biden"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Aurora"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Linkin Bark"
                    },
                }
            };
        }

        private static MeetupModel GetMeetupTwo()
        {
            return new MeetupModel
            {
                Budget = 8347723.24m,
                Description = "Just a meeting among homeless",
                EventDateTime = DateTime.Now.AddDays(16),
                EventLocation = "Republic Palace",
                Plan = "Get acknowledged.",
                Topic = "Homelesses",
                Sponsor = "Macron",
                Speakers = new List<SpeakerModel>()
                { 
                    new SpeakerModel()
                    {
                        FullName = "Siphon"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Boroda"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Danila"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Michalich"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Nashalnika"
                    },
                }
            };
        }

        private static MeetupModel GetMeetupThree()
        {
            return new MeetupModel
            {
                Budget = 9963.24m,
                Description = "Meetup with famous tik tokers.",
                EventDateTime = DateTime.Now.AddDays(16),
                EventLocation = "Dana mall",
                Plan = "Break an elevator.",
                Topic = "Tik Tok",
                Sponsor = "MVD",
                Speakers = new List<SpeakerModel>()
                { 
                    new SpeakerModel()
                    {
                        FullName = "Milohin"
                    },
                    new SpeakerModel()
                    {
                        FullName = "Drugih Ne Znau"
                    },
                }
            };
        }

        private static MeetupModel GetMeetupFour()
        {
            return new MeetupModel
            {
                Budget = 13732.55m,
                Description = "There is no need to speak about it.",
                EventDateTime = DateTime.Now.AddDays(16),
                EventLocation = "Moscow-city",
                Plan = "Get smarter.",
                Topic = "DOTNEXT",
                Sponsor = "Richter",
                Speakers = new List<SpeakerModel>()
                { 
                    new SpeakerModel()
                    {
                        FullName = "Beluga"
                    },
                }
            };
        }
    }
}
