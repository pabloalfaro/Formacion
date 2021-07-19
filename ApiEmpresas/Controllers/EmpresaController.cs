using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/empresa")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private static string _path = Directory.GetCurrentDirectory()+"\\Empresa.json";


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
        // GET: api/<TodoEmpresaController>
        [HttpGet]
        //public IEnumerable<string> Get()
        public string Get()
        {
            string empresasJSON;
            using (var reader = new StreamReader(_path))
            {
                empresasJSON = reader.ReadToEnd();
            }

            //List<TodoEmpresa> empresas = JsonSerializer.Deserialize<List<TodoEmpresa>>(empresasJSON);

            return empresasJSON;
        }


        /// <summary>
        /// Muestra la empresa con el indentificador CIF indicado
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
        /// <param name="Cif"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Devuelve los datos de la empresa</response>
        /// <response code="404">No existe una empresa registrada con ese CIF</response>   
        // GET api/<TodoEmpresaController>/5
        [HttpGet("{Cif}")]
        //public string Get(string Cif)
        public Empresa Get(string Cif)
        {
            string empresasJSON;
            using (var reader = new StreamReader(_path))
            {
                empresasJSON = reader.ReadToEnd();
            }

            List<Empresa> empresas = JsonSerializer.Deserialize<List<Empresa>>(empresasJSON);

            Empresa coincidente = new Empresa();

            foreach (var empresa in empresas)
            {
                if (empresa.Cif == Cif)
                {
                    coincidente = empresa;
                    break;
                }
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            string jsonString = JsonSerializer.Serialize(coincidente, options);

            return coincidente;
        }

        /*
        // POST api/<TodoEmpresaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoEmpresaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoEmpresaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
