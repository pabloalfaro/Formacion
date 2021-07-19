using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Extensions.Configuration;

namespace TodoApi.Controllers
{
    [Route("api/empresaBD")]
    [ApiController]
    public class EmpresasControllerEF : ControllerBase
    {
        private readonly ContextoDB _context;

        public EmpresasControllerEF(ContextoDB context)
        {
            _context = context;
        }

        /// <summary>
        /// Muestra una lista con todas las empresas
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/empresa
        ///     
        /// </remarks>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Devuelve los datos de todas las empresas</response>
        /// <response code="404">No hay empresas registradas</response>  
        // GET: api/EmpresasControllerContext
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            var resultado = await _context.EmpresasPAG.ToListAsync();
            return resultado;
        }

        /// <summary>
        /// Muestra la empresa con el indentificador CIF indicado o con el nombre indicado
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/empresa/<paramref name="Cif"/>
        ///     {
        ///        "CIF": A46103834,
        ///     }
        ///
        /// </remarks>
        /// <param name="cadena"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Devuelve los datos de la empresa</response>
        /// <response code="404">No existe una empresa registrada con ese CIF</response>   
        // GET: api/EmpresasControllerContext/5
        [HttpGet("{cadena}")]
        //public async Task<ActionResult<Empresa>> GetEmpresa(long id)
        public List<Empresa> GetEmpresa(string cadena)
        {
            //var empresaC = _context.EmpresasPAG.FirstOrDefault(empresa => empresa.Cif.Equals(cif));
            var empresaC = _context.EmpresasPAG
                .Where(empresa => empresa.NombreSociedad.Contains(cadena) || empresa.Cif.Equals(cadena));
            Console.WriteLine(empresaC.ToQueryString());

            //var empresaC = _context.Empresas.Select(empresa => new { empresa.NombreFiltrado, empresa.Provincia }).ToList();
            //var student = _context.Students.Select(s => new { s.Name, s.Age, NumberOfEvaluations = s.Evaluations.Count }).ToList();
            //var empresaC = _context.Empresas.FromSqlRaw("SELECT * FROM [Empresas]({0})").FirstOrDefault();//.ToList();
            //var studentForUpdate = _context.Students.FirstOrDefault(s => s.Name.Equals(“Mike Miles”));
            /*
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }
            */
            return empresaC.ToList();
        }

        // PUT: api/EmpresasControllerContext/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(long id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        /// <summary>
        /// Añade una empresa a la base de datos
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/empresaBD/<paramref name="id"/>
        ///     {
        ///        "NombreSociedad": "MERCADONA SA",
        ///        "Cif": "A46103834",
        ///        "CoincidenciaPor": "Acrónimo",
        ///        "TextoCoincidencia": "MERCADONA",
        ///        "Provincia": "VALENCIA",
        ///        "NombreFiltrado": "MERCADONA_SA",
        ///        "LongitudBarra": 100,
        ///        "DatosRegistrales": "Vigente"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">La empresa se ha añadido correctamente</response>
        /// <response code="400">Si no está bien la solicitud</response> 
        // POST: api/EmpresasControllerContext
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            _context.EmpresasPAG.Add(empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }


        // DELETE: api/EmpresasControllerContext/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Empresa>> DeleteEmpresa(long id)
        {
            var empresa = await _context.EmpresasPAG.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _context.EmpresasPAG.Remove(empresa);
            await _context.SaveChangesAsync();

            return empresa;
        }

        private bool EmpresaExists(long id)
        {
            return _context.EmpresasPAG.Any(e => e.Id == id);
        }
    }
}
