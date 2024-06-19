using System;
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

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IDataRepository<Message> dataRepositoryMessage;
        private readonly IDataRepository<ConversationUser> dataRepositoryConversationUser;

        public MessagesController(IDataRepository<Message> dataRepoMsg, IDataRepository<ConversationUser> dataRepoConvser)
        {
            dataRepositoryMessage = dataRepoMsg;
            dataRepositoryConversationUser = dataRepoConvser;
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

        //[Authorize]
        [HttpGet("messages/{userId}/{conversationId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Notification>>> GetMessagesByConversationId(int userId, int conversationId)
        {
            var messages = await dataRepositoryConversationUser.GetAllBySpecialIdAsync(conversationId);
            await dataRepositoryMessage.GetAllAsync();


            if (messages == null)
            {
                return NotFound();
            }

            return Ok(messages);
        }

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepositoryMessage.AddAsync(message);

            return CreatedAtAction("GetMessageByIds", new { userId = message.UserId, conversationId = message.ConversationId }, message); // GetById : nom de l’action
        }
    }
}
