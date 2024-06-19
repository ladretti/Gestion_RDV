using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class AvailabilityManager : IDataRepository<Availability>
        {
            private readonly GestionRdvDbContext _context;

            public AvailabilityManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Availability>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Availability>>(await _context.Availabilities.ToListAsync());
            }

            public async Task<ActionResult<Availability>> GetByIdAsync(int id)
            {
                var availability = await _context.Availabilities.FindAsync(id);
                if (availability == null) return new NotFoundResult();
                return new ActionResult<Availability>(availability);
            }

            public async Task AddAsync(Availability entity)
            {
                await _context.Availabilities.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Availability entityToUpdate, Availability entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Availability entity)
            {
                _context.Availabilities.Remove(entity);
                await _context.SaveChangesAsync();
            }


            public async Task<ActionResult<IEnumerable<Availability>>> GetAllBySpecialIdAsync(int id)
            {
                var availabilities = await _context.Availabilities.Where(a => a.OfficeId == id).ToListAsync();

                if (availabilities == null) return new NotFoundResult();

                return new ActionResult<IEnumerable<Availability>>(availabilities);
            }

            public Task<ActionResult<Availability>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            Task<ActionResult<Availability>> IDataRepository<Availability>.GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Availability>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Availability>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Availability>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<Availability>>> GetAllByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }
        }
    }

}
