using codeKade.DataLayer.DTOs.Blog;
using codeKade.DataLayer.Entities.Blog;

namespace codeKade.Application.Services.Interfaces;

public interface IBlogService : IAsyncDisposable
{
    Task<List<Blog>> GetNewBlogs();

    Task<FilterBlogDTO> GetAll(FilterBlogDTO filter);

    Task<Blog> GetBlogDetail(long id);

    Task<List<BlogCategory>> GetCategories();

    Task<List<Blog>> GetMostSeenBlog();

    Task AddSeenToBlog(long id);

    Task<int> CountOfTodayBlog();

    Task<int> CountOfBlogs();
}