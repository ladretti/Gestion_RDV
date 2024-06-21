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
    public class ReviewsController : ControllerBase
    {
        private readonly IDataRepository<Review> dataRepository;
        private readonly IDataRepository<RendezVous> dataRepositoryRdv;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IDataRepository<Comment> dataRepositoryComment;
        private readonly IDataRepository<LikeReview> dataRepositoryLikeReview;

        private readonly IMapper _mapper;

        public ReviewsController(IDataRepository<Review> dataRepo, IMapper mapper, IDataRepository<RendezVous> dataRepoRdv, IDataRepository<User> dataRepoUser, IDataRepository<Comment> dataRepoComment, IDataRepository<LikeReview> dataRepoLikeReview)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
            dataRepositoryRdv = dataRepoRdv;
            dataRepositoryUser = dataRepoUser;
            dataRepositoryComment = dataRepoComment;
            dataRepositoryLikeReview = dataRepoLikeReview;
        }

        [HttpGet("{officeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewByOfficeId(int officeId)
        {
            var reviews = await dataRepository.GetAllAsync();
            await dataRepositoryRdv.GetAllBySpecialIdAsync(officeId);
            await dataRepositoryComment.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();
            await dataRepositoryLikeReview.GetAllAsync();
            reviews = reviews.Value.Where(review => review.RendezVous != null && review.RendezVous.OfficeId == officeId).ToList();


            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ReviewDTO>>(reviews.Value));
        }
        [HttpGet("review/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewDTO>> GetReviewById(int id)
        {
            var reviews = await dataRepository.GetByIdAsync(id);

            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReviewDTO>(reviews.Value));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(review);

            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewId }, review); 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await dataRepository.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(review.Value);

            return NoContent();
        }
    }
}
