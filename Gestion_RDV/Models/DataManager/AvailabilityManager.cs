using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class AvailabilityManager : IDataRepositoyAvailability<Availability>
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
                _context.Availabilities.Add(entity);
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


            public async Task<ActionResult<IEnumerable<Availability>>> GetByOfficeId(int id)
            {
                var availabilities = await _context.Availabilities.Where(a => a.OfficeId == id).ToListAsync();

                if (availabilities == null) return new NotFoundResult();

                return new ActionResult<IEnumerable<Availability>>(availabilities);
            }

        }
    }

}
