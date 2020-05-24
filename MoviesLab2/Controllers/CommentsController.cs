using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesLab2.Models;

namespace MoviesLab2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly MoviesDbContext _context;

        public CommentsController(MoviesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of all comments;
        /// </summary>
        /// <returns>A list of Comment objects</returns>

        // GET: api/Comments
        [HttpGet]
      public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }



        /// <summary>
        /// Gets a specific comment.
        /// </summary>
        /// <param name="id">The id of the comment you want to return.</param>
        /// <returns>The comment with the id you gave.</returns>
        ///<response code= "201">Returns specific comment</response>
        ///<response code="404">Not found, if the param id does not exist.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        /// <summary>
        /// Updates a comment.
        /// </summary>
        /// <param name="id">Edit the id of the comment you want to update </param>
        /// <param name="comment">Enter the new name of the comment</param>
        /// <returns>The updated comment.</returns>

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(long id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creats a Comment object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "author": "First author",
        ///         "content": "something",
        ///         "important": true,
        ///         "movieId": 1
        ///      } 
        /// </remarks>
        /// <returns> A newly created comment</returns>
        /// <response code="201">Returns the newly created comment.</response>
        /// <response code = "400">If comments's maximum length is greater than 20. or
        ///                        If author minimum length is lower than 2. </response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }


        /// <summary>
        /// Delets a specific Comment object
        /// </summary>
        /// <param name="id">The id of comment you want to delete</param>
        ///<response code ="200">The comment was succesfully deleted.</response>
        ///<response code = "400">The comment was not succesfully deleted.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        private bool CommentExists(long id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
