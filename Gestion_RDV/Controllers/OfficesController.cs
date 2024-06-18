using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IDataRepository<Office> dataRepository;


        public OfficesController(IDataRepository<Office> dataRepo)
        {
            dataRepository = dataRepo;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Office>>> GetOfficies()
        {
            var officies = await dataRepository.GetAllAsync();

            if (officies == null)
            {
                return NotFound();
            }
            return officies;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Office>> GetUserById(int id)
        {
            var office = await dataRepository.GetByIdAsync(id);


            if (office == null)
            {
                return NotFound();
            }

            return office;
        }


    }
}