Feature: Ingresar
Un resumen breve de la característica
/Este escenario es referencia para agregar un nuevo producto al inventario/

@tag1
Scenario: Crear un nuevo producto
    Given Llenar los campos del producto
    | ProductName | Category    | Price | StockQuantity |
    | ProductoX   | Electrónica | 100   | 50            |
    When El registro se ingresa en la BDD
    | ProductName | Category    | Price | StockQuantity |
    | ProductoX   | Electrónica | 100   | 50            |
    Then El resultado se ingreso en la BDD
    | ProductName | Category    | Price | StockQuantity |
    | ProductoX   | Electrónica | 100   | 50            |
