using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class AddressManager : IDataRepository<Address>
        {
            private readonly GestionRdvDbContext _context;

            public AddressManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Address>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Address>>(await _context.Addresses.ToListAsync());
            }

            public async Task<ActionResult<Address>> GetByIdAsync(int id)
            {
                var address = await _context.Addresses.FindAsync(id);
                if (address == null) return new NotFoundResult();
                return new ActionResult<Address>(address);
            }

            public async Task AddAsync(Address entity)
            {
                await _context.Addresses.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Address entityToUpdate, Address entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Address entity)
            {
                _context.Addresses.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<IEnumerable<Address>>> GetAllBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Address>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Address>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Address>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Address>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<Address>>> GetAllByIdsAsync(int? id1, int? id2)
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
