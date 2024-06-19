using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    public class LikeReviewManager : IDataRepository<LikeReview>
    {
        private readonly GestionRdvDbContext _context;

        public LikeReviewManager(GestionRdvDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<LikeReview>>> GetAllBySpecialIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
        public async Task AddAsync(LikeReview entity)
        {
            await _context.LikesReview.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LikeReview entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<LikeReview>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(LikeReview entityToUpdate, LikeReview entity)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<LikeReview>> GetByIdsAsync(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<LikeReview>> GetBySpecialIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<LikeReview>> GetByStringAsync(string value)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<LikeReview>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<LikeReview>> GetByIdsAsync(int? userId, int? reviewId)
        {
            var likeReview = await _context.LikesReview.FirstOrDefaultAsync(s => s.UserId == userId && s.ReviewId == reviewId);
            if (likeReview == null) return new NotFoundResult();
            return new ActionResult<LikeReview>(likeReview);
        }

        public Task<ActionResult<IEnumerable<LikeReview>>> GetAllByIdsAsync(int? id1, int? id2)
        {
            throw new NotImplementedException();
        }
    }
}
