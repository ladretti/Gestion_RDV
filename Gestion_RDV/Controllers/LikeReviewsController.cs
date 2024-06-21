using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using AutoMapper;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DTO;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeReviewsController : ControllerBase
    {
        private readonly IDataRepository<LikeReview> dataRepository;
        private readonly IMapper _mapper;
        //private readonly IDataRepositoryUser<User> dataRepositoryUser;


        public LikeReviewsController(IDataRepository<LikeReview> dataRepo, IMapper mapper)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{postId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<LikeReview>> GetLikeReviewById(int userId, int postId)
        {
            var like = await dataRepository.GetByIdsAsync(userId, postId);

            if (like.Value == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LikeReviewDTO>(like.Value));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LikeReview>> ReviewLikeReview(LikeReviewDTO likeReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(_mapper.Map<LikeReview>(likeReview));

            return CreatedAtAction(nameof(GetLikeReviewById), new { userId = likeReview.UserId, postId = likeReview.ReviewId }, likeReview);
        }

        [HttpDelete("{userId}/{postId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLikeReview(int userId, int postId)
        {
            var like = await dataRepository.GetByIdsAsync(userId, postId);
            if (like == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(like.Value);

            return NoContent();
        }

        [HttpPut("{userId}/{reviewId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutLikeReview(int userId, int reviewId,  LikeReviewDTO likeReview)
        {
            if (userId != likeReview.UserId || reviewId != likeReview.ReviewId)
            {
                return BadRequest();
            }

            var likeReviewToUpdate = await dataRepository.GetByIdsAsync(userId, reviewId);
            if (likeReviewToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(likeReviewToUpdate.Value, _mapper.Map<LikeReview>(likeReview));
                return NoContent();
            }
        }
    }
}
