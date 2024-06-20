using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using AutoMapper;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DTO;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationUsersController : ControllerBase
    {
        private readonly IDataRepository<ConversationUser> dataRepositoryConversationUser;
        private readonly IDataRepository<Conversation> dataRepositoryConversation;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IMapper _mapper;

        public ConversationUsersController(IDataRepository<Conversation> dataRepoConv, IDataRepository<ConversationUser> dataRepoConvUser, IDataRepository<User> dataRepoUser, IMapper mapper)
        {
            dataRepositoryConversation = dataRepoConv;
            dataRepositoryConversationUser = dataRepoConvUser;
            dataRepositoryUser = dataRepoUser;
            _mapper = mapper;
        }

        // GET: api/Conversations/user/5
        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ConversationUser>>> GetConversationsByUserId(int userId)
        {
            var conversationsUser = await dataRepositoryConversationUser.GetAllBySpecialIdAsync(userId);
            await dataRepositoryConversation.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();

            if (conversationsUser == null)
            {
                return NotFound();
            }
            return Ok(conversationsUser);
            //return Ok(_mapper.Map<IEnumerable<ConversationUserDTO>>(conversationsUser.Value));
        }
    }
}
