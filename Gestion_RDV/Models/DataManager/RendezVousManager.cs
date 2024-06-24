using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class RendezVousManager : IDataRepositoryRendezVous<RendezVous>
        {
            private readonly GestionRdvDbContext _context;

            public RendezVousManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<RendezVous>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<RendezVous>>(await _context.RendezVous.ToListAsync());
            }

            public async Task<ActionResult<RendezVous>> GetByIdAsync(int id)
            {
                var rendezVous = await _context.RendezVous.FindAsync(id);
                if (rendezVous == null) return new NotFoundResult();
                return new ActionResult<RendezVous>(rendezVous);
            }

            public async Task AddAsync(RendezVous entity)
            {
                await _context.RendezVous.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(RendezVous entityToUpdate, RendezVous entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(RendezVous entity)
            {
                _context.RendezVous.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public async Task<ActionResult<IEnumerable<RendezVous>>> GetAllBySpecialIdAsync(int id)
            {
                var rendezVous = await _context.RendezVous.Where(a => a.OfficeId == id).ToListAsync();
                if (rendezVous == null) return new NotFoundResult();
                return new ActionResult<IEnumerable<RendezVous>>(rendezVous);
            }

            public async Task<ActionResult<RendezVous>> GetBySpecialIdAsync(int id)
            {
                var rendezVous = await _context.RendezVous.FirstOrDefaultAsync(a => a.OfficeId == id);
                if (rendezVous == null) return new NotFoundResult();
                return new ActionResult<RendezVous>(rendezVous);
            }

            public Task<ActionResult<RendezVous>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }


            public async Task<ActionResult<RendezVous>> GetByIdsAsync(int? userId, int? officeId)
            {
                var rendezVous = await _context.RendezVous.FirstOrDefaultAsync(s => s.UserId == userId && s.OfficeId == officeId);
                if (rendezVous == null) return new NotFoundResult();
                return new ActionResult<RendezVous>(rendezVous);
            }

            public async Task<ActionResult<IEnumerable<RendezVous>>> GetAllByIdsAsync(int? userId, int? officeId)
            {
                if (userId == null && officeId == null)
                {
                    return new BadRequestResult();
                }

                var rendezVous = await _context.RendezVous.Where(s => (userId == null || s.UserId == userId) && (officeId == null || s.OfficeId == officeId)).ToListAsync();

                if (rendezVous == null)
                {
                    return new NotFoundResult();
                }

                return new ActionResult<IEnumerable<RendezVous>>(rendezVous);
            }

            public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezvousForTomorrowAsync()
            {
                var tomorrow = DateTime.UtcNow.Date.AddDays(1); // Début du jour suivant

                var startOfTomorrow = tomorrow.Date; // 00:00:00 du jour suivant
                var endOfTomorrow = tomorrow.Date.AddDays(1).AddTicks(-1); // 23:59:59.9999999 du jour suivant

                return await _context.RendezVous
                    .Where(rv => rv.StartDate >= startOfTomorrow && rv.StartDate <= endOfTomorrow)
                    .ToListAsync();
            }

        }
    }

}
