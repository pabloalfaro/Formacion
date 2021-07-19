USE FORMACION_DB
GO
CREATE PROCEDURE dbo.GuardarEmpresaPAG
	@nombreSociedad nvarchar(50),
	@cif nvarchar(10),
	@coincidencia nvarchar(50),
	@provincia nvarchar(50),
	@longitudBarra int,
	@regis nvarchar(10)= 'vigente',
	
	@id int=null output,
	@devolver smallint=1
as
begin
	if not exists (select 1 from dbo.EmpresasPAG where Cif=@cif) begin
		--insert
		insert into dbo.EmpresasPAG (NombreSociedad, Cif, CoincidenciaPor, TextoCoincidencia, Provincia, NombreFiltrado, LongitudBarra, DatosRegistrales)
			values (@nombreSociedad, @cif, @coincidencia, @nombreSociedad, @provincia, @nombreSociedad, @longitudBarra, @regis)
		set @id=scope_identity()
	end
	else begin
		--update
		update dbo.EmpresasPAG 
		set NombreSociedad = @nombreSociedad,
			Cif = @cif,
			CoincidenciaPor = @coincidencia,
			TextoCoincidencia = @nombreSociedad,
			Provincia = @provincia,
			NombreFiltrado = @nombreSociedad,
			LongitudBarra = @longitudBarra,
			DatosRegistrales = @regis
		where Cif = @cif

		select @id=Id from dbo.EmpresasPAG where Cif=@cif
	end
	if @devolver=1 begin
		select @id as Id
	end
end
go