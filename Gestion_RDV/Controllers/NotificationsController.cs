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
        private readonly IDataRepositoryNotification<Notification> dataRepository;
        private readonly IDataRepository<Office> dataRepositoryOffice;
        private readonly IDataRepository<RendezVous> dataRepositoryRDV;
        private readonly IDataRepositoryUser<User> dataRepositoryUser;
        private readonly IMapper _mapper;


        public NotificationsController(IDataRepositoryNotification<Notification> dataRepo, IDataRepository<Office> dataRepoOffice, IDataRepository<RendezVous> dataRepoRDV, IMapper mapper, IDataRepositoryUser<User> dataRepoUser)
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
            var notifications = await dataRepository.GetByUserId(userId);
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
            var notifications = await dataRepository.GetByUserId(userId);

            if (notifications == null)
            {
                return NotFound();
            }
            return notifications.Value.Count();
        }
    }
}
