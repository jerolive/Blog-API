using Assignment3.Core.Models;

namespace Assignment3.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        
        Task<Post> CreatePostAsync(Post post);
        Task<Post?> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
    }
}