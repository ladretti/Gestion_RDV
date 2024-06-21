using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoryMessage<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetNewMessagesAsync(int conversationId, DateTime since);
        Task<ActionResult<IEnumerable<TEntity>>> GetMessagesPagedAsync(int conversationId, int pageIndex, int pageSize);
    }
}
