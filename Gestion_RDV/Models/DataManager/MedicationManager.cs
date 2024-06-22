using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class MedicationManager : IDataRepository<Medication>
    {
        private readonly GestionRdvDbContext _context;

        public MedicationManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Medication>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<Medication>>(await _context.Medications.ToListAsync());
        }

        public async Task AddAsync(Medication entity)
        {
            await _context.Medications.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medication entityToUpdate, Medication entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Medication entity)
        {
            _context.Medications.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<ActionResult<IEnumerable<Medication>>> GetAllBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Medication>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Medication>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Medication>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Medication>> GetByIdsAsync(int? userId, int? officeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Medication>>> GetAllByIdsAsync(int? officeId, int? userId)
        {

            throw new NotImplementedException();
        }

        public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
        {
            throw new NotImplementedException();
        }
    }
}
