using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class OfficeManager : IDataRepository<Office>
        {
            private readonly GestionRdvDbContext _context;

            public OfficeManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Office>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Office>>(await _context.Officies.ToListAsync());
            }

            public async Task<ActionResult<Office>> GetByIdAsync(int id)
            {
                var office = await _context.Officies.FindAsync(id);
                if (office == null) return new NotFoundResult();
                return new ActionResult<Office>(office);
            }

            public async Task AddAsync(Office entity)
            {
                await _context.Officies.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Office entityToUpdate, Office entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Office entity)
            {
                _context.Officies.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<IEnumerable<Office>>> GetAllBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Office>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Office>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Office>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Office>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<Office>>> GetAllByIdsAsync(int? id1, int? id2)
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
