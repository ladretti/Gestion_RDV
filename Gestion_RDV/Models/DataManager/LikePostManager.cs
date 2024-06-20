using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class LikePostManager : IDataRepository<LikePost>
    {
        private readonly GestionRdvDbContext _context;

        public LikePostManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<LikePost>>> GetAllBySpecialIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public async Task AddAsync(LikePost entity)
        {
            await _context.LikesPost.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LikePost entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<LikePost>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(LikePost entityToUpdate, LikePost entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<LikePost>> GetByIdsAsync(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<LikePost>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<LikePost>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<LikePost>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<LikePost>> GetByIdsAsync(int? userId, int? postId)
        {
            var likePost = await _context.LikesPost.FirstOrDefaultAsync(s => s.UserId == userId && s.PostId == postId);
            if (likePost == null) return new NotFoundResult();
            return new ActionResult<LikePost>(likePost);
        }

        public Task<ActionResult<IEnumerable<LikePost>>> GetAllByIdsAsync(int? id1, int? id2)
        {
            throw new NotImplementedException();
        }
    }
}
