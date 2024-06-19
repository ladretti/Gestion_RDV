using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IDataRepository<Conversation> dataRepositoryConversation;
        private readonly IDataRepository<ConversationUser> dataRepositoryConversationUser;
        private readonly IDataRepository<User> dataRepositoryUser;

        public ConversationsController(IDataRepository<Conversation> dataRepoConv, IDataRepository<ConversationUser> dataRepoConvUser, IDataRepository<User> dataRepoUser)
        {
            dataRepositoryConversation = dataRepoConv;
            dataRepositoryConversationUser = dataRepoConvUser;
            dataRepositoryUser = dataRepoUser;
        }

        // GET: api/Conversations
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
            var conversations = await dataRepositoryConversation.GetAllAsync();
            if (conversations == null)
            {
                return NotFound();
            }
            return conversations;
        }

        // GET: api/Conversations/5
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Conversation>> GetConversationById(int id)
        {
            var conversation = await dataRepositoryConversation.GetByIdAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }
            return Ok(conversation);
        }

        // GET: api/Conversations/user/5
        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversationsByUserId(int userId)
        {
            var conversationsUser = await dataRepositoryConversationUser.GetAllBySpecialIdAsync(userId);
            await dataRepositoryConversation.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();

            if (conversationsUser == null)
            {
                return NotFound();
            }
            return Ok(conversationsUser);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Conversation>> PostPost(Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepositoryConversation.AddAsync(conversation);

            return CreatedAtAction("GetConversationById", new { id = conversation.ConversationId }, conversation); // GetById : nom de l’action
        }
    }
}
