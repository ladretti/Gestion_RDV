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
                _context.Conversations.Add(entity);
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
        }
    }
}
