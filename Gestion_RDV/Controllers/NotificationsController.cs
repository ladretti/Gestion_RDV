using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Gestion_RDV.Filters;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IDataRepositoryNotification<Notification> dataRepository;

        public NotificationsController(IDataRepositoryNotification<Notification> dataRepo)
        {
            dataRepository = dataRepo;
        }


        [Authorize]
        [HttpGet("{userId}")]
        [UserAuthorize("userId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsById(int userId)
        {
            var notifications = await dataRepository.GetByUserId(userId);

            if (notifications == null)
            {
                return NotFound();
            }
            return notifications;
        }
        
        [Authorize]
        [HttpGet("{userId}/count")]
        [UserAuthorize("userId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<int>> GetNotificationsNumberById(int userId)
        {
            var notifications = await dataRepository.GetByUserId(userId);

            if (notifications == null)
            {
                return NotFound();
            }
            return notifications.Value.Count();
        }
    }
}
