using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class PostManager : IDataRepositoryPost<Post>
        {
            private readonly GestionRdvDbContext _context;

            public PostManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Post>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Post>>(await _context.Posts.ToListAsync());
            }

            public async Task<ActionResult<Post>> GetByIdAsync(int id)
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null) return new NotFoundResult();
                return new ActionResult<Post>(post);
            }

            public async Task<ActionResult<IEnumerable<Post>>> GetByParentIdAsync(int id)
            {
                return new ActionResult<IEnumerable<Post>>(await _context.Posts.Where(a => a.ParentPostId == id).ToListAsync());
            }

            public async Task AddAsync(Post entity)
            {
                _context.Posts.Add(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Post entityToUpdate, Post entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Post entity)
            {
                _context.Posts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
