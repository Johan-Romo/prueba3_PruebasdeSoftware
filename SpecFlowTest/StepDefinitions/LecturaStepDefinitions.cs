using ProductosPractica.Models;
using ProductosPractica.Data;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class LecturaStepDefinitions
    {
        private readonly ProductSqlDataAccessLayer _productoDataAccessLayer = new ProductSqlDataAccessLayer(); // Usando ProductSqlDataAccessLayer para métodos de producto
        private Product _productoConsultado;

        [Given(@"El producto con nombre ""([^""]*)"" existe en la base de datos")]
        public void GivenElProductoConNombreExisteEnLaBaseDeDatos(string productName)
        {
            // Buscar el producto en la base de datos por nombre
            _productoConsultado = _productoDataAccessLayer.GetProductByName(productName);

            // Verificar que el producto no sea nulo
            _productoConsultado.Should().NotBeNull($"El producto con nombre '{productName}' no existe en la base de datos.");
        }

        [When(@"Recuperación el producto desde la base de datos")]
        public void WhenRecuperacionElProductoDesdeLaBaseDeDatos()
        {
            // Este paso no es necesario si ya se recuperó el producto en el paso Given
            // Puedes eliminar este paso si no es necesario o implementarlo si se requiere una recuperación adicional.
        }

        [Then(@"Los datos del producto deben coincidir con:")]
        public void ThenLosDatosDelProductoDebenCoincidirCon(Table table)
        {
            // Obtener los datos esperados
            var productoEsperado = table.CreateInstance<Product>();

            // Comparar los datos
            _productoConsultado.Should().NotBeNull("El producto no fue recuperado correctamente.");
            _productoConsultado.ProductName.Should().Be(productoEsperado.ProductName);
            _productoConsultado.Category.Should().Be(productoEsperado.Category);
            _productoConsultado.Price.Should().Be(productoEsperado.Price);
            _productoConsultado.StockQuantity.Should().Be(productoEsperado.StockQuantity);
        }
    }
}
