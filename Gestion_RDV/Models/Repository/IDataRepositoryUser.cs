using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoryUser<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<TEntity>> GetByStringAsync(string email);
    }
}
