using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DTO;
using AutoMapper;
using Microsoft.Extensions.Hosting;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IDataRepository<Post> dataRepository;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IDataRepository<LikePost> dataRepositoryLikePost;
        private readonly IMapper _mapper;


        public PostsController(IDataRepository<Post> dataRepo, IDataRepository<User> dataRepositoryUser, IMapper mapper, IDataRepository<LikePost> dataRepoLikePost)
        {
            dataRepository = dataRepo;
            this.dataRepositoryUser = dataRepositoryUser;
            _mapper = mapper;
            dataRepositoryLikePost = dataRepoLikePost;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await dataRepository.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();
            await dataRepositoryLikePost.GetAllAsync();
            var filteredPosts = posts.Value.Where(post => !post.ParentPostId.HasValue).ToList();
            if (filteredPosts == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PostDTO>>(filteredPosts));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await dataRepository.GetByIdAsync(id);
            await dataRepositoryUser.GetByIdAsync(post.Value.UserId);
            await dataRepository.GetAllBySpecialIdAsync(id);
            await dataRepositoryLikePost.GetAllAsync();

            if (post == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PostDetailDTO>(post.Value));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PostDTO>> PostPost(PostPostDTO post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Post post1 = _mapper.Map<Post>(post);
            await dataRepository.AddAsync(post1);


            return CreatedAtAction(nameof(GetPostById), new { id = post1.PostId }, _mapper.Map<PostDTO>(post1));
        }
    }
}
