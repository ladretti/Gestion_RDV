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
using Gestion_RDV.Models.DTO;
using AutoMapper;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IDataRepository<Notification> dataRepository;
        private readonly IDataRepository<Office> dataRepositoryOffice;
        private readonly IDataRepository<RendezVous> dataRepositoryRDV;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IMapper _mapper;


        public NotificationsController(IDataRepository<Notification> dataRepo, IDataRepository<Office> dataRepoOffice, IDataRepository<RendezVous> dataRepoRDV, IMapper mapper, IDataRepository<User> dataRepoUser)
        {
            dataRepository = dataRepo;
            dataRepositoryOffice = dataRepoOffice;
            dataRepositoryRDV = dataRepoRDV;
            dataRepositoryUser = dataRepoUser;
            _mapper = mapper;
        }


        [HttpGet("{userId}")]
        /*[Authorize]
        [UserAuthorize("userId")]*/
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsById(int userId)
        {
            var notifications = await dataRepository.GetAllBySpecialIdAsync(userId);
            await dataRepositoryOffice.GetAllAsync();
            await dataRepositoryRDV.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();


            if (notifications == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<NotificationDetailsDTO>>(notifications.Value)); ;
        }
        
        [Authorize]
        [HttpGet("{userId}/count")]
        [UserAuthorize("userId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<int>> GetNotificationsNumberById(int userId)
        {
            var notifications = await dataRepository.GetAllBySpecialIdAsync(userId);

            if (notifications == null)
            {
                return NotFound();
            }
            return notifications.Value.Count();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Notification>> GetNotificationById(int id)
        {
            var notification = await dataRepository.GetByIdAsync(id);
            await dataRepositoryUser.GetByIdAsync(notification.Value.NotificationId);
            await dataRepository.GetAllBySpecialIdAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Notification>> PostOffice(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(notification);

            return CreatedAtAction("GetNotificationById", new { id = notification.NotificationId }, notification); // GetById : nom de l’action
        }
    }
}
