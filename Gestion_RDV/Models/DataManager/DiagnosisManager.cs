using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class DiagnosisManager : IDataRepository<Diagnosis>
    {
        private readonly GestionRdvDbContext _context;

        public DiagnosisManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Diagnosis>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<Diagnosis>>(await _context.Diagnoses.ToListAsync());
        }

        public async Task AddAsync(Diagnosis entity)
        {
            await _context.Diagnoses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Diagnosis entityToUpdate, Diagnosis entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Diagnosis entity)
        {
            _context.Diagnoses.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<ActionResult<IEnumerable<Diagnosis>>> GetAllBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Diagnosis>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Diagnosis>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Diagnosis>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Diagnosis>> GetByIdsAsync(int? userId, int? officeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Diagnosis>>> GetAllByIdsAsync(int? officeId, int? userId)
        {

            throw new NotImplementedException();
        }

        public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
        {
            throw new NotImplementedException();
        }
    }
}
