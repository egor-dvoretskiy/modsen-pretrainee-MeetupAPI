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
using MeetupAPI.Data.Repositories.Interfaces;

namespace MeetupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeetupModelsController : ControllerBase
    {
        private readonly int _maxBudgetRandomValue = 99999;

        private readonly IMeetupRepository _repository;
        private readonly IMapper _mapper;
        private readonly Random _random;

        public MeetupModelsController(IMapper mapper, IMeetupRepository repository)
        {
            this._repository = repository;
            this._mapper = mapper;

            this._random = new Random();
        }

        // GET: api/MeetupModels
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<MeetupDTO>> GetMeetupModels()
        {
            var meetupModels = await this._repository.GetAll();

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
            if (!await this._repository.IsExist(id))
            {
                return NotFound();
            }

            var meetupModel = await this._repository.GetById(id);
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
            if (!await this._repository.IsExist(id))
            {
                return NotFound();
            }

            var meetupModel = this._mapper.Map<MeetupModel>(meetupDTO);
            if (meetupModel == null)
            {
                return NotFound();
            }

            meetupModel.Id = id;
            meetupModel.Budget = this._random.Next(0, this._maxBudgetRandomValue);

            if (!await this._repository.Update(meetupModel))
            {
                return BadRequest();
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

            await this._repository.Add(meetupModel);

            return CreatedAtAction("GetMeetupModel", new { id = meetupModel.Id }, meetup);
        }

        // DELETE: api/MeetupModels/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMeetupModel(int id)
        {
            bool isDeleteSuccessful = await this._repository.DeleteById(id);

            if (!isDeleteSuccessful)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
