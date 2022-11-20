using codeKade.DataLayer.DTOs.Blog;
using codeKade.DataLayer.Entities.Blog;

namespace codeKade.Application.Services.Interfaces;

public interface IBlogService : IAsyncDisposable
{
    Task<List<Blog>> GetNewBlogs();

    Task<FilterBlogDTO> GetAll(FilterBlogDTO filter);
}