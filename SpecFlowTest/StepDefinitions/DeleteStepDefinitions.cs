using ProductosPractica.Data;
using ProductosPractica.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;

namespace SpecFlowTest.StepDefinitions
{
    [Binding]
    public class ProductoEliminacionStepDefinitions
    {
        private readonly ProductSqlDataAccessLayer _productoDAL = new ProductSqlDataAccessLayer();

       [Given(@"El producto existe en la base de datos con stock (.*)")]
public void GivenElProductoExisteEnLaBaseDeDatosConStock(int stockQuantity, Table table)
{
    var producto = table.CreateInstance<Product>();
    var productoExistente = _productoDAL.GetProductByName(producto.ProductName);

    // Verifica que el producto exista
    productoExistente.Should().NotBeNull($"El producto con nombre {producto.ProductName} no se encuentra en la base de datos.");

    // Verifica que el stock del producto coincida con el valor esperado
    productoExistente.StockQuantity.Should().Be(stockQuantity, $"El stock del producto con nombre {producto.ProductName} debería ser {stockQuantity}.");
}

[When(@"El producto es eliminado de la BDD")]
public void WhenElProductoEsEliminadoDeLaBDD(Table table)
{
    var productoParaEliminar = table.CreateInstance<Product>();

    // Obtiene el producto actual de la base de datos por nombre
    var productoExistente = _productoDAL.GetProductByName(productoParaEliminar.ProductName);

    // Solo elimina el producto si el stock es 0
    if (productoExistente != null && productoExistente.StockQuantity == 0)
    {
        _productoDAL.DeleteProductByName(productoParaEliminar.ProductName);
    }
}

[Then(@"El producto no debe existir en la BDD")]
public void ThenElProductoNoDebeExistirEnLaBDD(Table table)
{
    var productoEsperado = table.CreateInstance<Product>();
    var productoReal = _productoDAL.GetProductByName(productoEsperado.ProductName);
    productoReal.Should().BeNull($"El producto con nombre {productoEsperado.ProductName} todavía existe en la base de datos.");
}

    }
}
