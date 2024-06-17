﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_RDV.Controllers
{
    namespace API_Gymbrodyssey.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            private readonly IDataRepository<User> dataRepository;

            public UsersController(IDataRepository<User> dataRepo)
            {
                dataRepository = dataRepo;
            }

            /// <summary>
            /// Renvoie les informations utilisateur pour les utilisateurs ayant le rôle 'User'
            /// </summary>
            [HttpGet]
            [Route("GetUserData")]
            [Authorize(Policy = Policies.User)]
            public IActionResult GetUserData()
            {
                return Ok("This is a response from user method");
            }

            /// <summary>
            /// Renvoie les informations administrateur pour les utilisateurs ayant le rôle 'Admin'
            /// </summary>
            [HttpGet]
            [Route("GetAdminData")]
            [Authorize(Policy = Policies.Admin)]
            public IActionResult GetAdminData()
            {
                return Ok("This is a response from Admin method");
            }

            /// <summary>
            /// Renvoie tous les utilisateurs
            /// </summary>
            /// <returns>Une liste des utilisateurs</returns>
            [HttpGet]
            [Authorize(Policy = Policies.Admin)]
            [ProducesResponseType(200)]
            public async Task<ActionResult<IEnumerable<User>>> GetUsers()
            {
                var users = await dataRepository.GetAllAsync();

                if (users == null)
                {
                    return NotFound();
                }
                return users;
            }

            /// <summary>
            /// Renvoie un utilisateur par son identifiant
            /// </summary>
            /// <param name="id">Identifiant de l'utilisateur</param>
            /// <returns>L'utilisateur correspondant à l'identifiant donné</returns>
            [HttpGet("{id}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public async Task<ActionResult<User>> GetUserById(int id)
            {
                var user = await dataRepository.GetByIdAsync(id);
                await dataRepositoryPersonne.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }

            /// <summary>
            /// Renvoie un utilisateur par son pseudo
            /// </summary>
            /// <param name="pseudo">Pseudo de l'utilisateur</param>
            /// <returns>L'utilisateur correspondant au pseudo donné</returns>
            [HttpGet("{pseudo}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public async Task<ActionResult<User>> GetUserByPseudo(string pseudo)
            {
                var user = await dataRepository.GetByStringAsync(pseudo);
                await dataRepositoryPersonne.GetAllAsync();

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }

            /// <summary>
            /// Met à jour un utilisateur existant
            /// </summary>
            /// <param name="id">Identifiant de l'utilisateur à mettre à jour</param>
            /// <param name="user">Nouvelles informations pour l'utilisateur</param>
            /// <returns>Une réponse 'NoContent' si l'utilisateur est mis à jour avec succès</returns>

            [HttpPut("{id}")]
            [ProducesResponseType(201)]
            [ProducesResponseType(400)]
            public async Task<IActionResult> PutUser(int id, User user)
            {
                if (id != user.PersonneId)
                {
                    return BadRequest();
                }

                var userToUpdate = await dataRepository.GetByIdAsync(id);
                await dataRepositoryPersonne.GetByIdAsync(id);

                if (userToUpdate == null)
                {
                    return NotFound();
                }

                else
                {

                    await dataRepository.UpdateAsync(userToUpdate.Value, user);

                    return NoContent();
                }
            }

            /// <summary>
            /// Ajoute un nouvel utilisateur
            /// </summary>
            /// <param name="user">Informations pour le nouvel utilisateur</param>
            /// <returns>Une réponse 'CreatedAtAction' avec l'utilisateur nouvellement créé</returns>

            [HttpPost("{nom}/{mail}")]
            [ProducesResponseType(201)]
            [ProducesResponseType(400)]
            public async Task<ActionResult<User>> PostUser(User user, string nom, string mail)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await dataRepositoryPersonne.AddAsync(new Personne() { Mail = mail, Nom = nom });
                var e = await dataRepositoryPersonne.GetByStringAsync(mail);
                if (e != null)
                {
                    user.PersonneId = e.Value.PersonneId;
                    await dataRepository.AddAsync(user);
                }

                return CreatedAtAction("GetUserById", new { id = user.PersonneId }, user); // GetUserById : nom de l’action

            }

            /// <summary>
            /// Supprime un utilisateur en utilisant l'ID de l'utilisateur.
            /// </summary>
            /// <param name="id">ID de l'utilisateur à supprimer.</param>
            /// <returns>ActionResult avec un code de statut HTTP 204 No Content si la suppression est réussie, ou un code de statut HTTP 404 Not Found si l'utilisateur n'existe pas.</returns>

            [HttpDelete("{id}")]
            [ProducesResponseType(201)]
            [ProducesResponseType(400)]
            public async Task<IActionResult> DeleteUser(int id)
            {
                var user = await dataRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                await dataRepository.DeleteAsync(user.Value);
                var e = await dataRepositoryPersonne.GetByIdAsync(user.Value.PersonneId);
                if (e != null)
                    await dataRepositoryPersonne.DeleteAsync(e.Value);

                return NoContent();
            }
        }
    }
    }
