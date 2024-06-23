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
        private readonly IDataRepositoryConversation<Conversation> dataRepositoryConversation;
        private readonly IDataRepository<ConversationUser> dataRepositoryConversationUser;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IMapper _mapper;

        public ConversationsController(IDataRepositoryConversation<Conversation> dataRepoConv, IDataRepository<ConversationUser> dataRepoConvUser, IDataRepository<User> dataRepoUser, IMapper mapper)
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
        /*[Authorize]*/
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Conversation>> GetConversationById(int conversationId)
        {
            var conversation = await dataRepositoryConversation.GetByIdAsync(conversationId);

            if (conversation == null)
            {
                return NotFound();
            }
            return Ok(conversation);
        }

        /*[Authorize]*/
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ConversationDTO>> PostConversation(ConversationPostDTO conversationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conversationEntity = new Conversation
            {
                Name = conversationDto.ConversationName
            };

            try
            {
                await dataRepositoryConversation.AddAsync(conversationEntity);

                foreach (var userId in conversationDto.UserIds)
                {
                    var conversationUserEntity = new ConversationUser
                    {
                        ConversationId = conversationEntity.ConversationId,
                        UserId = userId
                    };

                    await dataRepositoryConversationUser.AddAsync(conversationUserEntity);
                }
                Conversation conv = _mapper.Map<Conversation>(conversationDto);

               /* var resultDto = _mapper.Map<ConversationPostDTO>(conversationEntity);
                resultDto.UserIds = conversationDto.UserIds; // Ajouter les UserIds au résultat*/

                //return CreatedAtAction(nameof(GetConversationById), new { conversationId = conversationEntity.ConversationId }, resultDto);
                return CreatedAtAction(nameof(GetConversationById), new { conversationId = conversationEntity.ConversationId }, _mapper.Map<ConversationDTO>(conv));
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Impossible de créer la conversation");
            }
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ConversationDTO>>> GetConversationsWithUsersByUserId(int userId)
        {
            var conversations = await dataRepositoryConversation.GetConversationsWithUsersByUserIdAsync(userId);
            if (conversations == null)
            {
                return NotFound();
            }

            return Ok(conversations);
        }
    }
}
