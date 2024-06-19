using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gestion_RDV.Models.DataManager
{
    public class ConversationUserManager : IDataRepository<ConversationUser>
    {
        private readonly GestionRdvDbContext _context;

        public ConversationUserManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<ConversationUser>>> GetAllBySpecialIdAsync(int userId)
        {
            var conversationUsers = await _context.ConversationsUser.Where(c => c.UserId == userId).ToListAsync();

            if (conversationUsers == null) return new NotFoundResult();

            return new ActionResult<IEnumerable<ConversationUser>>(conversationUsers);
        }
        public async Task AddAsync(ConversationUser entity)
        {
            await _context.ConversationsUser.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ConversationUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<ConversationUser>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<ConversationUser>>(await _context.ConversationsUser.ToListAsync());
        }
        public async Task UpdateAsync(ConversationUser entityToUpdate, ConversationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ConversationUser>> GetByIdsAsync(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ConversationUser>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ConversationUser>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<ConversationUser>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
