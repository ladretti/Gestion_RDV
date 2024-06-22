using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class PrescriptionManager : IDataRepository<Prescription>
    {
        private readonly GestionRdvDbContext _context;

        public PrescriptionManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Prescription>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<Prescription>>(await _context.Prescriptions.ToListAsync());
        }

        public async Task AddAsync(Prescription entity)
        {
            await _context.Prescriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Prescription entityToUpdate, Prescription entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Prescription entity)
        {
            _context.Prescriptions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<ActionResult<IEnumerable<Prescription>>> GetAllBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Prescription>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Prescription>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Prescription>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Prescription>> GetByIdsAsync(int? userId, int? officeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Prescription>>> GetAllByIdsAsync(int? officeId, int? userId)
        {

            throw new NotImplementedException();
        }

        public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
        {
            throw new NotImplementedException();
        }
    }
}
