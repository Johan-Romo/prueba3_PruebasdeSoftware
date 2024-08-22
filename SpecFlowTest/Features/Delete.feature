Feature: Eliminación de Producto

Un resumen breve de la característica
/Este escenario es referencia para eliminar un producto existente del inventario solamente si el stock es 0/

@tag1
Scenario: Eliminar un producto existente
    Given El producto existe en la base de datos con stock 0
    | ProductName | Category    | Price | StockQuantity |
    | ProductoB   | Electrónica | 100   | 0             |
    When El producto es eliminado de la BDD
    | ProductName |
    | ProductoB   |
    Then El producto no debe existir en la BDD
    | ProductName |
    | ProductoB   |
