using Assignment3.Core.Interfaces;
using Assignment3.Core.Models;
using Assignment3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            post.CreatedDate = DateTime.UtcNow;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> UpdatePostAsync(Post post)
        {
            var existingPost = await _context.Posts.FindAsync(post.Id);
            if (existingPost == null)
            {
                return null;
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return existingPost;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return false;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Posts.AnyAsync(p => p.Id == id);
        }
    }
}