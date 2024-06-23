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
using Microsoft.Extensions.Hosting;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IDataRepository<Address> dataRepository;
        private readonly IMapper _mapper;

        public AddressesController(IDataRepository<Address> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("{adresseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AddressDTO>> GetAdresseById(int adresseId)
        {
            var rdv = await dataRepository.GetByIdAsync(adresseId);
            if (rdv == null)
                return NotFound();
            if (rdv.Value == null)
                return NotFound();

            return Ok(_mapper.Map<AddressDTO>(rdv.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddressDTO>> PostAdresse(AddressPostDTO adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address adress = _mapper.Map<Address>(adresse);
            await dataRepository.AddAsync(_mapper.Map<Address>(adress));

            return CreatedAtAction(nameof(GetAdresseById), new { adresseId = adress.AdresseId }, _mapper.Map<AddressDTO>(adress));

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await dataRepository.GetByIdAsync(id);
            if (adresse.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(adresse.Value);

            return NoContent();
        }
        [HttpPut("{adresseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAddress(int adresseId, AddressDTO adresse)
        {
            if (adresseId != adresse.AdresseId)
            {
                return BadRequest();
            }

            var adresseToUpdate = await dataRepository.GetByIdAsync(adresseId);
            if (adresseToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(adresseToUpdate.Value, _mapper.Map<Address>(adresse));
                return NoContent();
            }
        }
    }
}
