using System;
using LibreriaWilliam;
using NUnit.Framework;

namespace LibreriaWilliamNUnitTest
{
    [TestFixture]
    public class ClienteNUnitTests
    {

        private Cliente cliente;

        [SetUp]
        public void Setup()
        {
            cliente = new Cliente();
        }

        [Test]
        public void CrearNombreCompleto_InputNombreApellido_ReturnNombreCompleto()
        {
            //arrange
           
            //act
            cliente.ClientNombre = cliente.CrearNombreCompleto("William", "Ballesteros");

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(cliente.ClientNombre, Is.EqualTo("William Ballesteros"));
                Assert.AreEqual(cliente.ClientNombre, "William Ballesteros");
                Assert.That(cliente.ClientNombre, Does.Contain("Ballesteros"));
                Assert.That(cliente.ClientNombre, Does.Contain("ballesteros").IgnoreCase);
                Assert.That(cliente.ClientNombre, Does.StartWith("William"));
                Assert.That(cliente.ClientNombre, Does.EndWith("Ballesteros"));
            });

            
            
        }

        [Test]
        public void ClientNombre_NoValues_ReturnNull()
        {
            Assert.IsNull(cliente.ClientNombre);
        }

        [Test]
        public void DescuentoEvaluacion_DefaultClient_ReturnsDescuentoIntervalo()
        {
            int descuento = cliente.Descuento;
            Assert.That(descuento, Is.InRange(5, 24));
        }

        [Test]
        public void CrearNombrecompleto_InputNombre_ReturnsNotNull()
        {
            cliente.CrearNombreCompleto("William", "");
            Assert.IsNotNull(cliente.ClientNombre);
            Assert.IsFalse(string.IsNullOrEmpty(cliente.ClientNombre)); 
        }

        [Test]
        public void ClientNombre_InputNombreEnBlanco_TrowsException()
        {
            var exceptionDetalle = Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Ballesteros"));
            Assert.AreEqual("El nombre esta en blanco", exceptionDetalle?.Message);

            Assert.That(() =>
                cliente.CrearNombreCompleto("", "Ballesteros"), Throws.ArgumentException.With.Message.EqualTo("El nombre esta en blanco")
            );

            Assert.Throws<ArgumentException>(() => cliente.CrearNombreCompleto("", "Ballesteros"));

            Assert.That(() =>
                cliente.CrearNombreCompleto("", "Ballesteros"), Throws.ArgumentException
            );
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMenos500TotalOrder_ReturnsClienteBasico()
        {
            cliente.OrderTotal = 150;
            var resultado = cliente.GetClienteDetalle();
            Assert.That(resultado, Is.TypeOf<ClienteBasico>());
        }

        [Test]
        public void GetClienteDetalle_CrearClienteConMas500TotalOrder_ReturnsClientePremium()
        {
            cliente.OrderTotal = 700;
            var resultado = cliente.GetClienteDetalle();
            Assert.That(resultado, Is.TypeOf<ClientePremium>());
        }
    }
}
