using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class ReviewManager : IDataRepository<Review>
        {
            private readonly GestionRdvDbContext _context;

            public ReviewManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public async Task<ActionResult<IEnumerable<Review>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Review>>(await _context.Reviews.ToListAsync());
            }

            public async Task<ActionResult<Review>> GetByIdAsync(int id)
            {
                var review = await _context.Reviews.FindAsync(id);
                if (review == null) return new NotFoundResult();
                return new ActionResult<Review>(review);
            }

            public async Task AddAsync(Review entity)
            {
                _context.Reviews.Add(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Review entityToUpdate, Review entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Review entity)
            {
                _context.Reviews.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
