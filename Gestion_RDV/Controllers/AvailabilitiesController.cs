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
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IDataRepositoyAvailability<Availability> dataRepository;
        //private readonly IDataRepositoryUser<User> dataRepositoryUser;


        public AvailabilitiesController(IDataRepositoyAvailability<Availability> dataRepo)
        {
            dataRepository = dataRepo;
        }

        [HttpGet("{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAvailabilitiesByOfficeId(int officeId)
        {
            var availabilities = await dataRepository.GetByOfficeId(officeId);

            if (availabilities == null)
            {
                return NotFound();
            }

            return availabilities;
        }

    }
}
