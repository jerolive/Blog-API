using Assignment3.Core.Models;

namespace Assignment3.Core.Interfaces
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetAllCommentsAsync();
        public Task<Comment?> GetCommentByIdAsync(int id);
        public Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);

        public Task<Comment> CreateCommentAsync(Comment comment);
        public Task<Comment?> UpdateCommentAsync(Comment comment);
        public Task<bool> DeleteCommentAsync(int id);
    }
}