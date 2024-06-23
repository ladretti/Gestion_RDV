using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using AutoMapper;
using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.Repository;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeEquipmentsController : ControllerBase
    {
        private readonly IDataRepository<OfficeEquipment> dataRepository;
        private readonly IDataRepository<Equipment> dataRepositoryEquipment;
        private readonly IMapper _mapper;

        public OfficeEquipmentsController(IDataRepository<OfficeEquipment> dataRepo, IMapper mapper, IDataRepository<Equipment> dataRepoEquipment)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
            dataRepositoryEquipment = dataRepoEquipment;
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<OfficeEquipmentDTO>>> GetEquipmentsById(int officeId)
        {
            var rdv = await dataRepository.GetAllBySpecialIdAsync(officeId);
            await dataRepositoryEquipment.GetAllAsync();
            if (rdv == null)
                return NotFound();
            if (rdv.Value == null)
                return NotFound();

            return Ok(_mapper.Map< IEnumerable<OfficeEquipmentDTO>>(rdv.Value));
        }


        [HttpGet("{equipmentId}/{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OfficeEquipmentDTO>> GetOfficeEquipmentById(int equipmentId, int officeId)
        {
            var like = await dataRepository.GetByIdsAsync(equipmentId, officeId);

            if (like.Value == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OfficeEquipmentDTO>(like.Value));
        }


        [HttpPut("{equipmentId}/{officeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutOfficeEquipment(int equipmentId, int officeId, OfficeEquipmentPostDTO officeEquipment)
        {
            if (equipmentId != officeEquipment.EquipmentId || officeId != officeEquipment.OfficeId)
            {
                return BadRequest();
            }
            var officeEquipmentToUpdate = await dataRepository.GetByIdsAsync(equipmentId, officeId);
            if (officeEquipmentToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(officeEquipmentToUpdate.Value, _mapper.Map<OfficeEquipment>(officeEquipment));
                return NoContent();
            }
        }

        [HttpDelete("{equipmentId}/{officeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOfficeEquipment(int equipmentId, int officeId)
        {
            var officeEquipment = await dataRepository.GetByIdsAsync(equipmentId, officeId);
            if (officeEquipment == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(officeEquipment.Value);

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OfficeEquipmentPostDTO>> PostOfficeEquipment(OfficeEquipmentPostDTO officeEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(_mapper.Map<OfficeEquipment>(officeEquipment));

            return CreatedAtAction(nameof(GetOfficeEquipmentById), new { equipmentId = officeEquipment.EquipmentId, officeId = officeEquipment.OfficeId }, officeEquipment);
        }
    }
}
