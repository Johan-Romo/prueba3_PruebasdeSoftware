using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosPractica.Data;
using ProductosPractica.Models;

namespace ProductosPractica.Controllers
{
    public class ProductController : Controller
    {
        ProductSqlDataAccessLayer objProductoDAL = new ProductSqlDataAccessLayer();

        public ActionResult Index()
        {
            List<Product> ListProduct = new List<Product>();
            ListProduct = objProductoDAL.GetAllProducts().ToList();
            return View(ListProduct);
        }

        public ActionResult Details(int id)
        {
            var product = objProductoDAL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product objProduct)
        {
            // Verificar si el producto ya existe por nombre
            if (objProductoDAL.ProductExistsByName(objProduct.ProductName))
            {
                // Agregar un mensaje de error al ModelState para que se muestre en la vista
                ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre.");
            }

            if (ModelState.IsValid)
            {
                objProductoDAL.AddProduct(objProduct);
                return RedirectToAction(nameof(Index));
            }

            // Si llega aquí, significa que hay un error, se vuelve a mostrar la vista con el mensaje de error
            return View(objProduct);
        }

        public ActionResult Edit(int id)
        {
            var product = objProductoDAL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product objProduct)
        {
            if (ModelState.IsValid)
            {
                objProductoDAL.UpdateProduct(objProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(objProduct);
        }

        public ActionResult Delete(int id)
        {
            var product = objProductoDAL.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objProductoDAL.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
