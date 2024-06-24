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
    public class EquipmentsController : ControllerBase
    {
        private readonly IDataRepository<Equipment> dataRepository;
        private readonly IMapper _mapper;

        public EquipmentsController(IDataRepository<Equipment> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("{equipmentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<EquipmentDTO>> GetAdresseById(int equipmentId)
        {
            var rdv = await dataRepository.GetByIdAsync(equipmentId);
            if (rdv == null)
                return NotFound();
            if (rdv.Value == null)
                return NotFound();

            return Ok(_mapper.Map<EquipmentDTO>(rdv.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EquipmentDTO>> PostAdresse(EquipmentPostDTO equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Equipment equipment1 = _mapper.Map<Equipment>(equipment);
            await dataRepository.AddAsync(_mapper.Map<Equipment>(equipment1));

            return CreatedAtAction(nameof(GetAdresseById), new { equipmentId = equipment1.EquipmentId }, _mapper.Map<EquipmentDTO>(equipment1));

        }
    }
}
