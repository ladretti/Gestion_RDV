using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IDataRepositoryConversation<Conversation> dataRepository;

        public ConversationsController(IDataRepositoryConversation<Conversation> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Conversations
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
            var conversations = await dataRepository.GetAllAsync();
            if (conversations == null)
            {
                return NotFound();
            }
            return conversations;
        }

        // GET: api/Conversations/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Conversation>> GetConversationById(int id)
        {
            var conversation = await dataRepository.GetByIdAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }
            return Ok(conversation);
        }

        // GET: api/Conversations/user/5
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversationsByUserId(int userId)
        {
            var conversations = await dataRepository.GetConversationsByUserIdAsync(userId);
            if (conversations == null)
            {
                return NotFound();
            }
            return Ok(conversations);
        }


        /*// PUT: api/Conversations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversation(int id, Conversation conversation)
        {
            if (id != conversation.ConversationId)
            {
                return BadRequest();
            }

            _context.Entry(conversation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Conversations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conversation>> PostConversation(Conversation conversation)
        {
            if (_context.Conversations == null)
            {
                return Problem("Entity set 'GestionRdvDbContext.Conversations'  is null.");
            }
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversation", new { id = conversation.ConversationId }, conversation);
        }

        // DELETE: api/Conversations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            if (_context.Conversations == null)
            {
                return NotFound();
            }
            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }

            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConversationExists(int id)
        {
            return (_context.Conversations?.Any(e => e.ConversationId == id)).GetValueOrDefault();
        }*/
    }
}
