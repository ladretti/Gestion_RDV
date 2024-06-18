using Gestion_RDV.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoyAvailability<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByOfficeId(int id);
    }
}
