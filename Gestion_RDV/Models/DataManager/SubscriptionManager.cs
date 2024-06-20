using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class SubscriptionManager : IDataRepository<Subscription>
        {
            private readonly GestionRdvDbContext _context;

            public SubscriptionManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Subscription>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Subscription>>(await _context.Subscriptions.ToListAsync());
            }

            public async Task AddAsync(Subscription entity)
            {
                await _context.Subscriptions.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Subscription entityToUpdate, Subscription entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Subscription entity)
            {
                _context.Subscriptions.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<IEnumerable<Subscription>>> GetAllBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Subscription>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Subscription>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Subscription>> GetByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public async Task<ActionResult<Subscription>> GetByIdsAsync(int? userId, int? officeId)
            {
                var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.UserId == userId && s.OfficeId == officeId);
                if (subscription == null) return new NotFoundResult();
                return new ActionResult<Subscription>(subscription);
            }

            public async Task<ActionResult<IEnumerable<Subscription>>> GetAllByIdsAsync(int? officeId, int? userId)
            {

                if (officeId == null && userId == null)
                {
                    return new BadRequestResult();
                }

                var subscription = await _context.Subscriptions.Where(s => (officeId == null || s.OfficeId == officeId) && (userId == null || s.UserId == userId)).ToListAsync();

                if (subscription == null)
                {
                    return new NotFoundResult();
                }

                return new ActionResult<IEnumerable<Subscription>>(subscription);
            }

            public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
            {
                throw new NotImplementedException();
            }
        }
    }

}
