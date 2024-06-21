using Gestion_RDV.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllBySpecialIdAsync(int id);
        Task<ActionResult<TEntity>> GetBySpecialIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string value);
        Task<ActionResult<TEntity>> GetByIdsAsync(int? id1, int? id2);
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByIdsAsync(int? id1, int? id2);
        Task<ActionResult<bool>> ExistsByIds(int id1, int id2);
    }
}
