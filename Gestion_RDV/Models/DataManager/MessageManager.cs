﻿using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RDV.Models.DataManager
{
    namespace API_Gymbrodyssey.Models.DataManager
    {
        public class MessageManager : IDataRepositoryMessage<Message>
        {
            private readonly GestionRdvDbContext _context;

            public MessageManager(GestionRdvDbContext context)
            {
                _context = context;
            }

            public MessageManager()
            {
            }

            public async Task<ActionResult<IEnumerable<Message>>> GetAllAsync()
            {
                return new ActionResult<IEnumerable<Message>>(await _context.Messages.ToListAsync());
            }

            public async Task<ActionResult<Message>> GetByIdsAsync(int UserId, int ConversationId)
            {
                var message = await _context.Messages.FirstOrDefaultAsync(s => s.UserId == UserId && s.ConversationId == ConversationId);
                if (message == null) return new NotFoundResult();
                return new ActionResult<Message>(message);
            }
            public Task<ActionResult<Message>> GetByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public async Task AddAsync(Message entity)
            {
                await _context.Messages.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Message entityToUpdate, Message entity)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(Message entity)
            {
                _context.Messages.Remove(entity);
                await _context.SaveChangesAsync();
            }

            public Task<ActionResult<Message>> GetBySpecialIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Message>> GetByStringAsync(string value)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<Message>> GetByIdsAsync(int? id1, int? id2)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<IEnumerable<Message>>> GetAllByIdsAsync(int? conversationId, int? userId)
            {
                throw new NotImplementedException();
            }

            public Task<ActionResult<bool>> ExistsByIds(int id1, int id2)
            {
                throw new NotImplementedException();
            }
            public async Task<ActionResult<IEnumerable<Message>>> GetAllBySpecialIdAsync(int conversationId)
            {
                var messages = await _context.Messages.Where(m => m.ConversationId == conversationId).ToListAsync();

                if (messages == null)
                {
                    return new NotFoundResult();
                }

                return new ActionResult<IEnumerable<Message>>(messages);
            }

            public async Task<ActionResult<IEnumerable<Message>>> GetNewMessagesAsync(int conversationId, DateTime since)
            {
                var messages = await _context.Messages
                                            .Where(m => m.ConversationId == conversationId && m.Created > since)
                                            .ToListAsync();

                if (messages == null)
                {
                    return new NotFoundResult();
                }

                return new ActionResult<IEnumerable<Message>>(messages);
            }
            public async Task<ActionResult<IEnumerable<Message>>> GetMessagesPagedAsync(int conversationId, int pageIndex, int pageSize, DateTime? beforeDate = null)
            {
                var query = _context.Messages
                        .Where(m => m.ConversationId == conversationId);

                if (beforeDate.HasValue)
                {
                    query = query.Where(m => m.Created <= beforeDate.Value);
                }

                var messages = await query
                                    .OrderByDescending(m => m.Created)
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
                if (messages == null)
                {
                    return new NotFoundResult();
                }

                return new ActionResult<IEnumerable<Message>>(messages);
            }
        }
    }
}
