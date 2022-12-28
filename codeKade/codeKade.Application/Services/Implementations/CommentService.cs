using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.Entities.Comment;
using codeKade.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace codeKade.Application.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _commentRepository;

        public CommentService(IGenericRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async ValueTask DisposeAsync()
        {
            if (_commentRepository != null)
            {
                await _commentRepository.DisposeAsync();
            }
        }

        public async Task<List<Comment>> GetEventComments(long eventId)
        {
            return await _commentRepository.GetEntityQuery().Where(c => c.EventId == eventId).Include(c=>c.User).ToListAsync();
        }

        public async Task<bool> AddComment(string text, long userId, long eventId,long? parentId)
        {
            if (parentId != null)
            {
                var comment = new Comment()
                {
                    Text = text,
                    UserId = userId,
                    EventId = eventId,
                    IsDelete = false,
                    ParentId = parentId
                };
                await _commentRepository.AddEntity(comment);
                await _commentRepository.SaveChanges();
            }
            else
            {
                var comment = new Comment()
                {
                    Text = text,
                    UserId = userId,
                    EventId = eventId,
                    IsDelete = false,
                };
                await _commentRepository.AddEntity(comment);
                await _commentRepository.SaveChanges();
            }
            
            return true;
        }

        public async Task<List<Comment>> GetIndexComments()
        {
            return await _commentRepository.GetEntityQuery().Where(c => c.ShowInIndex).Include(c=>c.User).ToListAsync();
        }

        public async Task<int> CountOfComments()
        {
            return await _commentRepository.GetEntityQuery().CountAsync();
        }

        public async Task<int> CountOfTodayComments()
        {
            return await _commentRepository.GetEntityQuery().CountAsync(c => c.CreateDate.Date == DateTime.Now.Date);
        }
    }
}
