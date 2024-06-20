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
                await _context.Notifications.AddAsync(entity);
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

            public async Task<ActionResult<IEnumerable<Notification>>> GetAllBySpecialIdAsync(int id)
            {
                var notifications = await _context.Notifications.Where(a => a.UserId == id).ToListAsync();

                if (notifications == null) return new NotFoundResult();

                return new ActionResult<IEnumerable<Notification>>(notifications);
            }

            public Task<ActionResult<Notification>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Notification>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Notification>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Notification>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<Notification>>> GetAllByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
            {
                throw new NotImplementedException();
            }
        }
    }

}
