using Gestion_RDV.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoryNotification<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByUserId(int id);
    }
}
