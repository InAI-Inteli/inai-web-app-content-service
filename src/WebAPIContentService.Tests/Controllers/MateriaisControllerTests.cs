using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPIContentService.Application.Controllers;
using WebAPIContentService.Domain.DTOs.Responses;
using WebAPIContentService.Domain.DTOs.ViewModels;
using WebAPIContentService.Domain.Entities;
using WebAPIContentService.Service.Interfaces;
using Xunit;

namespace WebAPIContentService.Tests.Controllers
{
    public class MateriaisControllerTests
    {
        private readonly MateriaisController _materiaisController;
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public MateriaisControllerTests()
        {
            _materialService = A.Fake<IMaterialService>();
            _mapper = A.Fake<IMapper>();

            _materiaisController = new MateriaisController(_materialService, _mapper);
        }

        [Fact]
        public async void GetMateriais_ReturnOk()
        {
            // Arrange
            IList<Material> fakeMateriais = A.CollectionOfDummy<Material>(2);
            IList<MaterialDto> fakeMateriaisDto = A.CollectionOfDummy<MaterialDto>(fakeMateriais.Count);

            A.CallTo(() => _materialService.GetAllMateriaisAsync()).Returns(fakeMateriais);
            A.CallTo(() => _mapper.Map<IEnumerable<MaterialDto>>(A<IEnumerable<Material>>.Ignored)).Returns(fakeMateriaisDto);

            // Act
            ActionResult<IEnumerable<MaterialDto>> result = await _materiaisController.GetMateriais();

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<MaterialDto>>>();

            ActionResult<IEnumerable<MaterialDto>> actionResult = result.As<ActionResult<IEnumerable<MaterialDto>>>();
            actionResult.Result.Should().BeOfType<OkObjectResult>();

            OkObjectResult okResult = actionResult.Result.As<OkObjectResult>();
            okResult.Value.Should().BeAssignableTo<IEnumerable<MaterialDto>>();

            IEnumerable<MaterialDto> model = okResult.Value.As<IEnumerable<MaterialDto>>();
            model.Should().HaveCount(fakeMateriaisDto.Count);
        }

        [Fact]
        public async Task GetMaterialById_ReturnsOk()
        {
            // Arrange
            int materialId = 1;
            var fakeMaterial = CreateFakeMaterial(materialId, "Material de Teste 1", "Descrição 1", true, new DateOnly(2023, 12, 31), false, "Simulado 1", "https://exemplo.com/material-simulado-1", 10, 2);
            var fakeMaterialDto = A.Dummy<MaterialDto>();

            A.CallTo(() => _materialService.GetMaterialByIdAsync(materialId)).Returns(fakeMaterial);
            A.CallTo(() => _mapper.Map<MaterialDto>(fakeMaterial)).Returns(fakeMaterialDto);

            // Act
            var result = await _materiaisController.GetMaterialById(materialId);

            // Assert
            result.Should().BeOfType<ActionResult<MaterialDto>>();

            var actionResult = result.As<ActionResult<MaterialDto>>();
            actionResult.Result.Should().BeOfType<OkObjectResult>();

            var okResult = actionResult.Result.As<OkObjectResult>();
            okResult.Value.Should().BeAssignableTo<MaterialDto>();

            var model = okResult.Value.As<MaterialDto>();
            model.Should().BeEquivalentTo(fakeMaterialDto);
        }

