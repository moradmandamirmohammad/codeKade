using codeKade.DataLayer.DTOs.Comment;
using codeKade.DataLayer.Entities.Comment;

namespace codeKade.Application.Services.Interfaces;

public interface IBlogCommentService : IAsyncDisposable
{
    Task<List<BlogComment>> GetBlogComments(long blogId);

    Task AddComment(AddBlogCommentDTO add);

    Task<int> CountOfTodayComments();

    Task<int> CountOfComments();
}