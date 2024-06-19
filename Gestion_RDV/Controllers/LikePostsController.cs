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

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikePostsController : ControllerBase
    {
        private readonly IDataRepository<LikePost> dataRepository;
        private readonly IMapper _mapper;
        //private readonly IDataRepositoryUser<User> dataRepositoryUser;


        public LikePostsController(IDataRepository<LikePost> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{postId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<LikePost>> GetLikePostById(int userId, int postId)
        {
            var like = await dataRepository.GetByIdsAsync(userId, postId);

            if (like == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LikePost>(like.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LikePost>> PostLikePost(LikePost likePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(likePost);

            return CreatedAtAction(nameof(GetLikePostById), new { userId = likePost.UserId, postId = likePost.PostId }, likePost);
        }
    }
}
