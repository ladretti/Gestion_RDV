﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Gestion_RDV.Filters;
using System.Diagnostics;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IDataRepositoryMessage<Message> dataRepositoryMessage;
        private readonly IDataRepository<ConversationUser> dataRepositoryConversationUser;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IMapper _mapper;

        public MessagesController(IDataRepositoryMessage<Message> dataRepoMsg, IDataRepository<ConversationUser> dataRepoConvser, IDataRepository<User> dataRepoUser, IMapper mapper)
        {
            dataRepositoryMessage = dataRepoMsg;
            dataRepositoryConversationUser = dataRepoConvser;
            dataRepositoryUser = dataRepoUser;
            _mapper = mapper;
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("message/{userId}/{conversationId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Conversation>> GetMessageByIds(int userId, int conversationId)
        {
            var message = await dataRepositoryMessage.GetByIdsAsync(userId, conversationId);

            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("messages/{conversationId}/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages(int conversationId, int userId)
        {
            var userIsInConversation  = await dataRepositoryConversationUser.ExistsByIds(conversationId, userId);

            if (!userIsInConversation.Value){
                return Forbid(); // Renvoie un statut HTTP 403 Forbidden
            }

            var messages = await dataRepositoryMessage.GetAllBySpecialIdAsync(conversationId);
            await dataRepositoryUser.GetAllAsync();

            if (messages == null){
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<MessageDTO>>(messages.Value));
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("newMessages/{conversationId}/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetNewMessages(int conversationId, int userId, [FromQuery] DateTime since)
        {
            var userIsInConversation = await dataRepositoryConversationUser.ExistsByIds(conversationId, userId);
            if (!userIsInConversation.Value)
            {
                return Forbid(); // Renvoie un statut HTTP 403 Forbidden
            }

            var newMessages = await dataRepositoryMessage.GetNewMessagesAsync(conversationId, since);
            await dataRepositoryUser.GetAllAsync();

            if (newMessages == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<MessageDTO>>(newMessages.Value));
        }

        /*[Authorize]
        [UserAuthorize("userId")]*/
        [HttpGet("messagesPaged/{conversationId}/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesPaged(int conversationId, int userId, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10, DateTime? beforeDate = null)
        {
            var userIsInConversation = await dataRepositoryConversationUser.ExistsByIds(conversationId, userId);
            await dataRepositoryUser.GetAllAsync();

            if (!userIsInConversation.Value)
            {
                return Forbid(); // Renvoie un statut HTTP 403 Forbidden
            }

            var messages = await dataRepositoryMessage.GetMessagesPagedAsync(conversationId, pageIndex, pageSize, beforeDate);

            if (messages == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<MessageDTO>>(messages.Value));
        }

        // POST: api/Messages
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<MessagePostDTO>> PostMessage(MessagePostDTO message)
        {
            // Vérifier si l'utilisateur fait partie de la conversation
            var userIsInConversation = await dataRepositoryConversationUser.ExistsByIds(message.ConversationId, message.UserId);
            if (!userIsInConversation.Value)
            {
                return Forbid(); // Renvoie un statut HTTP 403 Forbidden si l'utilisateur n'est pas autorisé
            }

            // Validation du modèle
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var messageEntity = _mapper.Map<Message>(message);
                await dataRepositoryMessage.AddAsync(messageEntity);

                return CreatedAtAction(nameof(GetMessageByIds), new { conversationId = message.ConversationId, userId = message.UserId }, messageEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Erreur lors de l'ajout du message");
            }

        }
    }
}
