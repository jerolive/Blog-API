using Assignment3.Core.DTOs;
using Assignment3.Core.Interfaces;
using Assignment3.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repository;

        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            var posts = await _repository.GetAllPostsAsync();

            return Ok(posts);
        }

        // GET: api/posts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _repository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound(new { Message = $"Post with ID {id} not found" });
            }

            return Ok(post);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] PostCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = "admin", // Hardcoded
                CreatedDate = DateTime.UtcNow
            };

            var createdPost = await _repository.CreatePostAsync(post);

            return CreatedAtAction(
                    nameof(GetPostById),
                    new { id = createdPost.Id },
                    createdPost
            );
        }

        // PUT: api/posts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exists = await _repository.ExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { Message = $"Post with ID {id} not found" });
            }

            var post = new Post
            {
                Id = id,
                Title = dto.Title,
                Content = dto.Content,
                UpdatedDate = DateTime.UtcNow
            };

            var updatedPost = await _repository.UpdatePostAsync(post);

            return Ok(updatedPost);
        }

        // PATCH: api/posts/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Post> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new { Message = "Patch document is null" });
            }

            var post = await _repository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound(new { Message = $"Post with ID {id} not found" });
            }

            patchDoc.ApplyTo(post, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.UpdatedDate = DateTime.UtcNow;
            await _repository.UpdatePostAsync(post);

            return Ok(post);
        }

        // DELETE: api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var deleted = await _repository.DeletePostAsync(id);
            if (!deleted)
            {
                return NotFound(new { Message = $"Post with ID {id} not found" });
            }

            return NoContent();
        }
    }
}