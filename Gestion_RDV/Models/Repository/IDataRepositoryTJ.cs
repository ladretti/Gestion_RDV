using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoryTJ<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int id1, int id2);
    }
}
