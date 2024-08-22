Feature: Edit

A short summary of the feature

@tag1
Scenario: Editar el producto
    Given Actualizar los campos del producto
   | ProductId | ProductName | Category    | Price | StockQuantity |
   | 2         | ProductoB   | Electrónica | 200   | 0             |
    When El registro se actualiza en la BDD 
   | ProductId | ProductName | Category    | Price | StockQuantity |
   | 2         | ProductoB   | Electrónica | 200   | 0             |
    Then El resultado se actualiza en la BDD
   | ProductId | ProductName | Category    | Price  | StockQuantity |
   | 2         | ProductoB   | Electrónica | 200    | 0             |
