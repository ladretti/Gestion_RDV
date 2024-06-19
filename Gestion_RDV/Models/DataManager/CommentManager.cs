using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class CommentManager : IDataRepository<Comment>
        {
            private readonly GestionRdvDbContext _context;

            public CommentManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Comment>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Comment>>(await _context.Comments.ToListAsync());
            }

            public async Task<ActionResult<Comment>> GetByIdAsync(int id)
            {
                var comment = await _context.Comments.FindAsync(id);
                if (comment == null) return new NotFoundResult();
                return new ActionResult<Comment>(comment);
            }

            public async Task AddAsync(Comment entity)
            {
                await _context.Comments.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Comment entityToUpdate, Comment entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Comment entity)
            {
                _context.Comments.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<IEnumerable<Comment>>> GetAllBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Comment>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Comment>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Comment>> GetByIdsAsync(int id1, int id2)
            {
                throw new NotImplementedException();
            }
        }
    }

}
