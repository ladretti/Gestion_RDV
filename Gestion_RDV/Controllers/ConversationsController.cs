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
using Gestion_RDV.Models.DTO;
using AutoMapper;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IDataRepository<Conversation> dataRepositoryConversation;
        private readonly IDataRepository<ConversationUser> dataRepositoryConversationUser;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IMapper _mapper;

        public ConversationsController(IDataRepository<Conversation> dataRepoConv, IDataRepository<ConversationUser> dataRepoConvUser, IDataRepository<User> dataRepoUser, IMapper mapper)
        {
            dataRepositoryConversation = dataRepoConv;
            dataRepositoryConversationUser = dataRepoConvUser;
            dataRepositoryUser = dataRepoUser;
            _mapper = mapper;
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
        /* [HttpGet("user/{userId}")]
         [ProducesResponseType(200)]
         [ProducesResponseType(404)]
         public async Task<ActionResult<IEnumerable<Conversation>>> GetConversationsByUserId(int userId)
         {
             var conversationsUser = await dataRepositoryConversationUser.GetAllBySpecialIdAsync(userId);
             await dataRepositoryConversation.GetAllAsync();
             await dataRepositoryConversationUser.GetAllAsync();
             await dataRepositoryUser.GetAllAsync();

             if (conversationsUser == null)
             {
                 return NotFound();
             }
             return Ok(conversationsUser);
             //return Ok(_mapper.Map<IEnumerable<ConversationDTO>>(conversationsUser.Value));
         }*/

        // GET: api/Conversations/user/5
        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversationsByUserId(int userId)
        {
            var conv = await dataRepositoryConversation.GetAllAsync();
            var conversationsUser = await dataRepositoryConversationUser.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();

            if (conv == null)
            {
                return NotFound();
            }
            var filteredConversations = conv.Value.Where(c => c.ConversationsUser != null && c.ConversationsUser.Any(cu => cu.UserId == userId)).ToList();
            return Ok(filteredConversations);
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
