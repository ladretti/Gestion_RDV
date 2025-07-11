﻿using System;
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
    public class OfficesController : ControllerBase
    {
        private readonly IDataRepository<Office> dataRepository;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IDataRepository<Address> dataRepositoryAddress;
        private readonly IDataRepository<RendezVous> dataRepositoryRendezVous;
        private readonly IDataRepository<Review> dataRepositoryReview;
        private readonly IDataRepository<Subscription> dataRepositorySub;
        private readonly IDataRepository<SocialMediaAccount> dataRepositorySocial;
        private readonly IMapper _mapper;



        public OfficesController(IDataRepository<Office> dataRepo, IDataRepository<User> dataRepoUser, IMapper mapper, IDataRepository<Address> dataRepoAddress, IDataRepository<RendezVous> dataRepoRendezVous, IDataRepository<Review> dataRepoReview, IDataRepository<Subscription> dataRepoSub, IDataRepository<SocialMediaAccount> dataRepoSocial)
        {
            dataRepository = dataRepo;
            dataRepositoryUser = dataRepoUser;
            _mapper = mapper;
            dataRepositoryAddress = dataRepoAddress;
            dataRepositoryRendezVous = dataRepoRendezVous;
            dataRepositoryReview = dataRepoReview;
            dataRepositorySub = dataRepoSub;
            dataRepositorySocial = dataRepoSocial;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Office>>> GetOfficies()
        {
            var officies = await dataRepository.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();
            await dataRepositoryAddress.GetAllAsync();
            await dataRepositoryRendezVous.GetAllAsync();
            await dataRepositoryReview.GetAllAsync();

            if (officies == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<OfficeDTO>>(officies.Value));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Office>> GetOfficeById(int id)
        {
            var office = await dataRepository.GetByIdAsync(id);
            await dataRepositoryUser.GetByIdAsync(office.Value.UserId);
            await dataRepositoryAddress.GetByIdAsync(office.Value.AdresseId);
            await dataRepositoryRendezVous.GetAllAsync();
            await dataRepositoryReview.GetAllAsync();
            await dataRepositorySub.GetAllAsync();
            await dataRepositorySocial.GetAllAsync();


            if (office == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OfficeDetailDTO>(office.Value)); ;
        }
        [HttpGet("DataOffice/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OfficeStatsDTO>> GetDataOfficeById(int id)
        {
            var office = await dataRepository.GetByIdAsync(id);
            if (office == null)
            {
                return NotFound();
            }

            var user = await dataRepositoryUser.GetByIdAsync(office.Value.UserId);
            var address = await dataRepositoryAddress.GetByIdAsync(office.Value.AdresseId);
            var rendezVous = await dataRepositoryRendezVous.GetAllAsync();
            var reviews = await dataRepositoryReview.GetAllAsync();
            var subscriptions = await dataRepositorySub.GetAllAsync();
            var socials = await dataRepositorySocial.GetAllAsync();

            // Map office to OfficeStatsDTO
            var officeStatsDto = _mapper.Map<OfficeStatsDTO>(office.Value);

            // Additional calculations (if needed)
/*            officeStatsDto.NbRdvPasse = rendezVous.Count(r => r.OfficeId == id && r.StartDate < DateTime.Now && r.EtatId == 1);
            officeStatsDto.NbRdvAVenir = rendezVous.Count(r => r.OfficeId == id && r.StartDate >= DateTime.Now && r.EtatId == 1);
            officeStatsDto.NbRdvAnnule = rendezVous.Count(r => r.OfficeId == id && r.EtatId == 3);
            officeStatsDto.TotalReviews = reviews.Count(r => r.RendezVous.OfficeId == id);
            officeStatsDto.AverageReviewNote = reviews.Where(r => r.RendezVous.OfficeId == id).Average(r => r.Note);
            officeStatsDto.NbFollowers = socials.Count(s => s.OfficeId == id && s.Type == "Follower");
            officeStatsDto.NbPosts = socials.Count(s => s.OfficeId == id && s.Type == "Post");*/

            return Ok(officeStatsDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Post>> PostOffice(Office office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(office);

            return CreatedAtAction("GetOfficeById", new { id = office.OfficeId }, office);
        }

        [HttpPut("officeId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutOffice(int officeId, OfficePutDTO office)
        {
            if (officeId != office.OfficeId)
            {
                return BadRequest();
            }

            var officeToUpdate = await dataRepository.GetByIdAsync(officeId);
            if (officeToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(officeToUpdate.Value, _mapper.Map<Office>(office));
                return NoContent();
            }
        }
    }
}