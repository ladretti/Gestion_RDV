using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class MedicalInfoManager : IDataRepository<MedicalInfo>
    {
        private readonly GestionRdvDbContext _context;

        public MedicalInfoManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<MedicalInfo>>> GetAllAsync()
        {
            return new ActionResult<IEnumerable<MedicalInfo>>(await _context.MedicalInfos.ToListAsync());
        }

        public async Task AddAsync(MedicalInfo entity)
        {
            await _context.MedicalInfos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MedicalInfo entityToUpdate, MedicalInfo entity)
        {
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MedicalInfo entity)
        {
            _context.MedicalInfos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<MedicalInfo>>> GetAllBySpecialIdAsync(int id)
        {
            var medicalInfo = await _context.MedicalInfos.Where(a => a.UserId == id).ToListAsync();
            if (medicalInfo == null) return new NotFoundResult();
            return new ActionResult<IEnumerable<MedicalInfo>>(medicalInfo);
        }

        public Task<ActionResult<MedicalInfo>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<MedicalInfo>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<MedicalInfo>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<MedicalInfo>> GetByIdsAsync(int? userId, int? officeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<MedicalInfo>>> GetAllByIdsAsync(int? officeId, int? userId)
        {

            throw new NotImplementedException();
        }

        public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
        {
            throw new NotImplementedException();
        }
    }
}
