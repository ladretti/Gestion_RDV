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
using System.Runtime.Intrinsics.Arm;
using Microsoft.Extensions.Hosting;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendezVousController : ControllerBase
    {
        private readonly IDataRepositoryRendezVous<RendezVous> dataRepository;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IDataRepository<Office> dataRepositoryOffice;
        private readonly IMapper _mapper;

        public RendezVousController(IDataRepositoryRendezVous<RendezVous> dataRepo, IMapper mapper, IDataRepository<User> dataRepoUser, IDataRepository<Office> dataRepoOffice)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
            dataRepositoryUser = dataRepoUser;
            dataRepositoryOffice = dataRepoOffice;
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

        [HttpGet("{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RendezVousSpecialDTO>>> GetRendezVousByOfficeId(int officeId)
        {
            var rdv = await dataRepository.GetAllBySpecialIdAsync(officeId);
            await dataRepositoryUser.GetAllAsync();
            var filteredRdv = rdv.Value.OrderBy(post => post.StartDate).ToList();

            if (filteredRdv == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RendezVousSpecialDTO>>(filteredRdv));
        }

        [HttpGet("getRendezVousByUserId/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<RendezVousByUserIdDTO>>> GetRendezVousByUserId(int userId)
        {
            var rdv = await dataRepository.GetAllByIdsAsync(userId, null);
            await dataRepositoryOffice.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();

            if (rdv == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RendezVousByUserIdDTO>>(rdv.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RendezVousDTO>> PostRendezVous(RendezVousPostDTO rendezVous)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(_mapper.Map<RendezVous>(rendezVous));

            return CreatedAtAction(nameof(GetRendezVousById), new { userId = rendezVous.UserId, officeId = rendezVous.OfficeId }, rendezVous);

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRendezVous(int id)
        {
            var rendezVous = await dataRepository.GetByIdAsync(id);
            if (rendezVous.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(rendezVous.Value);

            return NoContent();
        }
    }
}
