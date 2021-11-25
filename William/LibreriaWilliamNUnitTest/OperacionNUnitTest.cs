using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWilliam
{
    [TestFixture]
    public class OperacionNUnitTest
    {
        [Test]
        public void SumarNumeros_InputDosNumeros_GetValorCorrecto()
        {
            //1. Arrange - inicializar las variables o componentes que ejecutaran el test 
            Operacion op = new();
            int numero1Test = 50;
            int numero2Test = 69;

            //2. Act  - Representa la ejecucion de la operacion
            int resultado = op.SumarNumeros(numero1Test, numero2Test);

            //3. Assert - Comparacion de resultados
            Assert.AreEqual(119, resultado);

        }

        [Test]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(7, ExpectedResult = false)]
        public bool IsValorPar_InputNumeroImpar_ReturnFalse(int numeroImpar)
        {
            Operacion op = new();
            return op.IsValorPar(numeroImpar);
        }

        [Test]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(80)]
        public void IsValorPar_InputNumeroPar_ReturnTrue(int numeroPar)
        {
            Operacion op = new();
            bool isPar = op.IsValorPar(numeroPar);
            Assert.IsTrue(isPar);
            Assert.That(isPar, Is.EqualTo(true));

        }

        [Test]
        [TestCase(2.2,1.2)]
        [TestCase(2.23, 1.24)]
        public void SumarDecimal_InputDosNumeros_GetValorCorrecto(double decimal1Test, double decimal2Test)
        {
            //1. Arrange - inicializar las variables o componentes que ejecutaran el test 
            Operacion op = new();
            

            //2. Act  - Representa la ejecucion de la operacion
            double resultado = op.SumarDecimal(decimal1Test, decimal2Test);

            //3. Assert - Comparacion de resultados

            //El resultado esta en el intervalo de 3.3 y 3.5
            Assert.AreEqual(3.4, resultado, 0.1);
            

        }

        [Test]
        public void GetListaNumerosImpares_InputMinimoMaximoIntervalos_ReturnsListaImpares()
        {
            //Arrange
            Operacion op = new();
            List<int> numerosImparesEsperados = new() { 5, 7, 9 };
            List<int> resultados = op.GetListaNumerosImpares(5, 10);

            Assert.That(resultados, Is.EquivalentTo(numerosImparesEsperados));
            Assert.AreEqual(numerosImparesEsperados, resultados);
            Assert.That(resultados, Does.Contain(5));
            Assert.Contains(5, resultados);
            Assert.That(resultados, Is.Not.Empty);
            Assert.That(resultados.Count, Is.EqualTo(3));
            Assert.That(resultados, Has.No.Member(100));
            Assert.That(resultados, Is.Ordered.Ascending);
            Assert.That(resultados, Is.Unique);
        }


    }
}
