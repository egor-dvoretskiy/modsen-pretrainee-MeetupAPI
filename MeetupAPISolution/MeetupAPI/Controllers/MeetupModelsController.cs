using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetupAPI.Data;
using MeetupAPI.Models;
using AutoMapper;
using MeetupAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MeetupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeetupModelsController : ControllerBase
    {
        private readonly int _maxBudgetRandomValue = 99999;

        private readonly MeetupAPIDbContext _context;
        private readonly IMapper _mapper;
        private readonly Random _random;

        public MeetupModelsController(MeetupAPIDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

            this._random = new Random();
        }

        // GET: api/MeetupModels
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<MeetupDTO>> GetMeetupModels()
        {
            var meetupModels = await _context.MeetupModels
                .Include(x => x.Speakers)
                .ToListAsync();

            IEnumerable<MeetupDTO> meetupDTOs = meetupModels.Select(x => this._mapper.Map<MeetupDTO>(x));

            return meetupDTOs;
        }

        // GET: api/MeetupModels/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MeetupDTO>> GetMeetupModel(int id)
        {
            if (!this.MeetupModelExists(id))
            {
                return BadRequest();
            }

            var meetupModel = await _context.MeetupModels
                .Where(x => x.Id == id)
                .Include(x => x.Speakers)
                .SingleOrDefaultAsync();

            if (meetupModel == null)
            {
                return NotFound();
            }

            var meetupDTO = this._mapper.Map<MeetupDTO>(meetupModel);
            if (meetupDTO == null)
            {
                return NotFound();
            }

            return meetupDTO;
        }

        // PUT: api/MeetupModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMeetupModel(int id, MeetupDTO meetupDTO)
        {
            if (!this.MeetupModelExists(id))
            {
                return BadRequest();
            }

            //temporary decision -> problem with replacing nested list.
            var meetupModelDB = await _context.MeetupModels
                .Where(x => x.Id == id)
                .Include(x => x.Speakers)
                .SingleOrDefaultAsync();
            if (meetupModelDB == null)
            {
                return NotFound();
            }
            _context.MeetupModels.Remove(meetupModelDB);
            //

            var meetupModel = this._mapper.Map<MeetupModel>(meetupDTO);
            if (meetupModel == null)
            {
                return NotFound();
            }

            meetupModel.Id = id;
            meetupModel.Budget = this._random.Next(0, this._maxBudgetRandomValue);

            /*_context.Entry(meetupModelDB).CurrentValues.SetValues(meetupModel);  */
            _context.MeetupModels.Add(meetupModel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.MeetupModelExists(id))
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MeetupDTO>> PostMeetupModel(MeetupDTO meetup)
        {
            var meetupModel = this._mapper.Map<MeetupModel>(meetup);
            meetupModel.Budget = this._random.Next(0, this._maxBudgetRandomValue);

            _context.MeetupModels.Add(meetupModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeetupModel", new { id = meetupModel.Id }, meetup);
        }

        // DELETE: api/MeetupModels/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMeetupModel(int id)
        {
            var meetupModel = await _context.MeetupModels
                .Where(x => x.Id == id)
                .Include(x => x.Speakers)
                .SingleOrDefaultAsync();
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
