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
    public class SocialMediaAccountsController : ControllerBase
    {
        private readonly IDataRepository<SocialMediaAccount> dataRepository;
        private readonly IMapper _mapper;

        public SocialMediaAccountsController(IDataRepository<SocialMediaAccount> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        [HttpGet("id")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SocialMediaAccount>> GetSocialById(int id)
        {
            var like = await dataRepository.GetByIdAsync(id);

            if (like.Value == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SocialMediaAccount>(like.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SocialMediaAccount>>PostSocial(SocialPostDTO social)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SocialMediaAccount sub = _mapper.Map<SocialMediaAccount>(social);
            await dataRepository.AddAsync(sub);

            return CreatedAtAction(nameof(GetSocialById), new { id = sub.SocialMediaAccountId }, sub);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSocialMediaAccount(int id)
        {
            var social = await dataRepository.GetByIdAsync(id);
            if (social.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(social.Value);

            return NoContent();
        }
    }
}
