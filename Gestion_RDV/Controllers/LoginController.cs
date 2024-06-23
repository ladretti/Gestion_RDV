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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gestion_RDV.Models.DTO;
using Microsoft.CodeAnalysis.Scripting;
using AutoMapper;
using Npgsql;
using System.Data;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IDataRepository<User> dataRepository;
        private readonly IDataRepository<Office> dataRepositoryOffice;

        public LoginController(IConfiguration config, IDataRepository<User> dataRepo, IMapper mapper, IDataRepository<Office> dataRepoOffice)
        {
            _config = config;
            dataRepository = dataRepo;
            _mapper = mapper;
            dataRepositoryOffice = dataRepoOffice;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> SignIn(UserSignInDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            try
            {
                await dataRepository.AddAsync(_mapper.Map<User>(user));
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolationException(ex))
                {
                    ModelState.AddModelError("Email", "L'email est déjà utilisé");
                    return BadRequest(ModelState);
                }
                else
                {
                    // Log or handle other exceptions
                    return BadRequest("Impossible de créer l'utilisateur");
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login(UserLoginParametersDTO infos)
        {
            IActionResult response = Unauthorized();
            var user = dataRepository.GetByStringAsync(infos.Email).Result.Value;
            dataRepositoryOffice.GetAllAsync();
            if (user == null)
            {
                return Unauthorized("Utilisateur non trouvé.");
            }
            bool validPassword = BCrypt.Net.BCrypt.Verify(infos.Password, user.Password);

            if (!validPassword)
            {
                return Unauthorized("Mot de passe invalide.");
            }

            var token = GenerateJwtToken(user);
            object userDetailsDto;

            switch (user.Role)
            {
                case UserRole.Practitioner:
                    userDetailsDto = _mapper.Map<PractitionerLoginDTO>(user);
                    break;
                default:
                    userDetailsDto = _mapper.Map<UserLoginDTO>(user);
                    break;
            }

            return Ok(new
            {
                token = token,
                userDetails = userDetailsDto
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private bool IsUniqueConstraintViolationException(DbUpdateException ex)
        {
            var innerException = ex.InnerException;
            while (innerException != null)
            {
                if (innerException is PostgresException postgresException)
                {
                    if (postgresException.SqlState == "23505") // Unique violation error code for PostgreSQL
                    {
                        return true;
                    }
                }
                innerException = innerException.InnerException;
            }
            return false;
        }
    }
}