using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class ConversationManager : IDataRepositoryConversation<Conversation>
        {
            private readonly GestionRdvDbContext _context;

            public ConversationManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Conversation>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Conversation>>(await _context.Conversations.ToListAsync());
            }

            public async Task<ActionResult<Conversation>> GetByIdAsync(int id)
            {
                var conversation = await _context.Conversations.FindAsync(id);
                if (conversation == null) return new NotFoundResult();
                return new ActionResult<Conversation>(conversation);
            }

            public async Task AddAsync(Conversation entity)
            {
                await _context.Conversations.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Conversation entityToUpdate, Conversation entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Conversation entity)
            {
                _context.Conversations.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<IEnumerable<Conversation>>> GetAllBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Conversation>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Conversation>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<Conversation>>> GetAllByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Conversation>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
            {
                throw new NotImplementedException();
            }
            public async Task<ActionResult<IEnumerable<ConversationDTO>>> GetConversationsWithUsersByUserIdAsync(int userId)
            {
                var query = from conv in _context.Conversations
                            join convuser in _context.ConversationsUser on conv.ConversationId equals convuser.ConversationId
                            where convuser.UserId == userId
                            select conv;

                var conversations = await query
                    .Include(c => c.ConversationsUser)
                    .ThenInclude(cu => cu.User)
                    .ToListAsync();

                var result = conversations.Select(c => new ConversationDTO
                {
                    ConversationId = c.ConversationId,
                    Name = c.Name,
                    Users = c.ConversationsUser.Select(cu => new Conversation_UserDTO
                    {
                        UserId = cu.User.UserId,
                        FirstName = cu.User.FirstName,
                        LastName = cu.User.LastName
                    }).ToList()
                }).ToList();

                return result;
            }
        }
    }
}
