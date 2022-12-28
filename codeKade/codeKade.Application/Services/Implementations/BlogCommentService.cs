using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Blog;
using codeKade.DataLayer.DTOs.Comment;
using codeKade.DataLayer.Entities.Blog;
using codeKade.DataLayer.Entities.Comment;
using codeKade.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace codeKade.Application.Services.Implementations
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IGenericRepository<BlogComment> _blogCommentRepository;

        public BlogCommentService(IGenericRepository<BlogComment> blogCommentRepository)
        {
            _blogCommentRepository = blogCommentRepository;
        }


        public async ValueTask DisposeAsync()
        {
            if (_blogCommentRepository != null)
            {
                await _blogCommentRepository.DisposeAsync();
            }
        }

        public async Task<List<BlogComment>> GetBlogComments(long blogId)
        {
            return await _blogCommentRepository.GetEntityQuery().Where(bc => bc.BlogId == blogId).Include(bc=>bc.User).ToListAsync();
        }

        public async Task AddComment(AddBlogCommentDTO add)
        {
            if (add.ParentId != null)
            {
                var blogcomment = new BlogComment()
                {
                    BlogId = add.BLogId,
                    UserId = add.UserId,
                    Text = add.Text,
                    IsDelete = false,
                    ParentId = add.ParentId
                };
                await _blogCommentRepository.AddEntity(blogcomment);
                await _blogCommentRepository.SaveChanges();
            }
            else
            {
                var blogcomment = new BlogComment()
                {
                    BlogId = add.BLogId,
                    UserId = add.UserId,
                    Text = add.Text,
                    IsDelete = false,
                };
                await _blogCommentRepository.AddEntity(blogcomment);
                await _blogCommentRepository.SaveChanges();
            }
        }

        public async Task<int> CountOfTodayComments()
        { 
            return await _blogCommentRepository.GetEntityQuery().CountAsync(bc => bc.CreateDate.Date == DateTime.Now.Date);
        }

        public async Task<int> CountOfComments()
        {
            return await _blogCommentRepository.GetEntityQuery().CountAsync();

        }
    }
}
