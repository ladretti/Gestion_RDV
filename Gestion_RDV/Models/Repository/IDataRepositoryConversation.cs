using Gestion_RDV.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_RDV.Models.Repository
{
    public interface IDataRepositoryConversation<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<ConversationDTO>>> GetConversationsWithUsersByUserIdAsync(int userId);
    }
}
