using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class SocialMediaAccountManager : IDataRepository<SocialMediaAccount>
        {
            private readonly GestionRdvDbContext _context;

            public SocialMediaAccountManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<SocialMediaAccount>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<SocialMediaAccount>>(await _context.SocialMediaAccounts.ToListAsync());
            }

            public async Task<ActionResult<SocialMediaAccount>> GetByIdAsync(int id)
            {
                var socialMediaAccount = await _context.SocialMediaAccounts.FindAsync(id);
                if (socialMediaAccount == null) return new NotFoundResult();
                return new ActionResult<SocialMediaAccount>(socialMediaAccount);
            }

            public async Task AddAsync(SocialMediaAccount entity)
            {
                _context.SocialMediaAccounts.Add(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(SocialMediaAccount entityToUpdate, SocialMediaAccount entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(SocialMediaAccount entity)
            {
                _context.SocialMediaAccounts.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<IEnumerable<SocialMediaAccount>>> GetAllBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<SocialMediaAccount>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<SocialMediaAccount>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<SocialMediaAccount>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<SocialMediaAccount>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<SocialMediaAccount>>> GetAllByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }
        }
    }

}
