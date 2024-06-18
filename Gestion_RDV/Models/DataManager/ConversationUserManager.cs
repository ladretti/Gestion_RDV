using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gestion_RDV.Models.DataManager
{
    public class ConversationUserManager : IDataRepositoryConversationUser<ConversationUser>
    {
        private readonly GestionRdvDbContext _context;

        public ConversationUserManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ConversationUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(ConversationUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<ConversationUser>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<ConversationUser>>(await _context.ConversationsUser.ToListAsync());
        }

        public async Task<ActionResult<ConversationUser>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<ConversationUser>>> GetConversationsByUserIdAsync(int userId)
        {
            var conversationUsers = await _context.ConversationsUser
                                                  .Where(c => c.UserId == userId)
                                                  .ToListAsync();

            return new ActionResult<IEnumerable<ConversationUser>>(conversationUsers);
        }


        public async Task UpdateAsync(ConversationUser entityToUpdate, ConversationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
