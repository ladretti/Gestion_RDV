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
    public class OfficesController : ControllerBase
    {
        private readonly IDataRepository<Office> dataRepository;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IDataRepository<Address> dataRepositoryAddress;
        private readonly IDataRepository<RendezVous> dataRepositoryRendezVous;
        private readonly IDataRepository<Review> dataRepositoryReview;
        private readonly IDataRepository<Subscription> dataRepositorySub;
        private readonly IMapper _mapper;



        public OfficesController(IDataRepository<Office> dataRepo, IDataRepository<User> dataRepoUser, IMapper mapper, IDataRepository<Address> dataRepoAddress, IDataRepository<RendezVous> dataRepoRendezVous, IDataRepository<Review> dataRepoReview, IDataRepository<Subscription> dataRepoSub)
        {
            dataRepository = dataRepo;
            dataRepositoryUser = dataRepoUser;
            _mapper = mapper;
            dataRepositoryAddress = dataRepoAddress;
            dataRepositoryRendezVous = dataRepoRendezVous;
            dataRepositoryReview = dataRepoReview;
            dataRepositorySub = dataRepoSub;
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

            if (office == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OfficeDetailDTO>(office.Value)); ;
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

            return CreatedAtAction("GetOfficeById", new { id = office.OfficeId }, office); // GetById : nom de l’action
        }
    }
}