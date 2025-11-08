using Microsoft.EntityFrameworkCore;
using SistemaEmpresarial.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.Repository.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Persona> Personas => Set<Persona>();

        public DbSet<Contacto> Contactos => Set<Contacto>(); //Agregamos el DbSet para Contacto

        public DbSet<Region> Regiones => Set<Region>(); //Agregamos el DBset para Regiones

        public DbSet<Vendedor> Vendedores => Set<Vendedor>(); //Agregamos el DbSet para Vendedor
        public DbSet<Sucursal> Sucursales => Set<Sucursal>(); //Agregamos el DbSet para Sucursal

        public DbSet<Empresa> Empresas => Set<Empresa>();

        public DbSet<Clasificacion> Clasificaciones => Set<Clasificacion>();

    }
}
