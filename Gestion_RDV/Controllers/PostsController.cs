using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IDataRepository<Post> dataRepository;
        private readonly IDataRepositoryUser<User> dataRepositoryUser;


        public PostsController(IDataRepository<Post> dataRepo, IDataRepositoryUser<User> dataRepositoryUser)
        {
            dataRepository = dataRepo;
            this.dataRepositoryUser = dataRepositoryUser;
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
            return posts;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var office = await dataRepository.GetByIdAsync(id);
            await dataRepositoryUser.GetByIdAsync(office.Value.UserId);

            if (office == null)
            {
                return NotFound();
            }

            return office;
        }
    }
}
