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

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataRepositoryUser<User> dataRepository;

        public LoginController(IConfiguration config, IDataRepositoryUser<User> dataRepo)
        {
            _config = config;
            dataRepository = dataRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> SignIn(String email, String password, DateTime birthDate, string firstName, string lastName , string role )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User() { Email = email, Password = password, BirthDate = birthDate, FirstName = firstName, LastName = lastName, Role = role};
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await dataRepository.AddAsync(user);

            return null;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login(String email, String mdp)
        {
            IActionResult response = Unauthorized();
            var user = dataRepository.GetByStringAsync(email).Result.Value;
            if (user == null)
            {
                return Unauthorized("Utilisateur non trouvé.");
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(mdp, user.Password);

            if (!validPassword)
            {
                return Unauthorized("Mot de passe invalide.");
            }

            var token = GenerateJwtToken(user);
            if (user != null)
            {
                var tokenString = GenerateJwtToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, user.Email), new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()) };

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
    }
}