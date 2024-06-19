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
        private readonly IMapper _mapper;


        public PostsController(IDataRepository<Post> dataRepo, IDataRepository<User> dataRepositoryUser, IMapper mapper)
        {
            dataRepository = dataRepo;
            this.dataRepositoryUser = dataRepositoryUser;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await dataRepository.GetAllAsync();
            await dataRepositoryUser.GetAllAsync();

            if (posts == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PostDTO>>(posts.Value)); ;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await dataRepository.GetByIdAsync(id);
            await dataRepositoryUser.GetByIdAsync(post.Value.UserId);
            await dataRepository.GetAllBySpecialIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PostDetailDTO>(post.Value));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(post);

            return CreatedAtAction("GetPostById", new { id = post.PostId }, post); // GetById : nom de l’action
        }
    }
}
