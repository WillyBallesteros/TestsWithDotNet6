using LibreriaWilliam;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWilliamNUnitTest
{
    [TestFixture]
    public class CuentaBancariaNUnitTests
    {
        private CuentaBancaria cuenta;
        [SetUp]
        public void Setup()
        {          
        }
        [Test]
        public void Deposito_InputMonto100LoggerFake_ReturnsTrue()
        {
            CuentaBancaria cuentaBancaria = new CuentaBancaria(new LoggerFake());
            var resultado = cuentaBancaria.Deposito(100);
            Assert.True(resultado);
            Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));
        }
        [Test]
        public void Deposito_InputMonto100Mocking_ReturnsTrue()
        {
            var mocking = new Mock<LoggerGeneral>();
            CuentaBancaria cuentaBancaria = new CuentaBancaria(mocking.Object);
            var resultado = cuentaBancaria.Deposito(100);
            Assert.True(resultado);
            Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));
        }
        [Test]
        [TestCase(200,100)]
        [TestCase(200, 150)]
        public void Retiro_Retiro100ConBalance200_ReturnsTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            loggerMock.Setup(u => u.LogDatabase(It.IsAny<string>())).Returns(true);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x > 0))).Returns(true);
            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);
            var resultado = cuentaBancaria.Retiro(retiro);
            Assert.IsTrue(resultado);
        }
        [Test]
        [TestCase(200, 300)]
        public void Retiro_Retiro300ConBalance200_ReturnsTrue(int balance, int retiro)
        {
            var loggerMock = new Mock<ILoggerGeneral>();
            //loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.Is<int>(x => x < 0))).Returns(false);
            loggerMock.Setup(u => u.LogBalanceDespuesRetiro(It.IsInRange<int>(int.MinValue,-1,Moq.Range.Inclusive))).Returns(false);
            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerMock.Object);
            cuentaBancaria.Deposito(balance);
            var resultado = cuentaBancaria.Retiro(retiro);
            Assert.IsFalse(resultado);
        }
        [Test]
        public void CuentaBancariaLoggerGeneral_LogMocking_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola mundo";
            loggerGeneralMock.Setup(u => u.MessageConReturnStr(It.IsAny<string>())).Returns<string>(str => str.ToLower());

            var resultado = loggerGeneralMock.Object.MessageConReturnStr("holA MUndo");

            Assert.That(resultado, Is.EqualTo(textoPrueba));
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingOutPut_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            string textoPrueba = "hola";

            loggerGeneralMock.Setup(u => u.MessageConOutParametroReturnBoolean(It.IsAny<string>(), out textoPrueba)).Returns(true);
            
            string parametroOut = "";
            var resultado = loggerGeneralMock.Object.MessageConOutParametroReturnBoolean("William", out parametroOut);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingObjetoRef_ReturnTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            Cliente cliente = new Cliente();
            Cliente clienteNoUsado = new();

            loggerGeneralMock.Setup(u => u.MessageConObjetoReferenciaReturnBoolean(ref cliente)).Returns(true);

            Assert.IsTrue(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref cliente));

            Assert.IsFalse(loggerGeneralMock.Object.MessageConObjetoReferenciaReturnBoolean(ref clienteNoUsado));
        }
        [Test]
        public void CuentaBancariaLoggerGeneral_LogMockingPropiedadPrioridadTipo_ReturnsTrue()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();
            loggerGeneralMock.SetupAllProperties();
            loggerGeneralMock.Setup(u => u.TipoLogger).Returns("warning");
            loggerGeneralMock.Setup(u => u.PrioridadLogger).Returns(10);
       
            loggerGeneralMock.Object.PrioridadLogger = 100;

            Assert.That(loggerGeneralMock.Object.TipoLogger, Is.EqualTo("warning"));
            Assert.That(loggerGeneralMock.Object.PrioridadLogger, Is.EqualTo(100));

            //CALLBACKS

            string textoTemporal = "will";
            loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>()))
                .Returns(true)
                .Callback((string parametro) => textoTemporal += parametro);

            loggerGeneralMock.Object.LogDatabase("white");

            Assert.That(textoTemporal, Is.EqualTo("willwhite"));

            //int contador = 5;
            //loggerGeneralMock.Setup(u => u.LogDatabase(It.IsAny<string>()))
            //    .Returns(true)
            //    .Callback(() => contador++);

            //Assert.That(contador, Is.EqualTo(6));

        }

        [Test]
        public void CuentaBancariaLogger_VerifyEjemplo()
        {
            var loggerGeneralMock = new Mock<ILoggerGeneral>();

            CuentaBancaria cuentaBancaria = new CuentaBancaria(loggerGeneralMock.Object);
            cuentaBancaria.Deposito(100);

            Assert.That(cuentaBancaria.GetBalance, Is.EqualTo(100));

            //verifica cuantas veces el mock esta llamando al metodo .message

            loggerGeneralMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(3));

            loggerGeneralMock.Verify(u => u.Message("Visita WhiteTech.co"), Times.AtLeastOnce);

            //para propiedades es VerifySet Set y Get
            loggerGeneralMock.VerifySet(u => u.PrioridadLogger=100, Times.Once);
            loggerGeneralMock.VerifyGet(u => u.PrioridadLogger, Times.Once);

        }
    }
}
