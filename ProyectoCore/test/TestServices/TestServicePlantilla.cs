using Moq;
using ProyectoCore.Repositories.Interfaces;
using ProyectoCore.Models;
using ProyectoCore.Services;
using Xunit;

namespace ProyectoCore.Tests
{
    public class ServicePlantillaTests
    {
        [Fact]
        public void ObtenerPorId_CuandoExiste_DebeRetornarPlantilla()
        {
            // ARRANGE
            var mockRepo = new Mock<IRepoPlantilla>();
            
            var plantillaEsperada = new Plantilla 
            { 
                Id = 1, 
                Nombre = "Dream Team DT", 
                UsuarioId = 10 
            };

            // Moq sobre ObtenerPorId(1)
            mockRepo.Setup(repo => repo.ObtenerPorId(1))
                    .Returns(plantillaEsperada);

            var ServicePlantilla = new ServicePlantilla(mockRepo.Object);

            // ACT
            var resultado = ServicePlantilla.ObtenerPorId(1);

            // ASSERT
            Assert.NotNull(resultado);
            Assert.Equal("Dream Team DT", resultado.Nombre);
            Assert.Equal(10, resultado.UsuarioId);
            
            mockRepo.Verify(repo => repo.ObtenerPorId(1), Times.Once);
        }

        [Fact]
        public void ObtenerPorUsuarioId_CuandoExiste_DebeRetornarPlantilla()
        {
            // ARRANGE
            var mockRepo = new Mock<IRepoPlantilla>();
            
            var plantillaUsuario = new Plantilla 
            { 
                Id = 5, 
                Nombre = "Los Campiones", 
                UsuarioId = 2 
            };

            // Moq sobre ObtenerPorUsuarioId(2)
            mockRepo.Setup(repo => repo.ObtenerPorUsuarioId(2))
                    .Returns(plantillaUsuario);

            var ServicePlantilla = new ServicePlantilla(mockRepo.Object);

            // ACT
            var resultado = ServicePlantilla.ObtenerPorUsuarioId(2);

            // ASSERT
            Assert.NotNull(resultado);
            Assert.Equal(5, resultado.Id);
            Assert.Equal(2, resultado.UsuarioId);
            
            mockRepo.Verify(repo => repo.ObtenerPorUsuarioId(2), Times.Once);
        }

        [Fact]
        public void Crear_PlantillaValida_DebeRetornarIdGenerado()
        {
            // ARRANGE
            var mockRepo = new Mock<IRepoPlantilla>();
            
            var nuevaPlantilla = new Plantilla 
            { 
                Nombre = "Mi Plantilla 2026", 
                UsuarioId = 3 
            };

            // Moq sobre Crear(Plantilla) -> retorna ID 1
            mockRepo.Setup(repo => repo.Crear(It.IsAny<Plantilla>()))
                    .Returns(1);

            var ServicePlantilla = new ServicePlantilla(mockRepo.Object);

            // ACT
            var idCreado = ServicePlantilla.Crear(nuevaPlantilla);

            // ASSERT
            Assert.Equal(1, idCreado);
            mockRepo.Verify(repo => repo.Crear(It.IsAny<Plantilla>()), Times.Once);
        }
    }
}