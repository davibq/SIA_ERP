-- =============================================
-- Script Template
-- =============================================

-- =============================================
-- FIN_TipoAsiento
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal -> con razón no me sirve!
-- Fecha: 23/09/2012
-- Descripcion: Primeros valores para TipoAsiento
-----------------------------------------------------------
INSERT INTO dbo.FIN_TipoAsiento (Nombre, Descripcion) VALUES ('AS', 'Asientos Manuales')

GO
-- =============================================
-- FIN_Identificador
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 23/09/2012
-- Descripcion: Primeros valores para Identificadores de cuenta
-----------------------------------------------------------
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Activo', 1)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Pasivo', 2)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Patrimonio', 3)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Ingresos', 4)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Costos', 5)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Gastos', 6)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Otros Ingresos', 7)
INSERT INTO dbo.FIN_IdentificadorCuenta(Nombre, Numero) VALUES ('Otros Gastos', 8)


-- =============================================
-- FIN_EstadoMes
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 28/09/2012
-- Descripcion: Primeros valores para estado de mes
-----------------------------------------------------------
INSERT INTO dbo.FIN_EstadoMes VALUES('Abierto')
INSERT INTO dbo.FIN_EstadoMes VALUES('Cerrado')
INSERT INTO dbo.FIN_EstadoMes VALUES('Abierto excepto ventas')


-- =============================================
-- Monedas
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 06/10/2012
-- Descripcion: Primeros valores para monedas
-----------------------------------------------------------
EXEC dbo.ERPSP_InsertarMonedas '<Monedas><Moneda nombre="Colon" acronimo="CRC" esLocal="1" esSistema="0" idBCCR="1"/><Moneda nombre="Dolar" acronimo="USD" esLocal="0" esSistema="1" idBCCR="317"/></Monedas>'

-- =============================================
-- Cuentas
-- =============================================
-----------------------------------------------------------
-- Autor: Rmadrigal
-- Fecha: 06/10/2012
-- Descripcion: Inserta el catalogo de cuentas inicial
-----------------------------------------------------------
EXEC dbo.ERPSP_ActualizarCuenta 'ACTIVOS', '1', 1, 1, NULL, 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="ACTIVOS" idioma="es"/><Nombre nombre="ACTIVES" idioma="en-US"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'PASIVOS', '2', 1, 1, NULL, 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="PASIVOS" idioma="es"/><Nombre nombre="PASIVES" idioma="en-US"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'PATRIMONIO', '3', 1, 1, NULL, 'Patrimonio', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="PATRIMONIO" idioma="es"/><Nombre nombre="PATRIMONY" idioma="en-US"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'INGRESOS', '4', 1, 1, NULL, 'Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="INGRESOS" idioma="es"/><Nombre nombre="INCOMES" idioma="en-US"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'COSTOS', '5', 1, 1, NULL, 'Costos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="COSTOS" idioma="es"/><Nombre nombre="COSTS" idioma="en-US"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'GASTOS', '6', 1, 1, NULL, 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="GASTOS" idioma="es"/><Nombre nombre="ESXPENSES" idioma="en-US"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'OTROS INGRESOS', '7', 1, 1, NULL, 'Otros Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="OTROS INGRESOS" idioma="es"/><Nombre nombre="OTHER INCOMES" idioma="en-US"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'OTROS GASTOS', '8', 1, 1, NULL, 'Otros Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="OTROS GASTOS" idioma="es"/><Nombre nombre="OTHER EXPENSES" idioma="en-US"/></Nombres>'


EXEC dbo.ERPSP_ActualizarCuenta 'ACTIVOS CIRCULANTES', '1-1', 2, 1, '1', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="ACTIVOS CIRCULANTES" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'EFECTIVO Y BANCOS', '1-1-01', 3, 1, '1-1', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="EFECTIVO Y BANCOS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Caja Chica', '1-1-01-01', 4, 1, '1-1-01', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Caja Chica" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Banco Nacional Dólares', '1-1-01-02', 4, 1, '1-1-01', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Banco Nacional Dólares" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Banco Nacional colones', '1-1-01-03' , 4, 1, '1-1-01', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Banco Nacional colones" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Banco de CR colones', '1-1-01-04' , 4, 1, '1-1-01', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Banco de CR colones" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'CUENTAS POR COBRAR', '1-1-02', 3, 1, '1-1', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="CUENTAS POR COBRAR" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Cuentas por cobrar locales', '1-1-02-01' , 4, 1, '1-1-02', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Cuentas por cobrar locales" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Cuentas por cobrar internacionales', '1-1-02-02', 4, 1, '1-1-02', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Cuentas por cobrar internacionales" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'INVENTARIOS', '1-1-03',  3, 1, '1-1', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="INVENTARIOS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Inventario General', '1-1-03-01' , 4, 1, '1-1-03', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Inventario General" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Inventario Dañado', '1-1-03-02' , 4, 1, '1-1-03', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Inventario Dañado" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Inventario Consignación', '1-1-03-03', 4, 1, '1-1-03', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Inventario Consignación" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'ACTIVOS FIJOS', '1-2', 2, 1, '1', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="ACTIVOS FIJOS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'TERRENOS, EQUIPO, MAQUINARIA', '1-2-01' , 3, 1, '1-2', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="TERRENOS, EQUIPO, MAQUINARIA" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Equipo y maquinaria', '1-2-01-01',  4, 1, '1-2-01', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Equipo y maquinaria" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Depreciación acumulada', '1-2-01-02' , 4, 1, '1-2-01', 'Activo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Depreciación acumulada" idioma="es"/></Nombres>'


