using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class FactureManager : IDataRepository<Facture>
        {
            private readonly GestionRdvDbContext _context;

            public FactureManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Facture>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Facture>>(await _context.Factures.ToListAsync());
            }

            public async Task<ActionResult<Facture>> GetByIdAsync(int id)
            {
                var facture = await _context.Factures.FindAsync(id);
                if (facture == null) return new NotFoundResult();
                return new ActionResult<Facture>(facture);
            }

            public async Task AddAsync(Facture entity)
            {
                _context.Factures.Add(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Facture entityToUpdate, Facture entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Facture entity)
            {
                _context.Factures.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<IEnumerable<Facture>>> GetAllBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Facture>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Facture>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Facture>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Facture>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<Facture>>> GetAllByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }
        }
    }

}
