using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIContentService.Application.Controllers;
using WebAPIContentService.Domain.DTOs.Responses;
using WebAPIContentService.Domain.DTOs.ViewModels;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Domain.Enumerations;
using WebAPIContentService.Service.Interfaces;
using Xunit;

namespace WebAPIContentService.Tests.Controllers
{
    public class MaterialUsuarioControllerTests
    {
        private readonly MaterialUsuarioController _materialUsuarioController;
        private readonly IMaterialUsuarioService _materialUsuarioService;
        private readonly IMapper _mapper;

        public MaterialUsuarioControllerTests()
        {
            _materialUsuarioService = A.Fake<IMaterialUsuarioService>();
            _mapper = A.Fake<IMapper>();

            _materialUsuarioController = new MaterialUsuarioController(_materialUsuarioService, _mapper);
        }

        [Fact]
        public async Task GetMateriaisUsuario_ReturnOk()
        {
            // Arrange
            int idUsuario = 1;
            IList<MaterialUsuario> fakeMateriaisUsuario = A.CollectionOfDummy<MaterialUsuario>(2);
            IList<MaterialUsuarioDto> fakeMateriaisUsuarioDto = A.CollectionOfDummy<MaterialUsuarioDto>(fakeMateriaisUsuario.Count);

            A.CallTo(() => _materialUsuarioService.GetAllMateriaisUsuarioAsync(idUsuario)).Returns(fakeMateriaisUsuario);
            A.CallTo(() => _mapper.Map<IEnumerable<MaterialUsuarioDto>>(A<IEnumerable<MaterialUsuario>>.Ignored)).Returns(fakeMateriaisUsuarioDto);

            // Act
            ActionResult<IEnumerable<MaterialUsuarioDto>> result = await _materialUsuarioController.GetMateriaisUsuario(idUsuario);

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<MaterialUsuarioDto>>>();

            ActionResult<IEnumerable<MaterialUsuarioDto>> actionResult = result.As<ActionResult<IEnumerable<MaterialUsuarioDto>>>();
            actionResult.Result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = actionResult.Result.As<OkObjectResult>();
            okResult.Value.Should().BeAssignableTo<IEnumerable<MaterialUsuarioDto>>();

            IEnumerable<MaterialUsuarioDto> model = okResult.Value.As<IEnumerable<MaterialUsuarioDto>>();
            model.Should().HaveCount(fakeMateriaisUsuarioDto.Count);
        }

        [Fact]
        public async Task GetMaterialUsuarioById_ReturnsOk()
        {
            // Arrange
            int materialUsuarioId = 1;
            var fakeMaterialUsuario = CreateFakeMaterialUsuario(materialUsuarioId, 1, StatusEnum.Pendente);
            var fakeMaterialUsuarioDto = A.Dummy<MaterialUsuarioDto>();

            A.CallTo(() => _materialUsuarioService.GetMaterialUsuarioByIdAsync(materialUsuarioId)).Returns(fakeMaterialUsuario);
            A.CallTo(() => _mapper.Map<MaterialUsuarioDto>(fakeMaterialUsuario)).Returns(fakeMaterialUsuarioDto);

            // Act
            var result = await _materialUsuarioController.GetMaterialUsuarioById(materialUsuarioId);

            // Assert
            result.Should().BeOfType<ActionResult<MaterialUsuarioDto>>();

            var actionResult = result.As<ActionResult<MaterialUsuarioDto>>();
            actionResult.Result.Should().BeOfType<OkObjectResult>();

            var okResult = actionResult.Result.As<OkObjectResult>();
            okResult.Value.Should().BeAssignableTo<MaterialUsuarioDto>();

            var model = okResult.Value.As<MaterialUsuarioDto>();
            model.Should().BeEquivalentTo(fakeMaterialUsuarioDto);
        }

        [Fact]
        public async Task GetMaterialUsuarioById_ReturnsNotFound()
        {
            // Arrange
            int materialUsuarioId = 2;

            A.CallTo(() => _materialUsuarioService.GetMaterialUsuarioByIdAsync(materialUsuarioId)).Returns((MaterialUsuario)null!);

            // Act
            var result = await _materialUsuarioController.GetMaterialUsuarioById(materialUsuarioId);

            // Assert
            result.Should().BeOfType<ActionResult<MaterialUsuarioDto>>();

            var actionResult = result.As<ActionResult<MaterialUsuarioDto>>();
            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PostMaterialUsuario_ReturnsCreated()
        {
            // Arrange
            var fakeMaterialUsuarioAddViewModel = A.Dummy<MaterialUsuarioAddViewModel>();
            var fakeMaterialUsuario = A.Dummy<MaterialUsuario>();

            A.CallTo(() => _mapper.Map<MaterialUsuario>(fakeMaterialUsuarioAddViewModel)).Returns(fakeMaterialUsuario);

            // Act
            var result = await _materialUsuarioController.PostMaterialUsuario(fakeMaterialUsuarioAddViewModel);

            // Assert
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task PostMaterialUsuario_ReturnsBadRequest()
        {
            // Arrange
            var fakeInvalidMaterialUsuarioAddViewModel = new MaterialUsuarioAddViewModel();
            _materialUsuarioController.ModelState.AddModelError("IdUsuario", "IdUsuario Invalido"); 

            // Act
            var result = await _materialUsuarioController.PostMaterialUsuario(fakeInvalidMaterialUsuarioAddViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AlterarStatusMaterial_ReturnsOk()
        {
            // Arrange
            int materialUsuarioId = 1;

            A.CallTo(() => _materialUsuarioService.AlterarStatusMaterialUsuarioAsync(materialUsuarioId, StatusEnum.Concluido)).Returns(Task.CompletedTask);

            // Act
            var result = await _materialUsuarioController.AlterarStatusMaterial(materialUsuarioId, StatusEnum.Concluido);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task AlterarStatusMaterial_ReturnsBadRequest_WhenIdInvalid()
        {
            // Arrange
            int invalidId = 0;
            var invalidStatus = StatusEnum.Pendente;

            // Act
            var result = await _materialUsuarioController.AlterarStatusMaterial(invalidId, invalidStatus);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("ID de material de usuario invalido");
        }

        [Fact]
        public async Task AlterarStatusMaterial_ReturnsBadRequest_WhenStatusIsInvalid()
        {
            // Arrange
            int validId = 1;
            StatusEnum invalidStatus = (StatusEnum)100; // Define um status invalido

            // Act
            var result = await _materialUsuarioController.AlterarStatusMaterial(validId, invalidStatus);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Status invalido");
        }

        [Fact]
        public async Task AlterarStatusMaterial_ReturnsNotFound()
        {
            // Arrange
            int materialUsuarioId = 2;

            A.CallTo(() => _materialUsuarioService.AlterarStatusMaterialUsuarioAsync(materialUsuarioId, StatusEnum.Concluido)).Throws<Exception>();

            // Act
            var result = await _materialUsuarioController.AlterarStatusMaterial(materialUsuarioId, StatusEnum.Concluido);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        private static MaterialUsuario CreateFakeMaterialUsuario(int idMaterial, int idUsuario, StatusEnum status)
        {
            return new MaterialUsuario
            {
                IdMaterial = idMaterial,
                IdUsuario = idUsuario,
                Status = status,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
        }
    }
}