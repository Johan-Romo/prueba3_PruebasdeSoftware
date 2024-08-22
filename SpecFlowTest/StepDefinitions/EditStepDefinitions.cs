using ProductosPractica.Data;
using ProductosPractica.Models;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowPS.StepDefinitions
{
    [Binding]
    public class ActualizacionStepDefinitions
    {
        private readonly ProductSqlDataAccessLayer _productoDataAccessLayer = new ProductSqlDataAccessLayer();
        private Product _productoOriginal;

        [Given(@"Actualizar los campos del producto")]
        public void GivenElProductoExisteEnLaBaseDeDatos(Table table)
        {
            _productoOriginal = table.CreateInstance<Product>();
            _productoOriginal = _productoDataAccessLayer.GetProductById(_productoOriginal.ProductId);
            _productoOriginal.Should().NotBeNull("El producto original no existe en la base de datos.");
        }

        [When(@"El registro se actualiza en la BDD")]
        public void WhenActualizoLosDatosDelProducto(Table table)
        {
            var productoEditado = table.CreateInstance<Product>();
            productoEditado.ProductId = _productoOriginal.ProductId;
            _productoDataAccessLayer.UpdateProduct(productoEditado);
        }

        [Then(@"El resultado se actualiza en la BDD")]
        public void ThenElProductoDebeEstarActualizadoEnLaBaseDeDatos(Table table)
        {
            var productoEsperado = table.CreateInstance<Product>();
            var productoReal = _productoDataAccessLayer.GetProductById(productoEsperado.ProductId);

            productoReal.Should().NotBeNull($"El producto con el ID {productoEsperado.ProductId} no fue encontrado en la base de datos");
            productoReal.ProductId.Should().Be(productoEsperado.ProductId);
            productoReal.ProductName.Should().Be(productoEsperado.ProductName);
            productoReal.Category.Should().Be(productoEsperado.Category);
            productoReal.Price.Should().Be(productoEsperado.Price);
            productoReal.StockQuantity.Should().Be(productoEsperado.StockQuantity);
        }
    }
}
