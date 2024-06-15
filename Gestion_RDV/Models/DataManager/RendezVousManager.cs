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
                _context.RendezVous.Add(entity);
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
        }
    }

}
