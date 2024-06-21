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
            var subscriptions = await dataRepository.GetByIdsAsync(userId, officeId);

            if (subscriptions.Value == null)
            {
                return false;
            }

            return true;
        }

        [HttpGet("GetSubscriptionById/{officeId}/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SubscriptionPostDTO>> GetSubscriptionById(int officeId, int userId)
        {
            var like = await dataRepository.GetByIdsAsync(userId, officeId);

            if (like.Value == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SubscriptionPostDTO>(like.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubscriptionPostDTO>> ReviewSubscription(SubscriptionPostDTO subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subscription sub = _mapper.Map<Subscription>(subscription);
            await dataRepository.AddAsync(sub);

            return CreatedAtAction(nameof(GetSubscriptionById), new { officeId = subscription.OfficeId, userId = subscription.UserId }, _mapper.Map<SubscriptionPostDTO>(subscription));
        }

        [HttpDelete("{userId}/{officeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubscription(int userId, int officeId)
        {
            var subscription = await dataRepository.GetByIdsAsync(userId, officeId);
            if (subscription == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(subscription.Value);

            return NoContent();
        }
    }
}
