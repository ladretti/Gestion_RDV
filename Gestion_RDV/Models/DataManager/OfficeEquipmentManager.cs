using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class OfficeEquipmentManager : IDataRepository<OfficeEquipment>
    {
        private readonly GestionRdvDbContext _context;

        public OfficeEquipmentManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<OfficeEquipment>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<OfficeEquipment>>(await _context.OfficeEquipments.ToListAsync());
        }

        public async Task AddAsync(OfficeEquipment entity)
        {
            await _context.OfficeEquipments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OfficeEquipment entityToUpdate, OfficeEquipment entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OfficeEquipment entity)
        {
            _context.OfficeEquipments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<OfficeEquipment>>> GetAllBySpecialIdAsync(int id)
        {
            var officeEquipment = await _context.OfficeEquipments.Where(a => a.OfficeId == id).ToListAsync();

            if (officeEquipment == null) return new NotFoundResult();

            return new ActionResult<IEnumerable<OfficeEquipment>>(officeEquipment);
        }

        public Task<ActionResult<OfficeEquipment>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<OfficeEquipment>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<OfficeEquipment>> GetByIdAsync(int id)
        {
            var diagnosis = await _context.OfficeEquipments.FindAsync(id);
            if (diagnosis == null) return new NotFoundResult();
            return new ActionResult<OfficeEquipment>(diagnosis);
        }

        public async Task<ActionResult<OfficeEquipment>> GetByIdsAsync(int? equipmentid, int? officeId)
        {
            var officeEquipment = await _context.OfficeEquipments.FirstOrDefaultAsync(s => s.OfficeId == officeId && s.EquipmentId == equipmentid);
            if (officeEquipment == null) return new NotFoundResult();
            return new ActionResult<OfficeEquipment>(officeEquipment);
        }

        public async Task<ActionResult<IEnumerable<OfficeEquipment>>> GetAllByIdsAsync(int? officeId, int? userId)
        {

            throw new NotImplementedException();
        }

        public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
        {
            throw new NotImplementedException();
        }
    }
}
