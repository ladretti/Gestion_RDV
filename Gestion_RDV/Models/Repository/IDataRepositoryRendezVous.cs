using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoryRendezVous<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<RendezVous>>> GetRendezvousForTomorrowAsync();
    }
}
