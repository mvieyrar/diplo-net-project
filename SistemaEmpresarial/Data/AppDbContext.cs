using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaEmpresarial.Models;
//using MvcClientesEf.Models;

namespace SistemaEmpresarial.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Persona> Personas => Set<Persona>();

        public DbSet<Contacto> Contactos => Set<Contacto>(); //Agregamos el DbSet para Contacto

        public DbSet<Region> Regiones => Set<Region>(); //Agregamos el DBset para Regiones

        public DbSet<Vendedor> Vendedores => Set<Vendedor>(); //Agregamos el DbSet para Vendedor
        public DbSet<Sucursal> Sucursales => Set<Sucursal>(); //Agregamos el DbSet para Sucursal

        public DbSet<Empresa> Empresas => Set<Empresa>();

        public DbSet<Clasificacion> Clasificaciones => Set<Clasificacion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var charToString = new ValueConverter<char, string>(
                v => v.ToString(),
                v => string.IsNullOrEmpty(v) ? ' ' : v[0]);

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.HasKey(e => e.per_id);
                entity.Property(e => e.per_id)
                      .ValueGeneratedOnAdd(); // IDENTITY(1,1)

                entity.Property(e => e.per_rfc)
                      .HasMaxLength(13)
                      .IsUnicode(false)
                      .HasColumnType("varchar(13)");

                entity.Property(e => e.per_nombre)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.per_correo)
                      .HasMaxLength(100)
                      .IsUnicode(false)
                      .HasColumnType("varchar(100)");

                entity.Property(e => e.per_movil)
                      .HasMaxLength(10)
                      .IsUnicode(false)
                      .HasColumnType("varchar(10)");

                entity.Property(e => e.per_fecha_creacion)
                      .HasColumnType("datetime");

            });

            //Catálogos renombrados

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");
                entity.HasKey(e => e.reg_id);
                entity.Property(e => e.reg_nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(50)");
                entity.HasData(
                    new Region { reg_id = 1, reg_nombre = "Norte" },
                    new Region { reg_id = 2, reg_nombre = "Sur" },
                    new Region { reg_id = 3, reg_nombre = "Este" },
                    new Region { reg_id = 4, reg_nombre = "Oeste" }
                );
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.ToTable("sucursal");
                entity.HasKey(e => e.suc_id);
                entity.Property(e => e.suc_nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(50)");
                entity.HasData(
                    new Sucursal { suc_id = 1, suc_nombre = "MEX" },
                    new Sucursal { suc_id = 2, suc_nombre = "GDL" },
                    new Sucursal { suc_id = 3, suc_nombre = "MTY" },
                    new Sucursal { suc_id = 4, suc_nombre = "TIJ" }
                );

            });

            modelBuilder.Entity<Clasificacion>(entity =>
            {
                entity.ToTable("clasificacion");
                entity.HasKey(e => e.cla_id);
                entity.Property(e => e.cla_nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnType("varchar(50)");
                entity.HasData(
                    new Clasificacion { cla_id = 1, cla_nombre = "AAA" },
                    new Clasificacion { cla_id = 2, cla_nombre = "A" },
                    new Clasificacion { cla_id = 3, cla_nombre = "B" },
                    new Clasificacion { cla_id = 4, cla_nombre = "C" }
                );

            });


            // Configuración para la entidad Contacto con FKs
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.ToTable("Contacto");
                entity.HasKey(e => e.con_id);
                entity.Property(e => e.con_id)
                      .ValueGeneratedOnAdd(); // IDENTITY(1,1)                

                entity.Property(e => e.con_nombre)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.con_paterno)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.con_materno)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.con_empresa)
                      .HasMaxLength(100)
                      .IsUnicode(false)
                      .HasColumnType("varchar(100)");

                entity.Property(e => e.con_regionreg_id)
                        .HasColumnType("int");

                entity.Property(e => e.con_red_social)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.con_tipo_red_social)
                      .HasConversion<int>()
                      .HasColumnType("int");

                entity.Property(e => e.con_genero)
                      //.HasConversion(charToString) // Usamos el convertidor aquí
                      .HasMaxLength(1)
                      .IsUnicode(false)
                      .HasColumnType("char(1)");

                entity.Property(e => e.con_disponibilidad_matutino)
                       .HasColumnType("bit");

                entity.Property(e => e.con_disponibilidad_vespertino)
                       .HasColumnType("bit");

                entity.Property(e => e.con_area_interes)
                        .HasConversion<int>()
                      .HasColumnType("int");

                entity.Property(e => e.con_fecha_creacion)
                      .HasColumnType("datetime");

                entity.Property(e => e.con_peso)
                  .HasColumnType("decimal(5,2)");

                entity.Property(e => e.con_comentario)
                    .IsUnicode(false)
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.con_estatus)
                    .HasColumnType("bit");


            });


            // Configuración para la entidad Vendedor con FKs
            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.ToTable("Vendedor");
                entity.HasKey(e => e.ven_id);
                entity.Property(e => e.ven_id)
                      .ValueGeneratedOnAdd(); // IDENTITY(1,1)                

                entity.Property(e => e.ven_nombre)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.ven_paterno)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.ven_materno)
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.ven_sucursalsuc_id)
                        .HasColumnType("int");


                entity.Property(e => e.ven_rol)
                      .HasMaxLength(1)
                      .IsUnicode(false)
                      .HasColumnType("char(1)");

                entity.Property(e => e.ven_clave_empleado)
                      .HasConversion<int>()
                      .HasColumnType("int");

                entity.Property(e => e.ven_clave_agente)
                      .HasMaxLength(5)
                      .IsUnicode(false)
                      .HasColumnType("varchar(5)");

                entity.Property(e => e.ven_estatus)
                    .HasColumnType("bit");

                entity.Property(e => e.ven_objetivo)
                  .HasColumnType("decimal(9,2)");
            });


            // Configuración para la entidad Empresa con FKs
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa");
                entity.HasKey(e => e.emp_id);
                entity.Property(e => e.emp_id)
                      .ValueGeneratedOnAdd(); // IDENTITY(1,1)                

                entity.Property(e => e.emp_nombre)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnType("varchar(50)");

                entity.Property(e => e.emp_tipo)
                      .HasMaxLength(1)
                      .IsUnicode(false)
                      .HasColumnType("char(1)");

                entity.Property(e => e.emp_cp)
                      .HasMaxLength(5)
                      .IsUnicode(false)
                      .HasColumnType("varchar(5)");

                entity.Property(e => e.emp_poblacion)
                      .HasMaxLength(100)
                      .IsUnicode(false)
                      .HasColumnType("varchar(100)");

                entity.Property(e => e.emp_provincia)
                      .HasMaxLength(100)
                      .IsUnicode(false)
                      .HasColumnType("varchar(100)");

                entity.Property(e => e.emp_direccion)
                      .HasMaxLength(255)
                      .IsUnicode(false)
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.emp_telefono)
                      .HasMaxLength(20)
                      .IsUnicode(false)
                      .HasColumnType("varchar(20)");

                entity.Property(e => e.emp_observaciones)
                    .IsUnicode(false)
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.emp_clasificacioncla_id)
                        .HasColumnType("int");

                entity.Property(e => e.emp_web)
                      .HasMaxLength(255)
                      .IsUnicode(false)
                      .HasColumnType("varchar(255)");

                entity.Property(e => e.emp_latitud)
                  .HasColumnType("decimal(9,6)");

                entity.Property(e => e.emp_longitud)
                  .HasColumnType("decimal(9,6)");

                entity.Property(e => e.emp_fecha_creacion)
                      .HasColumnType("datetime");

                entity.Property(e => e.emp_estatus)
                    .HasColumnType("bit");

            });
        }
    }
}