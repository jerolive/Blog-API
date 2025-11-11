using Assignment3.Core.Models;

namespace Assignment3.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);

        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment?> UpdateCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}