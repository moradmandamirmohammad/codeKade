﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Blog;
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

        public async Task<FilterBlogDTO> GetAll(FilterBlogDTO filter)
        {
            var query = _blogRepository.GetEntityQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));
            }


            filter.SkipEntity = (filter.PageID - 1) * filter.TakeEntity;
            var entitiesCount = await query.CountAsync();
            filter.PageCount = (int)Math.Ceiling(entitiesCount / (double)filter.TakeEntity);

            var products = await query.OrderBy(s => s.ID).Skip(filter.SkipEntity).Take(filter.TakeEntity)
                .ToListAsync();
            filter.StartPage = filter.PageID - 3 > 0 ? filter.PageID - 3 : 1;
            filter.EndPage = filter.PageID + 3 <= filter.PageCount ? filter.PageID + 3 : filter.PageCount;
            filter.Blogs = products;
            return filter;
        }
    }
}