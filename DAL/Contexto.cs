using Microsoft.EntityFrameworkCore;
using Registrotecnico.Models;
using System.Collections.Generic;

namespace Registrotecnico.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
              : base(options) { }
        public DbSet<Tecnicos> Tecnico { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
    }
}
