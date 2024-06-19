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
    public class SubscriptionsController : ControllerBase
    {
        private readonly IDataRepository<Subscription> dataRepository;
        private readonly IMapper _mapper;

        public SubscriptionsController(IDataRepository<Subscription> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        [HttpGet("{officeId}/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> IsSubscribe(int officeId, int userId)
        {
            var subscriptions = await dataRepository.GetByIdsAsync(officeId, userId);

            if (subscriptions.Result == null)
            {
                return false;
            }

            return true;

        }
    }
}