        [Fact]
        public async Task GetMaterialById_ReturnsNotFound()
        {
            // Arrange
            int materialId = 2;

            A.CallTo(() => _materialService.GetMaterialByIdAsync(materialId)).Returns((Material?)null);

            // Act
            var result = await _materiaisController.GetMaterialById(materialId);

            // Assert
            result.Should().BeOfType<ActionResult<MaterialDto>>();

            var actionResult = result.As<ActionResult<MaterialDto>>();
            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetMaterialsByIdDiretoriaA_ReturnsOk()
        {
            // Arrange
            int diretoriaId = 1;
            var fakeMateriais = A.CollectionOfDummy<Material>(2);
            var fakeMateriaisDto = A.CollectionOfDummy<MaterialDto>(fakeMateriais.Count);

            A.CallTo(() => _materialService.GetMateriaisByIdDiretoriaAsync(diretoriaId)).Returns(fakeMateriais);
            A.CallTo(() => _mapper.Map<IEnumerable<MaterialDto>>(A<IEnumerable<Material>>.Ignored)).Returns(fakeMateriaisDto);

            // Act
            var result = await _materiaisController.GetMaterialsByIdDiretoriaA(diretoriaId);

            // Assert
            result.Should().BeOfType <ActionResult<IEnumerable<MaterialDto>>>();

            var actionResult = result.As<ActionResult<IEnumerable<MaterialDto>>>();
            actionResult.Result.Should().BeOfType<OkObjectResult>();

            var okResult = actionResult.Result.As<OkObjectResult>();
            okResult.Value.Should().BeAssignableTo<IEnumerable<MaterialDto>>();

            var model = okResult.Value.As<IEnumerable<MaterialDto>>();
            model.Should().HaveCount(fakeMateriaisDto.Count);
        }

        [Fact]
        public async Task GetMaterialByTitulo_ReturnsOk()
        {
            // Arrange
            string titulo = "Teste";
            var fakeMateriais = A.CollectionOfDummy<Material>(2);
            var fakeMateriaisDto = A.CollectionOfDummy<MaterialDto>(fakeMateriais.Count);

            A.CallTo(() => _materialService.GetMaterialByTituloAsync(titulo)).Returns(fakeMateriais);
            A.CallTo(() => _mapper.Map<IEnumerable<MaterialDto>>(A<IEnumerable<Material>>.Ignored)).Returns(fakeMateriaisDto);

            // Act
            var result = await _materiaisController.GetMaterialByTitulo(titulo);

            // Assert
            result.Should().BeOfType<ActionResult<IEnumerable<MaterialDto>>>();

            var actionResult = result.As<ActionResult<IEnumerable<MaterialDto>>>();
            actionResult.Result.Should().BeOfType<OkObjectResult>();

            var okResult = actionResult.Result.As<OkObjectResult>();
            okResult.Value.Should().BeAssignableTo<IEnumerable<MaterialDto>>();

            var model = okResult.Value.As<IEnumerable<MaterialDto>>();
            model.Should().HaveCount(fakeMateriaisDto.Count);
        }

        [Fact]
        public async Task PostMaterial_ReturnsCreated()
        {
            // Arrange
            var fakeMaterialAddViewModel = A.Dummy<MaterialAddViewModel>();
            var fakeMaterial = A.Dummy<Material>();

            A.CallTo(() => _mapper.Map<Material>(fakeMaterialAddViewModel)).Returns(fakeMaterial);

            // Act
            var result = await _materiaisController.PostMaterial(fakeMaterialAddViewModel);

            // Assert
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task PostMaterial_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var fakeInvalidMaterialAddViewModel = new MaterialAddViewModel();
            _materiaisController.ModelState.AddModelError("titulo", "titulo em branco");

            // Act
            var result = await _materiaisController.PostMaterial(fakeInvalidMaterialAddViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AlterarStatusMaterial_ReturnsOk()
        {
            // Arrange
            int materialId = 1;

            A.CallTo(() => _materialService.AlterarStatusMaterialAsync(materialId)).Returns(Task.CompletedTask);

            // Act
            var result = await _materiaisController.AlterarStatusMaterial(materialId);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task AlterarStatusMaterial_ReturnsNotFound()
        {
            // Arrange
            int materialId = 2;

            A.CallTo(() => _materialService.AlterarStatusMaterialAsync(materialId)).Throws<Exception>();

            // Act
            var result = await _materiaisController.AlterarStatusMaterial(materialId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateMaterial_ReturnsOk()
        {
            // Arrange
            int materialId = 1;
            var fakeMaterialUpdateViewModel = new MaterialUpdateViewModel
            {
                IdMaterial = 1,
            };
            var fakeMaterial = A.Dummy<Material>();

            A.CallTo(() => _materialService.UpdateMaterialAsync(fakeMaterial)).Returns(Task.CompletedTask);

            // Act
            var result = await _materiaisController.UpdateMaterial(materialId, fakeMaterialUpdateViewModel);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task UpdateMaterial_ReturnsBadRequest()
        {
            // Arrange
            int materialId = 3;
            var fakeMaterialUpdateViewModel = new MaterialUpdateViewModel
            {
                IdMaterial = 4,                    
            };

            A.CallTo(() => _materialService.UpdateMaterialAsync(A<Material>.Ignored))!.Returns(null);

            // Act
            var result = await _materiaisController.UpdateMaterial(materialId, fakeMaterialUpdateViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateMaterial_ReturnsNotFound()
        {
            // Arrange
            int materialId = 3;
            var fakeMaterialUpdateViewModel = new MaterialUpdateViewModel
            {
                IdMaterial = 3,
            };

            A.CallTo(() => _materialService.UpdateMaterialAsync(A<Material>.Ignored)).Throws<Exception>();

            // Act
            var result = await _materiaisController.UpdateMaterial(materialId, fakeMaterialUpdateViewModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        private static Material CreateFakeMaterial(int id, string titulo, string descricao, bool ativo, DateOnly dataFinal, bool obrigatorio, string tipo, string url, int pesoNota, int idDiretoria)
        {
            return new Material
            {
                IdMaterial = id,
                Titulo = titulo,
                Descricao = descricao,
                Ativo = ativo,
                DataFinal = dataFinal,
                Obrigatorio = obrigatorio,
                Tipo = tipo,
                Url = url,
                PesoNota = pesoNota,
                IdDiretoria = idDiretoria,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                CreatedBy = 3
            };
        }
    }
}
