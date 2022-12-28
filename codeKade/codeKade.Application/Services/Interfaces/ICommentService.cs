using codeKade.DataLayer.Entities.Comment;

namespace codeKade.Application.Services.Interfaces;

public interface ICommentService : IAsyncDisposable
{
    Task<List<Comment>> GetEventComments(long eventId);

    Task<bool> AddComment(string text, long userId, long eventId,long? parentId);

    Task<List<Comment>> GetIndexComments();

    Task<int> CountOfComments();

    Task<int> CountOfTodayComments();
}