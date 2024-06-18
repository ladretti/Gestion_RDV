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
        private readonly IDataRepositoryUser<User> dataRepositoryUser;
        private readonly IMapper _mapper;



        public OfficesController(IDataRepository<Office> dataRepo, IDataRepositoryUser<User> dataRepositoryUser, IMapper mapper)
        {
            dataRepository = dataRepo;
            this.dataRepositoryUser = dataRepositoryUser;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Office>>> GetOfficies()
        {
            var officies = await dataRepository.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();


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

            if (office == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OfficeDetailDTO>(office.Value)); ;
        }


    }
}