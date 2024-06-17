using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class MessageManager : IDataRepositoryTJ<Message>
        {
            private readonly GestionRdvDbContext _context;

            public MessageManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Message>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Message>>(await _context.Messages.ToListAsync());
            }

            public async Task<ActionResult<Message>> GetByIdAsync(int UserId, int ConversationId)
            {
                var message = await _context.Messages.FirstOrDefaultAsync(s => s.UserId == UserId && s.ConversationId == ConversationId);
                if (message == null) return new NotFoundResult();
                return new ActionResult<Message>(message);
            }
            public async Task<ActionResult<Message>> GetByIdAsync(int id)
            {
                return null;
            }

            public async Task AddAsync(Message entity)
            {
                _context.Messages.Add(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Message entityToUpdate, Message entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Message entity)
            {
                _context.Messages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
