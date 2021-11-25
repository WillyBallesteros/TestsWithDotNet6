using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibreriaWilliam
{
    
    public class OperacionXUnitTest
    {
        [Fact]
        public void SumarNumeros_InputDosNumeros_GetValorCorrecto()
        {
            //1. Arrange - inicializar las variables o componentes que ejecutaran el test 
            Operacion op = new();
            int numero1Test = 50;
            int numero2Test = 69;

            //2. Act  - Representa la ejecucion de la operacion
            int resultado = op.SumarNumeros(numero1Test, numero2Test);

            //3. Assert - Comparacion de resultados
            Assert.Equal(119, resultado);

        }

        [Theory]
        [InlineData(3,  false)]
        [InlineData(5,  false)]
        [InlineData(7,  false)]
        public void IsValorPar_InputNumeroImpar_ReturnFalse(int numeroImpar, bool expectedResult)
        {
            Operacion op = new();
            var resultado = op.IsValorPar(numeroImpar);
            Assert.Equal(expectedResult, resultado);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(80)]
        public void IsValorPar_InputNumeroPar_ReturnTrue(int numeroPar)
        {
            Operacion op = new();
            bool isPar = op.IsValorPar(numeroPar);
            
            Assert.True(isPar);
        }

        [Theory]
        [InlineData(2.2, 1.2)]
        [InlineData(2.23, 1.24)]
        public void SumarDecimal_InputDosNumeros_GetValorCorrecto(double decimal1Test, double decimal2Test)
        {
            //1. Arrange - inicializar las variables o componentes que ejecutaran el test 
            Operacion op = new();


            //2. Act  - Representa la ejecucion de la operacion
            double resultado = op.SumarDecimal(decimal1Test, decimal2Test);

            //3. Assert - Comparacion de resultados

            //El resultado esta en el intervalo de 3.3 y 3.5
            Assert.Equal(3.4, resultado, 0);


        }

        [Fact]
        public void GetListaNumerosImpares_InputMinimoMaximoIntervalos_ReturnsListaImpares()
        {
            //Arrange
            Operacion op = new();
            List<int> numerosImparesEsperados = new() { 5, 7, 9 };
            List<int> resultados = op.GetListaNumerosImpares(5, 10);

            Assert.Equal(numerosImparesEsperados, resultados);
            
            Assert.Contains(5, resultados);

            Assert.NotEmpty(resultados);
            Assert.Equal(3, resultados.Count);
            Assert.DoesNotContain(100,resultados);
            Assert.Equal(resultados.OrderBy(u => u), resultados);
           
        }


    }
}
