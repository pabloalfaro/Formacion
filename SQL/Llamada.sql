USE FORMACION_DB
GO

DECLARE @i AS INT
SET @i = 0

WHILE @i < 1000
BEGIN
	--Generar CIFs aleatorios
	DECLARE @chars AS VARCHAR(52),
			@cont AS INT,
			@index AS INT,
			@cif AS NVARCHAR(10),
			@numbers AS VARCHAR(10)

	SET @chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'
	SET @numbers = '0123456789'

	SET @index = ceiling( rand() * (len(@chars)))
	SET @cif = substring(@chars, @index, 1)

	SET @cont = 0
	WHILE @cont < 8
	BEGIN
		SET @index = ceiling( rand() * (len(@numbers)))
		SET @cif = @cif + substring(@numbers, @index, 1)
		SET @cont = @cont + 1
	END 
	--Final del generador de CIFs

	--Elección de provincia
	DECLARE @provincia AS NVARCHAR(50)

	SET @index = ceiling(rand() *5)

	IF @index = 1
		SET @provincia = 'GRANADA'
	ELSE IF @index = 2
		SET @provincia = 'NAVARRA'
	ELSE IF @index = 3
		SET @provincia = 'SEVILLA'
	ELSE IF @index = 4
		SET @provincia = 'VALENCIA'
	ELSE IF @index = 5
		SET @provincia = 'ASTURIAS'
	-- Fin de elección de provincia

	--Elección de empresa
	DECLARE @empresa AS NVARCHAR(50)

	SET @index = ceiling(rand() *5)

	IF @index = 1
		SET @empresa = 'SUPERMERCADO'
	ELSE IF @index = 2
		SET @empresa = 'TIENDA'
	ELSE IF @index = 3
		SET @empresa = 'ALMACÉN'
	ELSE IF @index = 4
		SET @empresa = 'OFICINA'
	ELSE IF @index = 5
		SET @empresa = 'FÁBRICA'

	SET @index = ceiling(rand() *5)

	IF @index = 1
		SET @empresa = @empresa +' EL CORTE INGLÉS'
	ELSE IF @index = 2
		SET @empresa = @empresa +' ADIDAS'
	ELSE IF @index = 3
		SET @empresa = @empresa +' LEGO'
	ELSE IF @index = 4
		SET @empresa = @empresa +' MERCADONA'
	ELSE IF @index = 5
		SET @empresa = @empresa +' SEAT'
	-- Fin de elección de empresa

	-- Elección coincidencia
	DECLARE @coincidencia AS NVARCHAR(50)

	SET @index = ceiling(rand() *3)

	IF @index = 1
		SET @coincidencia = 'Marca'
	ELSE IF @index = 2
		SET @coincidencia = 'Denominación social'
	ELSE IF @index = 3
		SET @coincidencia = 'Acrónimo'
	-- Fin elección de coincidencia

	exec  dbo.GuardarEmpresaPAG @empresa, @cif, @coincidencia, @provincia, '100'

	SET @i = @i + 1
END
go
--TODO: bucle para para insetar n  empresas llamando al SP
--DECLARE @empresa varchar(50) ='EMPRESA' +'A'
--exec  dbo.GuardarEmpresaPAG @empresa   ,'A12345678'

select * from dbo.EmpresasPAG