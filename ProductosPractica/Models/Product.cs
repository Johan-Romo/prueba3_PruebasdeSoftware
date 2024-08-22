using System;
using System.ComponentModel.DataAnnotations;

namespace ProductosPractica.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres")]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "El nombre de la catergoria es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre de la categoria no puede exceder los 100 caracteres")]
        public string Category { get; set; }


        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 9999999.99, ErrorMessage = "El precio debe estar entre 0.01 y 9,999,999.99")]
        [RegularExpression(@"^\d{1,7}(\.\d{1,2})?$", ErrorMessage = "El precio debe ser un número de hasta 7 dígitos enteros y hasta dos decimales")]
        public decimal Price { get; set;}

        [Required(ErrorMessage = "La cantidad en stock es obligatoria")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad en stock debe ser un número positivo")]
        public int StockQuantity { get; set; }

        public static ValidationResult ValidarPrecio(decimal precio, ValidationContext context)
        {
            if (precio <= 0)
            {
                return new ValidationResult("El precio debe ser mayor que cero");
            }

            return ValidationResult.Success;
        }
    }
}
