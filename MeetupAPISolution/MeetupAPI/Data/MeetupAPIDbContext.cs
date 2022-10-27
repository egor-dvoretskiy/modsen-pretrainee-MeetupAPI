using MeetupAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Data
{
    public class MeetupAPIDbContext : DbContext
    {
        public MeetupAPIDbContext(DbContextOptions<MeetupAPIDbContext> options)
            : base(options)
        {

        }

        public DbSet<MeetupModel> MeetupModels { get; set; }
    }
}
