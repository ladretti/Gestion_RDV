using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using AutoMapper;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DTO;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendezVousController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataRepository<RendezVous> dataRepository;
        private readonly IMapper _mapper;

        public RendezVousController(IConfiguration config, IDataRepository<RendezVous> dataRepo, IMapper mapper)
        {
            _config = config;
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("{userId}/{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RendezVousDTO>> GetRendezVousById(int userId, int officeId)
        {
            var rdv = await dataRepository.GetByIdsAsync(userId, officeId);

            if (rdv == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RendezVousDTO>(rdv.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RendezVousDTO>> PostRendezVous(RendezVous rendezVous)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(rendezVous);

            return CreatedAtAction(nameof(GetRendezVousById), new { userId = rendezVous.UserId, officeId = rendezVous.OfficeId }, rendezVous);
        }

    }
}
