using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class EquipmentManager : IDataRepository<Equipment>
    {
        private readonly GestionRdvDbContext _context;

        public EquipmentManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Equipment>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<Equipment>>(await _context.Equipments.ToListAsync());
        }

        public async Task AddAsync(Equipment entity)
        {
            await _context.Equipments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipment entityToUpdate, Equipment entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Equipment entity)
        {
            _context.Equipments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<ActionResult<IEnumerable<Equipment>>> GetAllBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Equipment>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Equipment>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Equipment>> GetByIdAsync(int id)
        {
            var diagnosis = await _context.Equipments.FindAsync(id);
            if (diagnosis == null) return new NotFoundResult();
            return new ActionResult<Equipment>(diagnosis);
        }

        public async Task<ActionResult<Equipment>> GetByIdsAsync(int? userId, int? officeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Equipment>>> GetAllByIdsAsync(int? officeId, int? userId)
        {

            throw new NotImplementedException();
        }

        public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
        {
            throw new NotImplementedException();
        }
    }
}
