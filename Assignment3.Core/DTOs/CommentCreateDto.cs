using System.ComponentModel.DataAnnotations;

namespace Assignment3.Core.DTOs
{
    public class CommentCreateDto
    {
        [Required(ErrorMessage = "PostId is required")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email cannot be longer than 150 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = string.Empty;
    }
}