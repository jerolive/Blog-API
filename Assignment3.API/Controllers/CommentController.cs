using Assignment3.Core.DTOs;
using Assignment3.Core.Interfaces;
using Assignment3.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;

        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            var comments = await _repository.GetAllCommentsAsync();

            return Ok(comments);
        }

        // GET: api/comment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _repository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new { Message = $"Comment with ID {id} not found" });
            }

            return Ok(comment);
        }

        // GET api/post/{postId}/comment
        [HttpGet("/api/post/{postId}/comment")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPostId(int postId)
        {
            var comments = await _repository.GetCommentsByPostIdAsync(postId);

            return Ok(comments);
        }

        // POST: api/post/{postId}/comment
        [HttpPost("/api/post/{postId}/comment")]
        public async Task<ActionResult<Comment>> CreateComment(int postId, [FromBody] CommentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = new Comment
            {
                PostId = postId,
                Name = dto.Name,
                Email = dto.Email,
                Content = dto.Content,
                CreatedDate = DateTime.UtcNow
            };

            var createdComment = await _repository.CreateCommentAsync(comment);

            return CreatedAtAction(
                    nameof(GetCommentById),
                    new { id = createdComment.Id },
                    createdComment
            );
        }

        // PUT: api/comment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = await _repository.ExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { Message = $"Comment with ID {id} not found" });
            }

            var comment = new Comment
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Content = dto.Content
            };

            var updatedComment = await _repository.UpdateCommentAsync(comment);

            return Ok(updatedComment);
        }

        // PATCH: api/comment/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchComment(int id, [FromBody] JsonPatchDocument<Comment> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new { Message = "Patch document is null" });
            }

            var comment = await _repository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound(new { Message = $"Comment with ID {id} not found" });
            }

            patchDoc.ApplyTo(comment, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.UpdateCommentAsync(comment);

            return Ok(comment);
        }

        // DELETE: api/comment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var deleted = await _repository.DeleteCommentAsync(id);
            if (!deleted)
            {
                return NotFound(new { Message = $"Comment with ID {id} not found" });
            }

            return NoContent();
        }
    }
}