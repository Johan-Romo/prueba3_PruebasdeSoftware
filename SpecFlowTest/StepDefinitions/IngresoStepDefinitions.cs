using ProductosPractica.Data;
using ProductosPractica.Models;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class ProductoStepDefinitions
    {
        private readonly ProductSqlDataAccessLayer _productoDAL = new ProductSqlDataAccessLayer();

        [Given(@"Llenar los campos del producto")]
        public void GivenLlenarLosCamposDelProducto(Table table)
        {
            // Verifica que la tabla tenga al menos una fila
            table.Rows.Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [When(@"El registro se ingresa en la BDD")]
        public void WhenElRegistroSeIngresaEnLaBDD(Table table)
        {
            var productos = table.CreateSet<Product>().ToList();

            foreach (var producto in productos)
            {
                // Verifica si el producto ya existe en la base de datos
                var productoExistente = _productoDAL.GetProductById(producto.ProductId);

                if (productoExistente != null)
                {
                    // Si el producto ya existe, lanzar una excepción o manejar el caso como consideres necesario
                    throw new Exception($"El producto con ProductId {producto.ProductId} ya está registrado en la base de datos.");
                }

                Product nuevoProducto = new Product
                {

                    ProductName = producto.ProductName,
                    Price = producto.Price,
                    Category = producto.Category,
                    StockQuantity = producto.StockQuantity,
                };

                _productoDAL.AddProduct(nuevoProducto);
            }
        }


        [Then(@"El resultado se ingreso en la BDD")]
        public void ThenElResultadoSeIngresaEnLaBDD(Table table)
        {
            var productoEsperado = table.CreateSet<Product>().First();

            // Obtiene todos los productos de la base de datos para verificar si el producto esperado ha sido insertado.
            var todosLosProductos = _productoDAL.GetAllProducts();

            // Busca el producto esperado en la lista de todos los productos por ProductName
            var productoReal = todosLosProductos.FirstOrDefault(p => p.ProductName == productoEsperado.ProductName);

            // Verifica que el producto obtenido de la base de datos no sea nulo.
            productoReal.Should().NotBeNull($"El producto con nombre {productoEsperado.ProductName} no se encontró en la base de datos");

            // Verifica que los campos del producto insertado coincidan con los datos esperados.
            productoReal.ProductName.Should().Be(productoEsperado.ProductName);
            productoReal.Price.Should().Be(productoEsperado.Price);
            productoReal.Category.Should().Be(productoEsperado.Category);
            productoReal.StockQuantity.Should().Be(productoEsperado.StockQuantity);
        }


    }
}