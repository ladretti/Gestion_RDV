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
    public class MedicalInfosController : ControllerBase
    {

        private readonly IDataRepository<MedicalInfo> dataRepository;
        private readonly IMapper _mapper;

        public MedicalInfosController(IDataRepository<MedicalInfo> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<MedicalInfoDTO>>> GetRendezVousByOfficeId(int userId)
        {
            var rdv = await dataRepository.GetAllBySpecialIdAsync(userId);

            if (rdv == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<MedicalInfoDTO>>(rdv.Value));
        }



    }
}