EXEC dbo.ERPSP_ActualizarCuenta 'PASIVOS CORTO PLAZO', '2-1', 2, 1, '2', 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="PASIVOS CORTO PLAZO" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Cuentas por pagar', '2-1-01', 3, 1, '2-1', 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Cuentas por pagar" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Cuentas por pagar nacionales', '2-1-01-01', 4, 1, '2-1-01', 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Cuentas por pagar nacionales" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Cuentas por pagar extranjero', '2-1-01-02', 4, 1, '2-1-01', 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Cuentas por pagar extranjero" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'PASIVOS LARGO PLAZO', '2-2' , 2, 1, '2', 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="PASIVOS LARGO PLAZO" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'PRESTAMOS', '2-2-01' , 3, 1, '2-2', 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="PRESTAMOS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Hipoteca', '2-2-01-01', 4, 1, '2-2-01', 'Pasivo', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Hipoteca" idioma="es"/></Nombres>'


EXEC dbo.ERPSP_ActualizarCuenta 'CAPITAL Y UTILIDADES', '3-1', 2, 1, '3', 'Patrimonio', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="CAPITAL Y UTILIDADES" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Capital social', '3-1-01', 3, 1, '3-1', 'Patrimonio', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Capital social" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Utilidades retenidas', '3-1-02', 3, 1, '3-1', 'Patrimonio', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Utilidades retenidas" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Pérdidas y ganancias', '3-1-03', 3, 1, '3-1', 'Patrimonio', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Pérdidas y ganancias" idioma="es"/></Nombres>'


EXEC dbo.ERPSP_ActualizarCuenta 'VENTAS', '4-1', 2, 1, '4', 'Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="VENTAS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Ventas', '4-1-01', 3, 1, '4-1', 'Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Ventas" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Descuentos sobre ventas', '4-1-02', 3, 1, '4-1', 'Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Descuentos sobre ventas" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Devoluciones sobre ventas', '4-1-03', 3, 1, '4-1', 'Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Devoluciones sobre ventas" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Ingresos por consultoría', '4-1-04' , 3, 1, '4-1', 'Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Ingresos por consultoría" idioma="es"/></Nombres>'

EXEC dbo.ERPSP_ActualizarCuenta 'COSTO DE VENTAS', '5-1', 2, 1, '5', 'Costos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="COSTO DE VENTAS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Costo de ventas', '5-1-01', 3, 1, '5-1', 'Costos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Costo de ventas" idioma="es"/></Nombres>'


EXEC dbo.ERPSP_ActualizarCuenta 'GASTOS DE VENTAS', '6-1', 2, 1, '6', 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="GASTOS DE VENTAS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Bonificaciones', '6-1-01', 3, 1, '6-1', 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Bonificaciones" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'GASTOS ADMINISTRATIVOS', '6-2', 2, 1, '6', 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="GASTOS ADMINISTRATIVOS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Salarios', '6-2-01', 3, 1, '6-2', 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Salarios" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Servicios varios', '6-2-02' , 3, 1, '6-2', 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Servicios varios" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'GASTOS DE OPERACION', '6-3' , 2, 1, '6', 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="GASTOS DE OPERACION" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Pérdidas por inventario dañado', '6-3-01', 3, 1, '6-3', 'Gastos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Pérdidas por inventario dañado" idioma="es"/></Nombres>'


EXEC dbo.ERPSP_ActualizarCuenta 'OTROS INGRESOS', '7-1', 2, 1, '7', 'Otros Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="OTROS INGRESOS" idioma="es"/></Nombres>'
EXEC dbo.ERPSP_ActualizarCuenta 'Ingresos financieros', '7-1-01', 3, 1, '7-1', 'Otros Ingresos', '<MonedasCuenta><Moneda moneda="CRC"/><Moneda moneda="USD"/></MonedasCuenta>', '<Nombres><Nombre nombre="Ingresos financieros" idioma="es"/></Nombres>'









