using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoryPost<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByParentIdAsync(int id);
    }
}