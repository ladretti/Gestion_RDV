using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class UserManager : IDataRepository<User>
        {
            private readonly GestionRdvDbContext _context;

            public UserManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<User>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<User>>(await _context.Users.ToListAsync());
            }

            public async Task<ActionResult<User>> GetByIdAsync(int id)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return new NotFoundResult();
                return new ActionResult<User>(user);
            }

            public async Task AddAsync(User entity)
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(User entityToUpdate, User entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(User entity)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
