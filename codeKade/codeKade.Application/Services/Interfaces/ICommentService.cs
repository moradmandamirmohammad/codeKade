using codeKade.DataLayer.Entities.Comment;

namespace codeKade.Application.Services.Interfaces;

public interface ICommentService : IAsyncDisposable
{
    Task<List<Comment>> GetEventComments(long eventId);
}