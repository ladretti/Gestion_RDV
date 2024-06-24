using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using AutoMapper;
using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.Repository;
using System.ComponentModel.Design;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IDataRepository<Comment> dataRepository;
        private readonly IMapper _mapper;

        public CommentsController(IDataRepository<Comment> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CommentDTO>> GetCommentById(int commentId)
        {
            var like = await dataRepository.GetByIdAsync(commentId);

            if (like.Value == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommentDTO>(like.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDTO>> PostComment(CommentPostDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment comment1 = _mapper.Map<Comment>(comment);
            await dataRepository.AddAsync(comment1);

            return CreatedAtAction(nameof(GetCommentById), new { commentId = comment1.CommentId }, _mapper.Map<CommentDTO>(comment1));
        }

        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await dataRepository.GetByIdAsync(commentId);
            if (comment.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(comment.Value);

            return NoContent();
        }
    }
}
