using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class RendezVousManager : IDataRepository<RendezVous>
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

            public Task<ActionResult<RendezVous>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<RendezVous>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<RendezVous>>> GetAllByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }
        }
    }

}
