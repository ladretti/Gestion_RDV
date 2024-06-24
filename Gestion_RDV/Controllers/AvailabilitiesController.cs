using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DTO;
using AutoMapper;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IDataRepository<Availability> dataRepository;
        private readonly IMapper _mapper;
        //private readonly IDataRepositoryUser<User> dataRepositoryUser;


        public AvailabilitiesController(IDataRepository<Availability> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        [HttpGet("GetByOfficeId/{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> GetAvailabilitiesByOfficeId(int officeId)
        {
            var availabilities = await dataRepository.GetAllBySpecialIdAsync(officeId);
            var filteredavailabilities = availabilities.Value.OrderBy(availabilities => availabilities.StartDate).ToList();

            if (filteredavailabilities == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AvailabilityDTO>>(filteredavailabilities));

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var availability = await dataRepository.GetByIdAsync(id);
            if (availability == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(availability.Value);

            return NoContent();
        }

        [HttpGet("GetByAvailabilityId/{availabilityId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AvailabilityDTO>> GetAvailabilityById(int availabilityId)
        {
            var rdv = await dataRepository.GetByIdAsync(availabilityId);

            if (rdv == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AvailabilityDTO>(rdv.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AvailabilityDTO>> PostAvailability(AvailabilityPostDTO availability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Availability availability1 = _mapper.Map<Availability>(availability);
            await dataRepository.AddAsync(availability1);

            return CreatedAtAction(nameof(GetAvailabilityById), new { availabilityId = availability1.AvailabilityId }, _mapper.Map<AvailabilityDTO>(availability1));
        }

        [HttpPut("{availabilityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAvailability(int availabilityId, bool reserve)
        {

            var availabilityToUpdate = await dataRepository.GetByIdAsync(availabilityId);
            availabilityToUpdate.Value.Reserve = reserve;
            if (availabilityToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(availabilityToUpdate.Value, availabilityToUpdate.Value);
                return NoContent();
            }
        }
    }
}
