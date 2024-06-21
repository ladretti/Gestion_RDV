using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Gestion_RDV.Models.DTO;
using AutoMapper;

namespace Gestion_RDV.Controllers
{
    namespace API_Gymbrodyssey.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            private readonly IDataRepository<User> dataRepository;
            private readonly IMapper _mapper;


            public UsersController(IDataRepository<User> dataRepo, IMapper mapper)
            {
                dataRepository = dataRepo;
                _mapper = mapper; 
            }


            [HttpGet("{id}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public async Task<ActionResult<UserDetailDTO>> GetUserById(int id)
            {
                var user = await dataRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<UserDetailDTO>(user.Value)); ;
            }
        }
    }
}
