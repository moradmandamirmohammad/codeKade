using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.Entities.Blog;
using codeKade.DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace codeKade.Application.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IGenericRepository<Blog> _blogRepository;

        public BlogService(IGenericRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async ValueTask DisposeAsync()
        {
            if (_blogRepository != null)
            {
                await _blogRepository.DisposeAsync();
            }
        }

        public async Task<List<Blog>> GetNewBlogs()
        {
            return await _blogRepository.GetEntityQuery().OrderByDescending(b=>b.ID).Take(3).Include(b=>b.BlogCategory).ToListAsync();
        }
    }
}
