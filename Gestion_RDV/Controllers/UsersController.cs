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
            private readonly IDataRepository<MedicalInfo> dataRepositoryMedicalInfo;
            private readonly IDataRepository<Diagnosis> dataRepositoryDiagnosis;
            private readonly IDataRepository<Prescription> dataRepositoryPrescription;
            private readonly IDataRepository<Medication> dataRepositoryMedication;
            private readonly IMapper _mapper;


            public UsersController(IDataRepository<User> dataRepo, IMapper mapper, IDataRepository<MedicalInfo> dataRepoMedicalInfo, IDataRepository<Diagnosis> dataRepoyDiagnosis, IDataRepository<Prescription> dataRepoPrescription, IDataRepository<Medication> dataRepoyMedication)
            {
                dataRepository = dataRepo;
                _mapper = mapper;
                dataRepositoryMedicalInfo = dataRepoMedicalInfo;
                dataRepositoryDiagnosis = dataRepoyDiagnosis;
                dataRepositoryPrescription = dataRepoPrescription;
                dataRepositoryMedication = dataRepoyMedication;
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
            [HttpGet("medicalInfo/{id}")]
            [ProducesResponseType(200)]
            [ProducesResponseType(404)]
            public async Task<ActionResult<UserMedicalDetailDTO>> GetMedicalUserInoById(int id)
            {
                var user = await dataRepository.GetByIdAsync(id);
                await dataRepositoryMedicalInfo.GetAllBySpecialIdAsync(id);
                await dataRepositoryDiagnosis.GetAllAsync();
                await dataRepositoryPrescription.GetAllAsync();
                await dataRepositoryMedication.GetAllAsync();

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<UserMedicalDetailDTO>(user.Value)); ;
            }
        }
    }
}
