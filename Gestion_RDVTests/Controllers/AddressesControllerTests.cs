using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gestion_RDV.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gestion_RDV.Models.EntityFramework;
using Gestion_RDV.Models.Repository;
using Gestion_RDV.Models.DataManager.API_Gymbrodyssey.Models.DataManager;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Gestion_RDV.AutoMapper;
using Moq;

namespace Gestion_RDV.Controllers.Tests
{
    [TestClass()]
    public class AddressesControllerTests
    {
        private AddressesController _controller;
        private GestionRdvDbContext _context;
        private readonly IMapper _mapper;
        private IDataRepository<Address> dataRepository;

        public AddressesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<GestionRdvDbContext>().UseNpgsql("Server=localhost;port=5432;Database=GestionRDV; uid=postgres; password=postgres");
            _context = new GestionRdvDbContext(builder.Options);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            dataRepository = new AddressManager(_context);
            _controller = new AddressesController(dataRepository, _mapper);
        }


        [TestMethod]
        public async Task GetAdresseById_ExistingId_ReturnsCorrectItem()
        {
            // Act
            ActionResult<AddressDTO> actionResult = await _controller.GetAdresseById(1);

            // Assert
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);

            Assert.AreEqual(okResult.Value as AddressDTO, _mapper.Map<AddressDTO>(_context.Addresses.Where(c => c.AdresseId == 1).FirstOrDefault()), "Adresse différent");
        }

        [TestMethod()]
        public async Task GetAdresseById_NonExistingId_ReturnsNotFoundResult()
        {
            // Act
            ActionResult<AddressDTO> actionResult = await _controller.GetAdresseById(0);

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }



        [TestMethod]
        public async Task GetAddressById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Address adresse = new Address
            {
                AdresseId = 1,
                Adresse = "Rue de la paix",
                Ville = "Paris",
                CodePostal = 75000
            };
            var mockRepository = new Mock<IDataRepository<Address>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(_mapper.Map<Address>(adresse));
            var AddressController = new AddressesController(mockRepository.Object, _mapper);
            // Act
            var actionResult = AddressController.GetAdresseById(1).Result.Result as OkObjectResult;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(_mapper.Map<AddressDTO>(adresse), actionResult.Value as AddressDTO);
        }

        [TestMethod]
        public void GetAdresseById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Address>>();
            var AddressController = new AddressesController(mockRepository.Object, _mapper);
            // Act
            var actionResult = AddressController.GetAdresseById(1).Result.Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task PostAdresse_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Address>>();
            var addressController = new AddressesController(mockRepository.Object, _mapper);

            AddressPostDTO addressPostDTO = new AddressPostDTO
            {
                Adresse = "Rue de la Paix",
                Ville = "Paris",
                CodePostal = 75000
            };

            // Act
            var actionResult = await addressController.PostAdresse(addressPostDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AddressDTO>), "Pas un ActionResult<AddressDTO>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(AddressDTO), "Pas un AddressDTO");

            //var addressDTO = result.Value as AddressPostDTO;
            Assert.AreEqual(addressPostDTO, _mapper.Map<AddressPostDTO>(result.Value), "Adresse pas identiques");
        }
        [TestMethod]
        public async Task DeleteAdresse_ExistingId_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Address>>();
            var addressController = new AddressesController(mockRepository.Object, _mapper);

            Address address = new Address
            {
                AdresseId = 1,
                Adresse = "Rue de la Paix",
                Ville = "Paris",
                CodePostal = 75000
            };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(address);
            mockRepository.Setup(repo => repo.DeleteAsync(address)).Returns(Task.CompletedTask);

            // Act
            var actionResult = await addressController.DeleteAdresse(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            mockRepository.Verify(repo => repo.DeleteAsync(address), Times.Once);
        }
        [TestMethod]
        public async Task DeleteAdresse_NonExistingId_ReturnsNotFound_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Address>>();
            var addressController = new AddressesController(mockRepository.Object, _mapper);

            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Address)null);

            // Act
            var actionResult = await addressController.DeleteAdresse(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult");
            mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Address>()), Times.Never);
        }

        [TestMethod]
        public async Task PutAddress_ValidIdAndModel_ReturnsNoContent_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Address>>();
            var addressController = new AddressesController(mockRepository.Object, _mapper);

            AddressDTO addressDTO = new AddressDTO
            {
                AdresseId = 1,
                Adresse = "Rue de la Paix",
                Ville = "Paris",
                CodePostal = 75000
            };

            Address address = new Address
            {
                AdresseId = 1,
                Adresse = addressDTO.Adresse,
                Ville = addressDTO.Ville,
                CodePostal = addressDTO.CodePostal
            };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(address);
            mockRepository.Setup(repo => repo.UpdateAsync(address, It.IsAny<Address>())).Returns(Task.CompletedTask);

            // Act
            var actionResult = await addressController.PutAddress(1, addressDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
            mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            mockRepository.Verify(repo => repo.UpdateAsync(address, It.IsAny<Address>()), Times.Once);
        }
        [TestMethod]
        public async Task PutAddress_IdMismatch_ReturnsBadRequest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Address>>();
            var addressController = new AddressesController(mockRepository.Object, _mapper);

            AddressDTO addressDTO = new AddressDTO
            {
                AdresseId = 1,
                Adresse = "Rue de la Paix",
                Ville = "Paris",
                CodePostal = 75000
            };

            // Act
            var actionResult = await addressController.PutAddress(2, addressDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Pas un BadRequestResult");
            mockRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Address>(), It.IsAny<Address>()), Times.Never);
        }
        [TestMethod]
        public async Task PutAddress_NonExistingId_ReturnsNotFound_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Address>>();
            var addressController = new AddressesController(mockRepository.Object, _mapper);

            AddressDTO addressDTO = new AddressDTO
            {
                AdresseId = 1,
                Adresse = "Rue de la Paix",
                Ville = "Paris",
                CodePostal = 75000
            };

            mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Address)null);

            // Act
            var actionResult = await addressController.PutAddress(1, addressDTO);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Pas un NotFoundResult");
            mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Address>(), It.IsAny<Address>()), Times.Never);
        }
    }
}