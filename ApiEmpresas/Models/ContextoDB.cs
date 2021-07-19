using Microsoft.EntityFrameworkCore;
//using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Api.Models
{
    public class ContextoDB : DbContext
    {
        public IConfiguration Configuration { get; }
        public ContextoDB(DbContextOptions<ContextoDB> options)
            : base(options)
        {
        }
        public DbSet<Empresa> EmpresasPAG { get; set; }
    }
}