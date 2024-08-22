Feature: Lectura 
  Este feature verifica que se pueden leer correctamente los productos desde la base de datos.

@Lectura
Scenario: Consultar un producto existente en la base de datos
  Given El producto con nombre "ProductoX" existe en la base de datos
  When Recuperación el producto desde la base de datos
  Then Los datos del producto deben coincidir con:
    | ProductName | Category    | Price | StockQuantity |
    | ProductoX   | Electrónica | 100   | 50            |