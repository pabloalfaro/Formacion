using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Empresa
{
    public int Id { get; set; }
    public string NombreSociedad { get; set; }
    public string Cif { get; set; }
    public string CoincidenciaPor { get; set; }
    public string TextoCoincidencia { get; set; }
    public string Provincia { get; set; }
    public string NombreFiltrado { get; set; }
    public int LongitudBarra { get; set; }
    public string DatosRegistrales { get; set; }
    //public Coincidentes ListaNombresCoincidentes { get; set; }
}