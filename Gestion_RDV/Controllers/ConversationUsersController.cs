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
        private readonly IMapper _mapper;

        public ConversationUsersController(IDataRepository<ConversationUser> dataRepoConvUser, IMapper mapper)
        {
            dataRepositoryConversationUser = dataRepoConvUser;
            _mapper = mapper;
        }

        /*[Authorize]
       [UserAuthorize("userId")]*/
        [HttpGet("convUser/{userId}/{conversationId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Conversation>> GetConversationUserById(int userId, int conversationId)
        {
            var message = await dataRepositoryConversationUser.GetByIdsAsync(userId, conversationId);

            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ConversationUserDTO>> PostConversationUser(ConversationUserDTO convUser)
        {
            // Validation du modèle
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            try
            {
                var convUserEntity = _mapper.Map<ConversationUser>(convUser);
                await dataRepositoryConversationUser.AddAsync(convUserEntity);

                return CreatedAtAction(nameof(GetConversationUserById), new { userId = convUser.UserId, conversationId = convUser.ConversationId }, convUserEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Erreur lors de l'ajout du de l'utilisateur à la conversation");
            }

        }
        //[Authorize]
        [HttpDelete("{userId}/{conversationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteConversationUser(int userId, int conversationId)
        {
            var convUser = await dataRepositoryConversationUser.GetByIdsAsync(userId, conversationId);
            if (convUser.Value == null)
            {
                return NotFound();
            }

            await dataRepositoryConversationUser.DeleteAsync(convUser.Value);

            return NoContent();
        }
    }
}
