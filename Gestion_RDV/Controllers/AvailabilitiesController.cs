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

        [HttpGet("{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> GetAvailabilitiesByOfficeId(int officeId)
        {
            var availabilities = await dataRepository.GetAllBySpecialIdAsync(officeId);

            if (availabilities == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AvailabilityDTO>>(availabilities.Value));

        }

    }
}
