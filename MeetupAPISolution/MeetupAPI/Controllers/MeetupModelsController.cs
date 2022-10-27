using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetupAPI.Data;
using MeetupAPI.Models;

namespace MeetupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupModelsController : ControllerBase
    {
        private readonly MeetupAPIDbContext _context;

        public MeetupModelsController(MeetupAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/MeetupModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetupModel>>> GetMeetupModels()
        {
            return await _context.MeetupModels.ToListAsync();
        }

        // GET: api/MeetupModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetupModel>> GetMeetupModel(int id)
        {
            var meetupModel = await _context.MeetupModels.FindAsync(id);

            if (meetupModel == null)
            {
                return NotFound();
            }

            return meetupModel;
        }

        // PUT: api/MeetupModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetupModel(int id, MeetupModel meetupModel)
        {
            if (id != meetupModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(meetupModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetupModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MeetupModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeetupModel>> PostMeetupModel(MeetupModel meetupModel)
        {
            _context.MeetupModels.Add(meetupModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeetupModel", new { id = meetupModel.Id }, meetupModel);
        }

        // DELETE: api/MeetupModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetupModel(int id)
        {
            var meetupModel = await _context.MeetupModels.FindAsync(id);
            if (meetupModel == null)
            {
                return NotFound();
            }

            _context.MeetupModels.Remove(meetupModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeetupModelExists(int id)
        {
            return _context.MeetupModels.Any(e => e.Id == id);
        }
    }
}
