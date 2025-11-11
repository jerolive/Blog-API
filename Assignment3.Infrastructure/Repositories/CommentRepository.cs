using Assignment3.Core.Interfaces;
using Assignment3.Core.Models;
using Assignment3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int id)
        {
            return await _context.Comments
                .Where(c => c.PostId == id)
                .ToListAsync();
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            comment.CreatedDate = DateTime.UtcNow;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(comment.Id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Name = comment.Name;
            existingComment.Email = comment.Email;
            existingComment.Content = comment.Content;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return false;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }
    }
}