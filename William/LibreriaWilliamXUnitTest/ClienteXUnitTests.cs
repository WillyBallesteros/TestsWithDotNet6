using System;
using LibreriaWilliam;
using Xunit;

namespace LibreriaWilliamNUnitTest
{
    
    public class ClienteXUnitTests
    {

        private Cliente cliente;

        
        public ClienteXUnitTests()
        {
            cliente = new Cliente();
        }

        [Fact]
        public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
        {
            //arrange

            //act
            cliente.ClientNombre = cliente.CrearNombreCompleto("William", "Ballesteros");

            //assert
                Assert.Equal("William Ballesteros", cliente.ClientNombre);
               
                Assert.Contains("Ballesteros", cliente.ClientNombre);
                
                Assert.StartsWith("William", cliente.ClientNombre);
                Assert.EndsWith("Ballesteros", cliente.ClientNombre);
        }

        [Fact]
        public void ClientNombre_NoValues_ReturnNull()
        {
            Assert.Null(cliente.ClientNombre);
        }

        [Fact]
        public void DescuentoEvaluacion_DefaultClient_ReturnsDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;
            Assert.InRange(descuento, 5, 24);
        }

        [Fact]
        public void CrearNombrecompleto_InputNombre_ReturnsNotNull()
        {
            cliente.CrearNombreCompleto("William", "");
            Assert.NotNull(cliente.ClientNombre);
            Assert.False(string.IsNullOrEmpty(cliente.ClientNombre));
        }

        [Fact]
        public void ClientNombre_InputNombreEnBlanco_TrowsException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Ballesteros"));
            Assert.Equal("El nombre esta en blanco", exceptionDetalle?.Message);

            Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Ballesteros"));

        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMenos500TotalOrder_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 150;
            var resultado = cliente.GetClienteDetalle();
            //Assert.That(resultado, Is.TypeOf<ClienteBasico>());
            Assert.IsType<ClienteBasico>(resultado);
        }

        [Fact]
        public void GetClienteDetalle_CrearClienteConMas500TotalOrder_ReturnsClientePremium()
        {
            cliente.OrderTotal = 700;
            var resultado = cliente.GetClienteDetalle();
            Assert.IsType<ClientePremium>(resultado);
        }
    }
}
