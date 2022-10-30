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
using FluentValidation;
using FluentValidation.Results;

namespace MeetupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupModelsController : ControllerBase
    {
        private readonly IMeetupRepository _repository;
        private readonly IValidator<MeetupDTO> _validator;
        private readonly IMapper _mapper;

        public MeetupModelsController(IMapper mapper, IMeetupRepository repository, IValidator<MeetupDTO> validator)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._validator = validator;
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMeetupModel(int id, MeetupDTO meetupDTO)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(meetupDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult);

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

            if (!await this._repository.Update(meetupModel))
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/MeetupModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MeetupDTO>> PostMeetupModel(MeetupDTO meetup)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(meetup);
            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var meetupModel = this._mapper.Map<MeetupModel>(meetup);

            await this._repository.Add(meetupModel);

            return CreatedAtAction("GetMeetupModel", new { id = meetupModel.Id }, meetup);
        }

        // DELETE: api/MeetupModels/5
        [HttpDelete("{id}")]
        [Authorize]
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
