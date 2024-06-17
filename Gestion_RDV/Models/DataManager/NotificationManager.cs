using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class NotificationManager : IDataRepository<Notification>
        {
            private readonly GestionRdvDbContext _context;

            public NotificationManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Notification>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Notification>>(await _context.Notifications.ToListAsync());
            }

            public async Task<ActionResult<Notification>> GetByIdAsync(int id)
            {
                var notification = await _context.Notifications.FindAsync(id);
                if (notification == null) return new NotFoundResult();
                return new ActionResult<Notification>(notification);
            }

            public async Task AddAsync(Notification entity)
            {
                _context.Notifications.Add(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Notification entityToUpdate, Notification entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Notification entity)
            {
                _context.Notifications.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
