using System.ComponentModel.DataAnnotations;

namespace Assignment3.Core.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = string.Empty;

        // Hardcoded
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; } = "admin";

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }


        // Navigation property for comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}