﻿using System;
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
        private readonly IDataRepository<Notification> dataRepositoryRDV;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IMapper _mapper;


        public NotificationsController(IDataRepository<Notification> dataRepo, IDataRepository<Office> dataRepoOffice, IDataRepository<Notification> dataRepoRDV, IMapper mapper, IDataRepository<User> dataRepoUser)
        {
            dataRepository = dataRepo;
            dataRepositoryOffice = dataRepoOffice;
            dataRepositoryRDV = dataRepoRDV;
            dataRepositoryUser = dataRepoUser;
            _mapper = mapper;
        }


        [HttpGet("GetByUserId/{userId}")]
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

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Notification>> GetNotificationById(int id)
        {
            var notification = await dataRepository.GetByIdAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(notification);

            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.NotificationId }, notification);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await dataRepository.GetByIdAsync(id);
            if (notification.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(notification.Value);

            return NoContent();
        }
    }
}
